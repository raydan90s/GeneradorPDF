using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
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
                // 1️⃣ EMISOR
                var emisor = new Emisor
                {
                    DireccionMatriz = request.Emisor.DireccionMatriz,
                    DireccionEstablecimiento = request.Emisor.DireccionEstablecimiento,
                    EnumTipoAmbiente = EnumParserHelper.ParseTipoAmbiente(request.Emisor.EnumTipoAmbiente),
                    NombreComercial = request.Emisor.NombreComercial,
                    ObligadoContabilidad = request.Emisor.ObligadoContabilidad.HasValue ? (request.Emisor.ObligadoContabilidad.Value ? "SI" : "NO") : null,
                    RazonSocial = request.Emisor.RazonSocial,
                    RegimenMicroEmpresas = request.Emisor.RegimenMicroEmpresas.HasValue ? (request.Emisor.RegimenMicroEmpresas.Value ? "SI" : "NO") : null,
                    RUC = request.Emisor.RUC,
                    ContribuyenteEspecial = request.Emisor.ContribuyenteEspecial,
                    AgenteRetencion = request.Emisor.AgenteRetencion.HasValue ? (request.Emisor.AgenteRetencion.Value ? "SI" : "NO") : null
                };

                // 2️⃣ ESTABLECIMIENTO
                var establecimiento = new Establecimiento
                {
                    Codigo = request.CodigoEstablecimiento,
                    DireccionEstablecimiento = request.Emisor.DireccionEstablecimiento,
                    Emisor = emisor
                };

                // 3️⃣ PUNTO DE EMISIÓN
                var puntoEmision = new PuntoEmision
                {
                    Codigo = request.CodigoPuntoEmision,
                    Establecimiento = establecimiento
                };

                // 4️⃣ DOCUMENTO MODIFICADO
                var documentoModificado = new DocumentoSustento
                {
                    CodDocumento = EnumParserHelper.ParseTipoDocumento(request.DocumentoModificado.CodDocumento),
                    NumDocumento = request.DocumentoModificado.NumDocumento,
                    FechaEmisionDocumento = request.DocumentoModificado.FechaEmisionDocumento
                };

                // 5️⃣ IMPUESTOS TOTALES
                var impuestosTotales = MapperHelper.MapearImpuestosVenta(request.InfoNotaCredito.Impuestos);

                // 6️⃣ MOTIVOS
                var motivos = request.Motivos?.Select(m => new Motivo
                {
                    Razon = m.Razon,
                    Valor = m.Valor
                }).ToList() ?? new List<Motivo>();

                // 7️⃣ NOTA DE CRÉDITO
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
                        Motivo = string.Join(", ", motivos.Select(m => m.Razon))
                    },
                    InfoAdicional = request.InfoAdicional,
                    Motivos = motivos
                };

                // 8️⃣ INFO TRIBUTARIA
                notaCredito.InfoTributaria = new InfoTributaria
                {
                    Secuencial = request.Secuencial,
                    EnumTipoEmision = EnumParserHelper.ParseTipoEmision(request.EnumTipoEmision)
                };

                // 9️⃣ CLAVE DE ACCESO
                notaCredito.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(
                    NotaCredito_1_0_0Modelo.TipoDocumento,
                    notaCredito.FechaEmision,
                    notaCredito.PuntoEmision,
                    notaCredito.InfoTributaria.Secuencial,
                    notaCredito.InfoTributaria.EnumTipoEmision
                );

                // 🔟 SERIALIZAR Y FIRMAR XML
                var xmlObj = NotaCredito_1_0_0Mapper.Map(notaCredito);
                var xmlDoc = new XmlDocument();
                using (var ms = new MemoryStream())
                {
                    var serializer = new XmlSerializer(typeof(notaCredito));
                    serializer.Serialize(ms, xmlObj);
                    ms.Position = 0;
                    xmlDoc.Load(ms);
                }
                xmlDoc.DocumentElement.SetAttribute("id", "comprobante");

                certificadoService.CargarDesdeP12(
                    "C:\\Users\\Sistemas\\Documents\\GeneradorPDF\\Yachasoft.Sri.FacturacionElectronica\\signature.p12",
                    "Compus1234"
                );
                var xmlFirmado = certificadoService.FirmarDocumento(xmlDoc);

                var nombreXml = $"NOTACREDITO_{notaCredito.InfoTributaria.ClaveAcceso}.xml";
                xmlFirmado.Save(nombreXml);

                // 1️⃣1️⃣ VALIDAR Y AUTORIZAR
                var envio = await webService.ValidarComprobanteAsync(xmlFirmado);
                if (!envio.Ok) return BadRequest(new { success = false, estado = envio.Data?.Estado });

                System.Threading.Thread.Sleep(3000);
                var auto = await webService.AutorizacionComprobanteAsync(notaCredito.InfoTributaria.ClaveAcceso);
                var autorizacionData = auto.Data?.Autorizaciones?.Autorizacion?.FirstOrDefault();
                if (!auto.Ok) return BadRequest(new { success = false, estado = autorizacionData?.Estado });

                if (autorizacionData != null)
                {
                    notaCredito.Autorizacion.Numero = autorizacionData.NumeroAutorizacion;
                    if (DateTimeOffset.TryParse(autorizacionData.FechaAutorizacion, out var fechaOffset))
                        notaCredito.Autorizacion.Fecha = fechaOffset.ToOffset(TimeSpan.FromHours(-5)).DateTime;
                }

                // 1️⃣2️⃣ GENERAR PDF Y SUBIR A FRAPPE
                var rutaPDF = $"C:\\Users\\Sistemas\\Documents\\GeneradorPDF\\Yachasoft.Sri.FacturacionElectronica\\NOTACREDITO_{notaCredito.InfoTributaria.ClaveAcceso}.pdf";
                rIDEService.NotaCredito_1_0_0(notaCredito, rutaPDF);

                var respPDF = await _frappeUploader.UploadFileAsync(rutaPDF, Path.GetFileName(rutaPDF), folder: "Home/NotaCredito/PDF");
                var respXML = await _frappeUploader.UploadFileAsync(nombreXml, Path.GetFileName(nombreXml), folder: "Home/NotaCredito/XML");

                await FileCleanupHelper.DeleteFileAsync(rutaPDF);
                await FileCleanupHelper.DeleteFileAsync(nombreXml);

                return Ok(new
                {
                    success = true,
                    claveAcceso = notaCredito.InfoTributaria.ClaveAcceso,
                    numeroAutorizacion = notaCredito.Autorizacion.Numero,
                    fechaAutorizacion = notaCredito.Autorizacion.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                    respuestaFrappePDF = respPDF,
                    respuestaFrappeXML = respXML
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
    }
}
