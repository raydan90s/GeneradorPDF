using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd;
using Yachasoft.Sri.Xsd.Contratos.NotaDebito_1_0_0;
using Yachasoft.Sri.Xsd.Map;
using Yachasoft.Sri.FacturacionElectronica.Models.Request;
using Yachasoft.Sri.FacturacionElectronica.Services;

namespace Yachasoft.Sri.FacturacionElectronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaDebitoController : ControllerBase
    {
        private readonly Signer.ICertificadoService certificadoService;
        private readonly WebService.ISriWebService webService;
        private readonly Ride.IRIDEService rIDEService;
        private readonly FrappeFileUploader _frappeUploader;

        public NotaDebitoController(
            Signer.ICertificadoService certificadoService,
            WebService.ISriWebService webService,
            Ride.IRIDEService rIDEService,
            FrappeFileUploader frappeUploader)
        {
            this.certificadoService = certificadoService;
            this.webService = webService;
            this.rIDEService = rIDEService;
            this._frappeUploader = frappeUploader;
        }

        [HttpPost("GenerarNotaDebito")]
        public async Task<IActionResult> GenerarNotaDebito([FromBody] NotaDebitoRequest request)
        {
            try
            {
                // 1. EMISOR - Construir desde request.Emisor
                var emisor = new Emisor
                {
                    DireccionMatriz = request.Emisor.DireccionMatriz,
                    EnumTipoAmbiente = EnumParserHelper.ParseTipoAmbiente(request.Emisor.EnumTipoAmbiente),
                    Logo = "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/Logo_UTPL.png",
                    NombreComercial = request.Emisor.NombreComercial,
                    ObligadoContabilidad = request.Emisor.ObligadoContabilidad,
                    RazonSocial = request.Emisor.RazonSocial,
                    RegimenMicroEmpresas = request.Emisor.RegimenMicroEmpresas,
                    RUC = request.Emisor.RUC,
                    ContribuyenteEspecial = request.Emisor.ContribuyenteEspecial,
                    AgenteRetencion = request.Emisor.AgenteRetencion,
                };

                // 2. ESTABLECIMIENTO - Construir con CodigoEstablecimiento
                var establecimiento = new Establecimiento
                {
                    Codigo = request.CodigoEstablecimiento,
                    DireccionEstablecimiento = request.Emisor.DireccionEstablecimiento,
                    Emisor = emisor
                };

                // 3. PUNTO EMISION - Construir con CodigoPuntoEmision
                var puntoEmision = new PuntoEmision
                {
                    Codigo = request.CodigoPuntoEmision,
                    Establecimiento = establecimiento
                };

                // 4. DIRECCION CLIENTE - Extraer de InfoAdicional (igual que factura)
                string direccionCliente = null;
                if (request.InfoAdicional != null)
                {
                    var direccionAdicional = request.InfoAdicional.FirstOrDefault(ca =>
                        ca.Nombre.Equals("Direccion", StringComparison.OrdinalIgnoreCase) ||
                        ca.Nombre.Equals("Dirección", StringComparison.OrdinalIgnoreCase));
                    direccionCliente = direccionAdicional?.Valor;
                }

                // 5. MOTIVOS - Mapear desde request.Motivos
                var motivos = request.Motivos.Select(m => new Motivo
                {
                    Razon = m.Razon,
                    Valor = m.Valor
                }).ToList();

                // 6. DOCUMENTO MODIFICADO - Construir desde request.DocumentoModificado
                var documentoModificado = new DocumentoSustento
                {
                    CodDocumento = EnumParserHelper.ParseTipoDocumento(request.DocumentoModificado.CodDocumento),
                    NumDocumento = request.DocumentoModificado.NumDocumento,
                    FechaEmisionDocumento = request.DocumentoModificado.FechaEmisionDocumento
                };

                // 7. IMPUESTOS - Mapear desde request.InfoNotaDebito.Impuestos
                var impuestosMapeados = MapperHelper.MapearImpuestosVenta(request.InfoNotaDebito.Impuestos);

                // 8. NOTA DE DEBITO - Construir modelo completo
                var notaDebito = new NotaDebito_1_0_0Modelo.NotaDebito
                {
                    PuntoEmision = puntoEmision,
                    FechaEmision = request.FechaEmision,

                    Sujeto = new Sujeto
                    {
                        Identificacion = request.Cliente.Identificacion,
                        RazonSocial = request.Cliente.RazonSocial,
                        TipoIdentificador = EnumParserHelper.ParseTipoIdentificacion(request.Cliente.TipoIdentificador)
                    },

                    InfoNotaDebito = new NotaDebito_1_0_0Modelo.InfoNotaDebito
                    {
                        DocumentoModificado = documentoModificado,
                        TotalSinImpuestos = request.InfoNotaDebito.TotalSinImpuestos,
                        Impuestos = impuestosMapeados,
                        ValorTotal = request.InfoNotaDebito.ValorTotal,
                        Pagos = MapperHelper.MapearPagosParaDocumento(request.InfoNotaDebito.Pagos)
                    },

                    Motivos = motivos,
                    InfoAdicional = request.InfoAdicional
                };

                // 9. INFO TRIBUTARIA - Construir con Secuencial y EnumTipoEmision
                notaDebito.InfoTributaria = new InfoTributaria
                {
                    Secuencial = request.Secuencial,
                    EnumTipoEmision = EnumParserHelper.ParseTipoEmision(request.EnumTipoEmision)
                };

                // 10. CLAVE DE ACCESO - Generar automáticamente
                notaDebito.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(
                    NotaDebito_1_0_0Modelo.TipoDocumento,
                    notaDebito.FechaEmision,
                    notaDebito.PuntoEmision,
                    notaDebito.InfoTributaria.Secuencial,
                    notaDebito.InfoTributaria.EnumTipoEmision
                );

                // 11. MAPEAR A XML - Usar NotaDebito_1_0_0Mapper
                var xmlObj = NotaDebito_1_0_0Mapper.Map(notaDebito);

                // 12. SERIALIZAR XML
                var xmlDoc = new XmlDocument();
                using (var memoryStream = new MemoryStream())
                {
                    var serializer = new XmlSerializer(typeof(notaDebito));
                    serializer.Serialize(memoryStream, xmlObj);
                    memoryStream.Position = 0;
                    xmlDoc.Load(memoryStream);
                }

                xmlDoc.DocumentElement.SetAttribute("id", "comprobante");

                // 13. FIRMAR DOCUMENTO
                // Cambiar línea 163-166:
                certificadoService.CargarDesdeP12(
                    "C:\\Users\\Sistemas\\Documents\\GeneradorPDF\\Yachasoft.Sri.FacturacionElectronica\\signature.p12",
                    "Compus1234"
                );
                var xmlFirmado = certificadoService.FirmarDocumento(xmlDoc);

                // 14. GUARDAR XML FIRMADO
                var nombreArchivoXml = $"NOTADEBITO_{notaDebito.InfoTributaria.ClaveAcceso}.xml";
                xmlFirmado.Save(nombreArchivoXml);

                // 15. VALIDAR COMPROBANTE EN EL SRI
                var envio = await webService.ValidarComprobanteAsync(xmlFirmado);
                Console.WriteLine($"ESTADO DE COMPROBANTE DE ENVIO: {System.Text.Json.JsonSerializer.Serialize(envio, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

                if (envio.Ok)
                {
                    // 16. ESPERAR 3 SEGUNDOS
                    System.Threading.Thread.Sleep(3000);

                    // 17. OBTENER AUTORIZACION
                    var auto = await webService.AutorizacionComprobanteAsync(notaDebito.InfoTributaria.ClaveAcceso);
                    var autorizacionData = auto.Data?.Autorizaciones?.Autorizacion?.FirstOrDefault();
                    Console.WriteLine($"ESTADO DE COMPROBANTE DE AUTORIZACION: {System.Text.Json.JsonSerializer.Serialize(auto, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

                    if (auto.Ok)
                    {
                        Console.WriteLine("AUTORIZADO");

                        // 18. ASIGNAR DATOS DE AUTORIZACION
                        if (autorizacionData != null)
                        {
                            notaDebito.Autorizacion.Numero = autorizacionData.NumeroAutorizacion;
                            if (DateTimeOffset.TryParse(autorizacionData.FechaAutorizacion, out var fechaOffset))
                            {
                                notaDebito.Autorizacion.Fecha = fechaOffset.ToOffset(TimeSpan.FromHours(-5)).DateTime;
                            }
                            else
                            {
                                throw new Exception($"Fecha de autorización inválida: {autorizacionData.FechaAutorizacion}");
                            }
                        }

                        // 19. GENERAR RIDE (PDF)
                        var rutaPDF = $"C:\\Users\\Sistemas\\Documents\\GeneradorPDF\\Yachasoft.Sri.FacturacionElectronica\\NOTADEBITO_{notaDebito.InfoTributaria.ClaveAcceso}.pdf";
                        rIDEService.NotaDebito_1_0_0(notaDebito, rutaPDF);

                        // 20. SUBIR PDF A FRAPPE
                        var respuestaUploadPDF = await _frappeUploader.UploadFileAsync(
                            rutaPDF,
                            Path.GetFileName(rutaPDF),
                            folder: "Home/NotaDebito/PDF"
                        );
                        Console.WriteLine("📤 Archivo PDF subido a Frappe:");
                        Console.WriteLine(respuestaUploadPDF);

                        // 21. SUBIR XML A FRAPPE
                        var rutaXML = $"C:\\Users\\Sistemas\\Documents\\GeneradorPDF\\Yachasoft.Sri.FacturacionElectronica\\NOTADEBITO_{notaDebito.InfoTributaria.ClaveAcceso}.xml";
                        var respuestaUploadXML = await _frappeUploader.UploadFileAsync(
                            rutaXML,
                            Path.GetFileName(rutaXML),
                            folder: "Home/NotaDebito/XML"
                        );
                        Console.WriteLine("📤 Archivo XML subido a Frappe:");
                        Console.WriteLine(respuestaUploadXML);

                        // 22. LIMPIAR ARCHIVOS LOCALES
                        await FileCleanupHelper.DeleteFileAsync(rutaPDF);
                        await FileCleanupHelper.DeleteFileAsync(rutaXML);

                        // 23. RETORNAR RESULTADO EXITOSO
                        var resultado = new
                        {
                            success = true,
                            claveAcceso = notaDebito.InfoTributaria.ClaveAcceso,
                            mensaje = "Nota de Débito autorizada, PDF generado y archivos subidos a Frappe correctamente",
                            numeroAutorizacion = notaDebito.Autorizacion.Numero,
                            fechaAutorizacion = notaDebito.Autorizacion.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                            respuestaFrappePDF = respuestaUploadPDF,
                            respuestaFrappeXML = respuestaUploadXML
                        };

                        return Ok(resultado);
                    }
                    else
                    {
                        // 24. MANEJAR ERROR DE AUTORIZACION
                        var mensajesAutorizacion = autorizacionData?.Mensajes?.Mensaje?
                            .Select(m => new { m.Identificador, m.Mensaje_, m.Tipo, m.InformacionAdicional })
                            .ToList();

                        if (mensajesAutorizacion != null && mensajesAutorizacion.Count > 0)
                        {
                            Console.WriteLine("MENSAJES DE AUTORIZACIÓN:");
                            foreach (var msg in mensajesAutorizacion)
                            {
                                Console.WriteLine($"- Identificador: {msg.Identificador}");
                                Console.WriteLine($"  Mensaje: {msg.Mensaje_}");
                                Console.WriteLine($"  Tipo: {msg.Tipo}");
                                Console.WriteLine($"  Información Adicional: {msg.InformacionAdicional}");
                                Console.WriteLine("-------------------------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay mensajes de autorización disponibles.");
                        }

                        return Ok(new
                        {
                            success = false,
                            estado = autorizacionData?.Estado,
                            mensajes = mensajesAutorizacion
                        });
                    }
                }
                else
                {
                    // 25. MANEJAR ERROR DE ENVIO
                    var primerComprobante = envio.Data?.Comprobantes?.Comprobante?.FirstOrDefault();
                    var mensajesEnvio = primerComprobante?.Mensajes?.Mensaje
                        ?.Select(m => new { m.Identificador, m.Mensaje_, m.Tipo, m.InformacionAdicional })
                        .ToList();

                    if (mensajesEnvio != null && mensajesEnvio.Count > 0)
                    {
                        Console.WriteLine("MENSAJES DE ENVÍO:");
                        foreach (var msg in mensajesEnvio)
                        {
                            Console.WriteLine($"- Identificador: {msg.Identificador}");
                            Console.WriteLine($"  Mensaje: {msg.Mensaje_}");
                            Console.WriteLine($"  Tipo: {msg.Tipo}");
                            Console.WriteLine($"  Información Adicional: {msg.InformacionAdicional}");
                            Console.WriteLine("-------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay mensajes de envío disponibles.");
                    }

                    return Ok(new
                    {
                        success = false,
                        estado = envio.Data?.Estado,
                        mensajes = mensajesEnvio
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                return BadRequest(new
                {
                    success = false,
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}