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
using Yachasoft.Sri.Xsd.Contratos.NotaCredito_1_0_0;
using Yachasoft.Sri.Xsd.Map;
using Yachasoft.Sri.FacturacionElectronica.Models.Request;
using Yachasoft.Sri.FacturacionElectronica.Services;

namespace Yachasoft.Sri.FacturacionElectronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaCreditoController : ControllerBase
    {
        private readonly Signer.ICertificadoService certificadoService;
        private readonly WebService.ISriWebService webService;
        private readonly Ride.IRIDEService rIDEService;
        private readonly FrappeFileUploader _frappeUploader;

        public NotaCreditoController(
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

        [HttpPost("GenerarNotaCredito")]
        public async Task<IActionResult> GenerarNotaCredito([FromBody] NotaCreditoRequest request)
        {
            try
            {
                // 1. EMISOR
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
                    AgenteRetencion = request.Emisor.AgenteRetencion
                };

                // 2. ESTABLECIMIENTO
                var establecimiento = new Establecimiento
                {
                    Codigo = request.CodigoEstablecimiento,  // ✅ DIRECTO, SIN ToString
                    DireccionEstablecimiento = request.Emisor.DireccionEstablecimiento,
                    Emisor = emisor
                };

                // 3. PUNTO EMISION
                var puntoEmision = new PuntoEmision
                {
                    Codigo = request.CodigoPuntoEmision,  // ✅ DIRECTO, SIN ToString
                    Establecimiento = establecimiento
                };

                // 4. DETALLES
                var detalles = new List<DetalleDocumentoItemPrecio>();
                if (request.Detalles != null && request.Detalles.Any())
                {
                    detalles = request.Detalles.Select(d =>
                    {
                        var detalle = new DetalleDocumentoItemPrecio
                        {
                            Item = new Item
                            {
                                CodigoPrincipal = d.CodigoInterno ?? "SIN_CODIGO",
                                CodigoAuxiliar = d.CodigoAdicional ?? "",
                                Descripcion = d.Descripcion ?? "Sin descripción"
                            },
                            Cantidad = (int)d.Cantidad,
                            PrecioUnitario = d.PrecioUnitario,
                            Descuento = d.Descuento,
                            PrecioTotalSinImpuesto = d.PrecioTotalSinImpuesto,
                            Impuestos = new List<Impuesto>(),
                            DetallesAdicionales = new List<CampoAdicional>()  // ✅ ESTO FALTABA
                        };

                        if (d.Impuestos != null && d.Impuestos.Any())
                        {
                            var impuestosVenta = MapperHelper.MapearImpuestosVenta(d.Impuestos);
                            detalle.Impuestos = impuestosVenta.OfType<Impuesto>().ToList();
                        }

                        return detalle;
                    }).ToList();
                }

                // 5. DOCUMENTO MODIFICADO
                var documentoModificado = new DocumentoSustento
                {
                    CodDocumento = EnumParserHelper.ParseTipoDocumento(request.DocumentoModificado.CodDocumento),
                    NumDocumento = request.DocumentoModificado.NumDocumento,
                    FechaEmisionDocumento = request.DocumentoModificado.FechaEmisionDocumento
                };

                // 6. IMPUESTOS TOTALES
                var impuestosTotales = MapperHelper.MapearImpuestosVenta(request.InfoNotaCredito.Impuestos);

                // 7. NOTA DE CREDITO
                var notaCredito = new NotaCredito_1_0_0Modelo.NotaCredito
                {
                    PuntoEmision = puntoEmision,
                    FechaEmision = request.FechaEmision,

                    Sujeto = new Sujeto
                    {
                        Identificacion = request.Cliente.Identificacion,
                        RazonSocial = request.Cliente.RazonSocial,
                        TipoIdentificador = EnumParserHelper.ParseTipoIdentificacion(request.Cliente.TipoIdentificador)
                    },

                    InfoNotaCredito = new NotaCredito_1_0_0Modelo.InfoNotaCredito
                    {
                        DocumentoModificado = documentoModificado,
                        TotalSinImpuestos = request.InfoNotaCredito.TotalSinImpuestos,
                        TotalConImpuestos = impuestosTotales,
                        ValorModificacion = request.InfoNotaCredito.ValorTotal,
                        Motivo = request.InfoNotaCredito.Motivo
                    },

                    Detalles = detalles,
                    InfoAdicional = request.InfoAdicional
                };

                // 8. INFO TRIBUTARIA
                notaCredito.InfoTributaria = new InfoTributaria
                {
                    Secuencial = request.Secuencial,
                    EnumTipoEmision = EnumParserHelper.ParseTipoEmision(request.EnumTipoEmision)
                };

                // 9. CLAVE DE ACCESO
                notaCredito.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(
                    NotaCredito_1_0_0Modelo.TipoDocumento,
                    notaCredito.FechaEmision,
                    notaCredito.PuntoEmision,
                    notaCredito.InfoTributaria.Secuencial,
                    notaCredito.InfoTributaria.EnumTipoEmision
                );

                // 10. MAPEAR A XML
                var xmlObj = NotaCredito_1_0_0Mapper.Map(notaCredito);

                // 11. SERIALIZAR XML
                var xmlDoc = new XmlDocument();
                using (var memoryStream = new MemoryStream())
                {
                    var serializer = new XmlSerializer(typeof(notaCredito));
                    serializer.Serialize(memoryStream, xmlObj);
                    memoryStream.Position = 0;
                    xmlDoc.Load(memoryStream);
                }

                xmlDoc.DocumentElement.SetAttribute("id", "comprobante");

                // 12. FIRMAR DOCUMENTO
                certificadoService.CargarDesdeP12(
                    "C:\\Users\\Sistemas\\Documents\\GeneradorPDF\\Yachasoft.Sri.FacturacionElectronica\\signature.p12",
                    "Compus1234"
                );
                var xmlFirmado = certificadoService.FirmarDocumento(xmlDoc);

                // 13. GUARDAR XML FIRMADO
                var nombreArchivoXml = $"NOTACREDITO_{notaCredito.InfoTributaria.ClaveAcceso}.xml";
                xmlFirmado.Save(nombreArchivoXml);

                // 14. VALIDAR COMPROBANTE EN EL SRI
                var envio = await webService.ValidarComprobanteAsync(xmlFirmado);
                Console.WriteLine($"ESTADO DE COMPROBANTE DE ENVIO: {System.Text.Json.JsonSerializer.Serialize(envio, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

                if (envio.Ok)
                {
                    // 15. ESPERAR 3 SEGUNDOS
                    System.Threading.Thread.Sleep(3000);

                    // 16. OBTENER AUTORIZACION
                    var auto = await webService.AutorizacionComprobanteAsync(notaCredito.InfoTributaria.ClaveAcceso);
                    var autorizacionData = auto.Data?.Autorizaciones?.Autorizacion?.FirstOrDefault();
                    Console.WriteLine($"ESTADO DE COMPROBANTE DE AUTORIZACION: {System.Text.Json.JsonSerializer.Serialize(auto, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

                    if (auto.Ok)
                    {
                        Console.WriteLine("AUTORIZADO");

                        // 17. ASIGNAR DATOS DE AUTORIZACION
                        if (autorizacionData != null)
                        {
                            notaCredito.Autorizacion.Numero = autorizacionData.NumeroAutorizacion;
                            if (DateTimeOffset.TryParse(autorizacionData.FechaAutorizacion, out var fechaOffset))
                            {
                                notaCredito.Autorizacion.Fecha = fechaOffset.ToOffset(TimeSpan.FromHours(-5)).DateTime;
                            }
                            else
                            {
                                throw new Exception($"Fecha de autorización inválida: {autorizacionData.FechaAutorizacion}");
                            }
                        }

                        // 18. GENERAR RIDE (PDF)
                        var rutaPDF = $"C:\\Users\\Sistemas\\Documents\\GeneradorPDF\\Yachasoft.Sri.FacturacionElectronica\\NOTACREDITO_{notaCredito.InfoTributaria.ClaveAcceso}.pdf";
                        rIDEService.NotaCredito_1_0_0(notaCredito, rutaPDF);

                        // 19. SUBIR PDF A FRAPPE
                        var respuestaUploadPDF = await _frappeUploader.UploadFileAsync(
                            rutaPDF,
                            Path.GetFileName(rutaPDF),
                            folder: "Home/NotaCredito/PDF"
                        );
                        Console.WriteLine("📤 Archivo PDF subido a Frappe:");
                        Console.WriteLine(respuestaUploadPDF);

                        // 20. SUBIR XML A FRAPPE
                        var rutaXML = $"C:\\Users\\Sistemas\\Documents\\GeneradorPDF\\Yachasoft.Sri.FacturacionElectronica\\NOTACREDITO_{notaCredito.InfoTributaria.ClaveAcceso}.xml";
                        var respuestaUploadXML = await _frappeUploader.UploadFileAsync(
                            rutaXML,
                            Path.GetFileName(rutaXML),
                            folder: "Home/NotaCredito/XML"
                        );
                        Console.WriteLine("📤 Archivo XML subido a Frappe:");
                        Console.WriteLine(respuestaUploadXML);

                        // 21. LIMPIAR ARCHIVOS LOCALES
                        await FileCleanupHelper.DeleteFileAsync(rutaPDF);
                        await FileCleanupHelper.DeleteFileAsync(rutaXML);

                        // 22. RETORNAR RESULTADO EXITOSO
                        var resultado = new
                        {
                            success = true,
                            claveAcceso = notaCredito.InfoTributaria.ClaveAcceso,
                            mensaje = "Nota de Crédito autorizada, PDF generado y archivos subidos a Frappe correctamente",
                            numeroAutorizacion = notaCredito.Autorizacion.Numero,
                            fechaAutorizacion = notaCredito.Autorizacion.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                            respuestaFrappePDF = respuestaUploadPDF,
                            respuestaFrappeXML = respuestaUploadXML
                        };

                        return Ok(resultado);
                    }
                    else
                    {
                        // 23. MANEJAR ERROR DE AUTORIZACION
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
                    // 24. MANEJAR ERROR DE ENVIO
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