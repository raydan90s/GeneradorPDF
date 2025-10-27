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
using Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0;
using Yachasoft.Sri.Xsd.Map;
using Yachasoft.Sri.FacturacionElectronica.Models.Request;
using Yachasoft.Sri.FacturacionElectronica.Services;

namespace Yachasoft.Sri.FacturacionElectronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly Signer.ICertificadoService certificadoService;
        private readonly WebService.ISriWebService webService;
        private readonly Ride.IRIDEService rIDEService;
        private readonly FrappeFileUploader _frappeUploader;

        public FacturaController(
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

        [HttpPost("GenerarFactura")]
        public async Task<IActionResult> GenerarFactura([FromBody] FacturaRequest request)
        {
            try
            {
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

                string direccionCliente = null;
                if (request.InfoAdicional != null)
                {
                    var direccionAdicional = request.InfoAdicional.FirstOrDefault(ca => 
                        ca.Nombre.Equals("Direccion", StringComparison.OrdinalIgnoreCase) || 
                        ca.Nombre.Equals("Dirección", StringComparison.OrdinalIgnoreCase));
                    direccionCliente = direccionAdicional?.Valor;
                }

                var detallesMapeados = MapperHelper.MapearDetallesConSubsidio(request.Detalles);

                var factura = new Factura_1_0_0Modelo.Factura
                {
                    PuntoEmision = puntoEmision,
                    FechaEmision = request.FechaEmision,
                    
                    Sujeto = new Sujeto
                    {
                        Identificacion = request.Cliente.Identificacion,
                        RazonSocial = request.Cliente.RazonSocial,
                        TipoIdentificador = EnumParserHelper.ParseTipoIdentificacion(request.Cliente.TipoIdentificador)
                    },
                    
                    InfoFactura = new Factura_1_0_0Modelo.InfoFactura
                    {
                        TotalSinImpuestos = request.InfoFactura.TotalSinImpuestos,
                        TotalDescuento = request.InfoFactura.TotalDescuento,
                        ImporteTotal = request.InfoFactura.ImporteTotal,
                        DireccionComprador = direccionCliente,
                        
                        TotalConImpuestos = MapperHelper.MapearImpuestosVentaDesdeDetallesConSubsidio(detallesMapeados),
                        Pagos = MapperHelper.MapearPagos(request.InfoFactura.Pagos)
                    },
                    
                    Detalles = detallesMapeados, 
                    InfoAdicional = request.InfoAdicional
                };

                factura.InfoTributaria = new InfoTributaria
                {
                    Secuencial = request.Secuencial,
                    EnumTipoEmision = EnumParserHelper.ParseTipoEmision(request.EnumTipoEmision)
                };

                factura.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(
                    Factura_1_0_0Modelo.TipoDocumento,
                    factura.FechaEmision,
                    factura.PuntoEmision,
                    factura.InfoTributaria.Secuencial,
                    factura.InfoTributaria.EnumTipoEmision
                );

                var xmlObj = Factura_1_0_0Mapper.Map(factura);

                var xmlDoc = new XmlDocument();
                using (var memoryStream = new MemoryStream())
                {
                    var serializer = new XmlSerializer(typeof(factura));
                    serializer.Serialize(memoryStream, xmlObj);
                    memoryStream.Position = 0;
                    xmlDoc.Load(memoryStream);
                }

                xmlDoc.DocumentElement.SetAttribute("id", "comprobante");

                certificadoService.CargarDesdeP12(
                    "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/signature.p12",
                    "Compus1234"
                );
                var xmlFirmado = certificadoService.FirmarDocumento(xmlDoc);

                var nombreArchivoXml = $"FACTURA_{factura.InfoTributaria.ClaveAcceso}.xml";
                xmlFirmado.Save(nombreArchivoXml);

                var envio = await webService.ValidarComprobanteAsync(xmlFirmado);
                Console.WriteLine($"ESTADO DE COMPROBANTE DE ENVIO: {System.Text.Json.JsonSerializer.Serialize(envio, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

                if (envio.Ok)
                {
                    System.Threading.Thread.Sleep(3000);
                    
                    var auto = await webService.AutorizacionComprobanteAsync(factura.InfoTributaria.ClaveAcceso);
                    var autorizacionData = auto.Data?.Autorizaciones?.Autorizacion?.FirstOrDefault();
                    Console.WriteLine($"ESTADO DE COMPROBANTE DE AUTORIZACION: {System.Text.Json.JsonSerializer.Serialize(auto, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

                    if (auto.Ok)
                    {
                        Console.WriteLine("AUTORIZADO");

                        if (autorizacionData != null)
                        {
                            factura.Autorizacion.Numero = autorizacionData.NumeroAutorizacion;
                            if (DateTimeOffset.TryParse(autorizacionData.FechaAutorizacion, out var fechaOffset))
                            {
                                factura.Autorizacion.Fecha = fechaOffset.ToOffset(TimeSpan.FromHours(-5)).DateTime;
                            }
                            else
                            {
                                throw new Exception($"Fecha de autorización inválida: {autorizacionData.FechaAutorizacion}");
                            }
                        }

                        var rutaPDF = $"/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/FACTURA_{factura.InfoTributaria.ClaveAcceso}.pdf";
                        rIDEService.Factura_1_0_0(factura, rutaPDF);

                        var respuestaUploadPDF = await _frappeUploader.UploadFileAsync(
                            rutaPDF,
                            Path.GetFileName(rutaPDF),
                            folder: "Home/Facturacion/PDF"
                        );
                        Console.WriteLine("📤 Archivo PDF subido a Frappe:");
                        Console.WriteLine(respuestaUploadPDF);

                        var rutaXML = $"/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/FACTURA_{factura.InfoTributaria.ClaveAcceso}.xml";
                        var respuestaUploadXML = await _frappeUploader.UploadFileAsync(
                            rutaXML,
                            Path.GetFileName(rutaXML),
                            folder: "Home/Facturacion/XML"
                        );
                        Console.WriteLine("📤 Archivo XML subido a Frappe:");
                        Console.WriteLine(respuestaUploadXML);

                        await FileCleanupHelper.DeleteFileAsync(rutaPDF);
                        await FileCleanupHelper.DeleteFileAsync(rutaXML);

                        var resultado = new
                        {
                            success = true,
                            claveAcceso = factura.InfoTributaria.ClaveAcceso,
                            mensaje = "Factura autorizada, PDF generado y archivos subidos a Frappe correctamente",
                            numeroAutorizacion = factura.Autorizacion.Numero,
                            fechaAutorizacion = factura.Autorizacion.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                            respuestaFrappePDF = respuestaUploadPDF,
                            respuestaFrappeXML = respuestaUploadXML
                        };

                        return Ok(resultado);
                    }
                    else
                    {
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