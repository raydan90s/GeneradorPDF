using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Core.Atributos;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd;
using Yachasoft.Sri.Xsd.Map;
using Yachasoft.Sri.FacturacionElectronica.Models.Request;
using Yachasoft.Core.Extensions;

namespace Yachasoft.Sri.FacturacionElectronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetencionController : ControllerBase
    {
        private readonly Signer.ICertificadoService certificadoService;
        private readonly WebService.ISriWebService webService;
        private readonly Ride.IRIDEService rIDEService;

        public RetencionController(
            Signer.ICertificadoService certificadoService,
            WebService.ISriWebService webService,
            Ride.IRIDEService rIDEService)
        {
            this.certificadoService = certificadoService;
            this.webService = webService;
            this.rIDEService = rIDEService;
        }

        [HttpPost("GenerarRetencion")]
        public async Task<IActionResult> GenerarRetencion([FromBody] RetencionRequest request)
        {
            Console.Write("LLego la peticion");
            try
            {
                Console.WriteLine($"Recibido request para RUC: {request.Emisor.RUC}, Secuencial: {request.Secuencial}, Fecha: {request.FechaEmision}");
                var emisor = new Emisor
                {
                    DireccionMatriz = request.Emisor.DireccionMatriz,
                    EnumTipoAmbiente = ParseTipoAmbiente(request.Emisor.EnumTipoAmbiente),
                    Logo = "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/Logo_UTPL.png",
                    NombreComercial = request.Emisor.NombreComercial,
                    ObligadoContabilidad = request.Emisor.ObligadoContabilidad,
                    RazonSocial = request.Emisor.RazonSocial,
                    RegimenMicroEmpresas = request.Emisor.RegimenMicroEmpresas,
                    RUC = request.Emisor.RUC,
                    ContribuyenteEspecial = request.Emisor.ContribuyenteEspecial,
                    AgenteRetencion = request.Emisor.AgenteRetencion,
                };

                var establecimiento = new Establecimiento
                {
                    Codigo = request.CodigoEstablecimiento,
                    DireccionEstablecimiento = request.Emisor.DireccionEstablecimiento,
                    Emisor = emisor
                };

                var puntoEmision = new PuntoEmision
                {
                    Codigo = request.CodigoPuntoEmision,
                    Establecimiento = establecimiento
                };

                var retencion = new ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion
                {
                    PuntoEmision = puntoEmision,
                    FechaEmision = request.FechaEmision,
                    InfoCompRetencion = new ComprobanteRetencion_1_0_0Modelo.InfoCompRetencion
                    {
                        PeriodoFiscal = request.PeriodoFiscal,
                    },
                    Sujeto = new Sujeto
                    {
                        Identificacion = request.Sujeto.Identificacion,
                        RazonSocial = request.Sujeto.RazonSocial,
                        TipoIdentificador = ParseTipoIdentificacion(request.Sujeto.TipoIdentificador)
                    },
                    Impuestos = MapearImpuestos(request.Impuestos),
                    InfoAdicional = request.InfoAdicional
                };

                retencion.InfoTributaria = new InfoTributaria
                {
                    Secuencial = request.Secuencial,
                    EnumTipoEmision = ParseTipoEmision(request.EnumTipoEmision)
                };

                retencion.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(
                    retencion.TipoDocumento,
                    retencion.FechaEmision,
                    retencion.PuntoEmision,
                    retencion.InfoTributaria.Secuencial,
                    retencion.InfoTributaria.EnumTipoEmision
                );

                var comprobanteXml = ComprobanteRetencion_1_0_0Mapper.Map(retencion);

                certificadoService.CargarDesdeP12("/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/signature.p12", "Compus1234");
                var xmlFirmado = certificadoService.FirmarDocumento(comprobanteXml);

                var nombreArchivoXml = $"COMPROBANTE_RETENCION_{retencion.InfoTributaria.ClaveAcceso}.xml";
                xmlFirmado.Save(nombreArchivoXml);

                var envio = await webService.ValidarComprobanteAsync(xmlFirmado);
                Console.WriteLine($"ESTADO DE COMPROBANTE DE ENVIO: {System.Text.Json.JsonSerializer.Serialize(envio, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");
                if (envio.Ok)
                {
                    Console.WriteLine("RECIBIDA");
                    System.Threading.Thread.Sleep(3000);

                    var auto = await webService.AutorizacionComprobanteAsync(retencion.InfoTributaria.ClaveAcceso);
                    Console.WriteLine($"ESTADO DE COMPROBANTE DE AUTORIZACION: {System.Text.Json.JsonSerializer.Serialize(auto, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");
                    if (auto.Ok)
                    {
                        Console.WriteLine(auto.Ok ? "AUTORIZADO" : auto.Error);
                        rIDEService.ComprobanteRetencion_1_0_0(retencion, "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/RETENCION.pdf");
                        Console.WriteLine("PDF generado correctamente");
                        
                        return Ok(new
                        {
                            success = true,
                            claveAcceso = retencion.InfoTributaria.ClaveAcceso,
                            mensaje = "Retención autorizada y PDF generado correctamente",
                            autorizacion = auto
                        });
                    }

                    return Ok(new
                    {
                        success = false,
                        mensaje = "Error en la autorización",
                        error = auto.Error,
                        autorizacion = auto
                    });
                }

                return Ok(new
                {
                    success = false,
                    mensaje = "Error en el envío al SRI",
                    error = envio.Error,
                    envio = envio
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        #region Métodos de Mapeo y Parseo

        private List<ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion> MapearImpuestos(List<ImpuestoRetencionRequest> impuestos)
        {
            var resultado = new List<ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion>();

            foreach (var impuesto in impuestos)
            {
                try
                {
                    Console.WriteLine($"Procesando impuesto con código: {impuesto.CodigoRetencion}");
                    
                    var codigoRetencion = ParseCodigoRetencion(impuesto.CodigoRetencion);
                    Console.WriteLine($"Código parseado: {codigoRetencion.GetType().Name} = {codigoRetencion}");
                    
                    if (codigoRetencion is EnumTipoRetencionRenta)
                    {
                        var impuestoRenta = new ComprobanteRetencion_1_0_0Modelo.ImpuestoRenta
                        {
                            BaseImponible = impuesto.BaseImponible,
                            Tarifa = impuesto.Tarifa,
                            Valor = Math.Round(impuesto.BaseImponible * impuesto.Tarifa / 100, 2),
                            CodigoRetencion = (EnumTipoRetencionRenta)codigoRetencion,
                            DocumentoSustento = MapearDocumentoSustento(impuesto.DocumentoSustento)
                        };
                        
                        Console.WriteLine($"Impuesto Renta creado - Base: {impuestoRenta.BaseImponible}, Código: {impuestoRenta.CodigoRetencion}");
                        resultado.Add(impuestoRenta);
                    }
                    else if (codigoRetencion is EnumTipoRetencionIVA)
                    {
                        var impuestoIVA = new ComprobanteRetencion_1_0_0Modelo.ImpuestoIVA
                        {
                            BaseImponible = impuesto.BaseImponible,
                            Tarifa = impuesto.Tarifa,
                            Valor = Math.Round(impuesto.BaseImponible * impuesto.Tarifa / 100, 2),
                            CodigoRetencion = (EnumTipoRetencionIVA)codigoRetencion,
                            DocumentoSustento = MapearDocumentoSustento(impuesto.DocumentoSustento)
                        };

                        
                        Console.WriteLine($"Impuesto IVA creado - Base: {impuestoIVA.BaseImponible}, Código: {impuestoIVA.CodigoRetencion}");
                        resultado.Add(impuestoIVA);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al mapear impuesto: {ex.Message}");
                    throw new ArgumentException($"Error al mapear impuesto con código {impuesto.CodigoRetencion}: {ex.Message}", ex);
                }
            }

            return resultado;
        }

        private DocumentoSustento MapearDocumentoSustento(DocumentoSustentoRequest documentoRequest)
        {
            if (documentoRequest == null)
                throw new ArgumentNullException(nameof(documentoRequest), "El documento sustento no puede ser nulo");

            var tipoDocumento = Enum.GetValues(typeof(EnumTipoDocumento))
                                    .Cast<EnumTipoDocumento>()
                                    .FirstOrDefault(e => e.GetAttributeOfType<SRICodigoAttribute>()?.Code == documentoRequest.CodDocumento.ToString());

           if (!Enum.IsDefined(typeof(EnumTipoDocumento), tipoDocumento))
                throw new ArgumentException($"Código de documento inválido: {documentoRequest.CodDocumento}");

            Console.WriteLine($"Código de documento a retener: {documentoRequest.CodDocumento} mapeado es {tipoDocumento}");

            return new DocumentoSustento
            {
                CodDocumento = tipoDocumento,
                NumDocumento = documentoRequest.NumDocumento,
                FechaEmisionDocumento = documentoRequest.FechaEmisionDocumento
            };
        }


        private object ParseCodigoRetencion(string codigoRetencion)
        {
            // Primero intentar buscar por SRICodigo en EnumTipoRetencionIVA
            var enumIVA = BuscarEnumPorSRICodigo<EnumTipoRetencionIVA>(codigoRetencion);
            if (enumIVA.HasValue)
            {
                Console.WriteLine($"Encontrado en IVA: {enumIVA.Value}");
                return enumIVA.Value;
            }

            // Luego intentar buscar por SRICodigo en EnumTipoRetencionRenta
            var enumRenta = BuscarEnumPorSRICodigo<EnumTipoRetencionRenta>(codigoRetencion);
            if (enumRenta.HasValue)
            {
                Console.WriteLine($"Encontrado en Renta: {enumRenta.Value}");
                return enumRenta.Value;
            }

            // Si no se encuentra por código SRI, intentar parsear por nombre
            if (Enum.TryParse<EnumTipoRetencionIVA>(codigoRetencion, true, out var iva))
            {
                return iva;
            }
            
            if (Enum.TryParse<EnumTipoRetencionRenta>(codigoRetencion, true, out var renta))
            {
                return renta;
            }

            throw new ArgumentException($"Código de retención inválido: '{codigoRetencion}'. No se encontró en ningún enum de retención.");
        }

        private T? BuscarEnumPorSRICodigo<T>(string codigo) where T : struct, Enum
        {
            var enumType = typeof(T);
            foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var atributo = field.GetCustomAttribute<SRICodigoAttribute>();
                if (atributo != null && atributo.Code == codigo)
                {
                    return (T)field.GetValue(null);
                }
            }
            return null;
        }

        private EnumTipoAmbiente ParseTipoAmbiente(string tipoAmbiente)
        {
            // Buscar por SRICodigo
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoAmbiente>(tipoAmbiente);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }

            // Intentar por nombre
            if (Enum.TryParse<EnumTipoAmbiente>(tipoAmbiente, true, out var resultado))
            {
                return resultado;
            }

            throw new ArgumentException($"Tipo de ambiente inválido: {tipoAmbiente}");
        }

        private EnumTipoIdentificacion ParseTipoIdentificacion(string tipoIdentificacion)
        {
            // Buscar por SRICodigo
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoIdentificacion>(tipoIdentificacion);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }

            // Intentar por nombre
            if (Enum.TryParse<EnumTipoIdentificacion>(tipoIdentificacion, true, out var resultado))
            {
                return resultado;
            }

            throw new ArgumentException($"Tipo de identificación inválido: {tipoIdentificacion}");
        }

        private EnumTipoEmision ParseTipoEmision(string tipoEmision)
        {
            // Buscar por SRICodigo
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoEmision>(tipoEmision);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }

            // Intentar por nombre
            if (Enum.TryParse<EnumTipoEmision>(tipoEmision, true, out var resultado))
            {
                return resultado;
            }

            throw new ArgumentException($"Tipo de emisión inválido: {tipoEmision}");
        }

        #endregion
    }
}