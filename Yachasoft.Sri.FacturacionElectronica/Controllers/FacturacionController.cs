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
using Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0;  // ⚠️ CAMBIO: XSD de FACTURA
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

        /// <summary>
        /// Genera, firma, envía y autoriza una FACTURA al SRI
        /// </summary>
        [HttpPost("GenerarFactura")]
        public async Task<IActionResult> GenerarFactura([FromBody] FacturaRequest request)
        {
            try
            {
                // 1️⃣ CREAR EMISOR (igual que liquidación)
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

                // 2️⃣ CREAR ESTABLECIMIENTO Y PUNTO EMISIÓN (igual)
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

                // 3️⃣ EXTRAER INFORMACIÓN ADICIONAL DEL CLIENTE
                string direccionCliente = null;
                string correoCliente = null;
                string telefonoCliente = null;

                if (request.InfoAdicional != null)
                {
                    var direccionAdicional = request.InfoAdicional.FirstOrDefault(ca => 
                        ca.Nombre.Equals("Direccion", StringComparison.OrdinalIgnoreCase) || 
                        ca.Nombre.Equals("Dirección", StringComparison.OrdinalIgnoreCase) ||
                        ca.Nombre.Equals("Direccion Cliente", StringComparison.OrdinalIgnoreCase));
                    direccionCliente = direccionAdicional?.Valor;

                    var correoAdicional = request.InfoAdicional.FirstOrDefault(ca => 
                        ca.Nombre.Equals("Email", StringComparison.OrdinalIgnoreCase) ||
                        ca.Nombre.Equals("Correo", StringComparison.OrdinalIgnoreCase) ||
                        ca.Nombre.Equals("CorreoCliente", StringComparison.OrdinalIgnoreCase));
                    correoCliente = correoAdicional?.Valor;

                    var telefonoAdicional = request.InfoAdicional.FirstOrDefault(ca => 
                        ca.Nombre.Equals("Telefono", StringComparison.OrdinalIgnoreCase) ||
                        ca.Nombre.Equals("Teléfono", StringComparison.OrdinalIgnoreCase) ||
                        ca.Nombre.Equals("TelefonoCliente", StringComparison.OrdinalIgnoreCase));
                    telefonoCliente = telefonoAdicional?.Valor;
                }

                // 4️⃣ MAPEAR DETALLES (igual)
                var detallesMapeados = MapperHelper.MapearDetalles(request.Detalles);

                // 5️⃣ ⚠️ CREAR FACTURA (PRINCIPAL DIFERENCIA)
                var factura = new Factura_1_0_0Modelo.Factura
                {
                    PuntoEmision = puntoEmision,
                    FechaEmision = request.FechaEmision,
                    
                    // ⚠️ DIFERENCIA: Factura usa "Sujeto" (que es el CLIENTE)
                    Sujeto = new Sujeto
                    {
                        Identificacion = request.Cliente.Identificacion,
                        RazonSocial = request.Cliente.RazonSocial,
                        TipoIdentificador = EnumParserHelper.ParseTipoIdentificacion(request.Cliente.TipoIdentificador)
                    },
                    
                    // ⚠️ DIFERENCIA: InfoFactura (no InfoLiquidacionCompra)
                    InfoFactura = new Factura_1_0_0Modelo.InfoFactura
                    {
                        TotalSinImpuestos = request.InfoFactura.TotalSinImpuestos,
                        TotalDescuento = request.InfoFactura.TotalDescuento,
                        ImporteTotal = request.InfoFactura.ImporteTotal,
                        
                        // ⚠️ CAMPOS ESPECÍFICOS DE FACTURA:
                        DireccionComprador = direccionCliente,
                        Propina = request.InfoFactura.Propina ?? 0.00m,
                        
                        // Mapeo de impuestos y pagos (igual)
                        TotalConImpuestos = MapperHelper.MapearImpuestosVentaDesdeDetalles(detallesMapeados),
                        Pagos = MapperHelper.MapearPagos(request.InfoFactura.Pagos)
                    },
                    
                    Detalles = detallesMapeados,
                    InfoAdicional = request.InfoAdicional
                };

                // 6️⃣ CREAR INFO TRIBUTARIA Y CLAVE DE ACCESO (igual)
                factura.InfoTributaria = new InfoTributaria
                {
                    Secuencial = request.Secuencial,
                    EnumTipoEmision = EnumParserHelper.ParseTipoEmision(request.EnumTipoEmision)
                };

                factura.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(
                    factura.TipoDocumento,  // ⚠️ Automáticamente es "01" para factura
                    factura.FechaEmision,
                    factura.PuntoEmision,
                    factura.InfoTributaria.Secuencial,
                    factura.InfoTributaria.EnumTipoEmision
                );

                // 7️⃣ ⚠️ MAPEAR A XSD DE FACTURA
                var xmlObj = Factura_1_0_0Mapper.Map(factura);

                // 8️⃣ SERIALIZAR A XML (igual)
                var xmlDoc = new XmlDocument();
                using (var memoryStream = new MemoryStream())
                {
                    var serializer = new XmlSerializer(typeof(factura));  // ⚠️ Tipo específico de factura
                    serializer.Serialize(memoryStream, xmlObj);
                    memoryStream.Position = 0;
                    xmlDoc.Load(memoryStream);
                }

                xmlDoc.DocumentElement.SetAttribute("id", "comprobante");

                // 9️⃣ FIRMAR DIGITALMENTE (igual)
                certificadoService.CargarDesdeP12(
                    "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/signature.p12",
                    "Compus1234"
                );
                var xmlFirmado = certificadoService.FirmarDocumento(xmlDoc);

                // 🔟 GUARDAR XML FIRMADO
                var nombreArchivoXml = $"FACTURA_{factura.InfoTributaria.ClaveAcceso}.xml";
                xmlFirmado.Save(nombreArchivoXml);

                // 1️⃣1️⃣ ENVIAR AL SRI (Recepción)
                var envio = await webService.ValidarComprobanteAsync(xmlFirmado);
                Console.WriteLine($"📤 ESTADO DE RECEPCIÓN: {System.Text.Json.JsonSerializer.Serialize(envio, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

                if (envio.Ok)
                {
                    // Esperar 3 segundos antes de consultar autorización
                    System.Threading.Thread.Sleep(3000);
                    
                    // 1️⃣2️⃣ SOLICITAR AUTORIZACIÓN
                    var auto = await webService.AutorizacionComprobanteAsync(factura.InfoTributaria.ClaveAcceso);
                    var autorizacionData = auto.Data?.Autorizaciones?.Autorizacion?.FirstOrDefault();
                    Console.WriteLine($"✅ ESTADO DE AUTORIZACIÓN: {System.Text.Json.JsonSerializer.Serialize(auto, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");

                    if (auto.Ok)
                    {
                        Console.WriteLine("✅ FACTURA AUTORIZADA");

                        // Asignar número y fecha de autorización
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

                        // 1️⃣3️⃣ ⚠️ GENERAR PDF RIDE (método específico de FACTURA)
                        var rutaPDF = $"/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/FACTURA_{factura.InfoTributaria.ClaveAcceso}.pdf";
                        rIDEService.Factura_1_0_0(factura, rutaPDF);  // ⚠️ Método de FACTURA
                        Console.WriteLine("📄 PDF de factura generado");

                        // 1️⃣4️⃣ SUBIR PDF A FRAPPE
                        var respuestaUploadPDF = await _frappeUploader.UploadFileAsync(
                            rutaPDF,
                            Path.GetFileName(rutaPDF),
                            folder: "Home/Facturacion/PDF"  // ⚠️ Carpeta específica de facturas
                        );
                        Console.WriteLine("📤 PDF subido a Frappe:");
                        Console.WriteLine(respuestaUploadPDF);

                        // 1️⃣5️⃣ SUBIR XML A FRAPPE
                        var rutaXML = $"/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/FACTURA_{factura.InfoTributaria.ClaveAcceso}.xml";
                        var respuestaUploadXML = await _frappeUploader.UploadFileAsync(
                            rutaXML,
                            Path.GetFileName(rutaXML),
                            folder: "Home/Facturacion/XML"  // ⚠️ Carpeta específica de facturas
                        );
                        Console.WriteLine("📤 XML subido a Frappe:");
                        Console.WriteLine(respuestaUploadXML);

                        // 1️⃣6️⃣ LIMPIAR ARCHIVOS TEMPORALES
                        await FileCleanupHelper.DeleteFileAsync(rutaPDF);
                        await FileCleanupHelper.DeleteFileAsync(rutaXML);

                        // 1️⃣7️⃣ RESPUESTA EXITOSA
                        var resultado = new
                        {
                            success = true,
                            mensaje = "Factura autorizada, PDF generado y archivos subidos a Frappe correctamente",
                            claveAcceso = factura.InfoTributaria.ClaveAcceso,
                            numero_autorizacion = factura.Autorizacion.Numero,
                            fecha_autorizacion = factura.Autorizacion.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                            estado_autorizacion = "AUTORIZADO",
                            estado_recepcion = envio.Data?.Estado,
                            respuestaFrappePDF = respuestaUploadPDF,
                            respuestaFrappeXML = respuestaUploadXML
                        };

                        return Ok(resultado);
                    }
                    else
                    {
                        // ❌ NO AUTORIZADA
                        var mensajesAutorizacion = autorizacionData?.Mensajes?.Mensaje?
                            .Select(m => new 
                            { 
                                tipo = m.Tipo,
                                identificador = m.Identificador, 
                                mensaje = m.Mensaje_, 
                                informacion_adicional = m.InformacionAdicional 
                            })
                            .ToList();

                        if (mensajesAutorizacion != null && mensajesAutorizacion.Count > 0)
                        {
                            Console.WriteLine("⚠️ MENSAJES DE AUTORIZACIÓN:");
                            foreach (var msg in mensajesAutorizacion)
                            {
                                Console.WriteLine($"- [{msg.tipo}] [{msg.identificador}] {msg.mensaje}");
                                if (!string.IsNullOrEmpty(msg.informacion_adicional))
                                    Console.WriteLine($"  Info adicional: {msg.informacion_adicional}");
                            }
                        }

                        return Ok(new
                        {
                            success = false,
                            mensaje = "Factura enviada pero no autorizada",
                            estado_autorizacion = autorizacionData?.Estado,
                            estado_recepcion = envio.Data?.Estado,
                            mensajes_recepcion = mensajesAutorizacion
                        });
                    }
                }
                else
                {
                    // ❌ ERROR EN RECEPCIÓN
                    var primerComprobante = envio.Data?.Comprobantes?.Comprobante?.FirstOrDefault();
                    var mensajesEnvio = primerComprobante?.Mensajes?.Mensaje
                        ?.Select(m => new 
                        { 
                            tipo = m.Tipo,
                            identificador = m.Identificador, 
                            mensaje = m.Mensaje_, 
                            informacion_adicional = m.InformacionAdicional 
                        })
                        .ToList();

                    if (mensajesEnvio != null && mensajesEnvio.Count > 0)
                    {
                        Console.WriteLine("⚠️ MENSAJES DE RECEPCIÓN:");
                        foreach (var msg in mensajesEnvio)
                        {
                            Console.WriteLine($"- [{msg.tipo}] [{msg.identificador}] {msg.mensaje}");
                            if (!string.IsNullOrEmpty(msg.informacion_adicional))
                                Console.WriteLine($"  Info adicional: {msg.informacion_adicional}");
                        }
                    }

                    return Ok(new
                    {
                        success = false,
                        mensaje = "Error al enviar la factura al SRI",
                        estado_recepcion = envio.Data?.Estado,
                        mensajes_recepcion = mensajesEnvio
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR CRÍTICO: {ex.Message}");
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