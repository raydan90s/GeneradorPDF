using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd;
using Yachasoft.Sri.Xsd.Map;

namespace Yachasoft.Sri.FacturacionElectronica. ollers
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

        [HttpGet("GenerarRetencion")]
        public async Task<IActionResult> GenerarRetencion()
        {
            try
            {
                // 1️⃣ Crear emisor, establecimiento y punto de emisión
                var emisor = new Emisor
                {
                    DireccionMatriz = "Casa",
                    EnumTipoAmbiente = Core.Enumerados.EnumTipoAmbiente.Prueba,
                    Logo = @"C:\Users\siste\Downloads\Logo_UTPL.png",
                    NombreComercial = "Yachasoft pruebas",
                    ObligadoContabilidad = false,
                    RazonSocial = "Sri pruebas",
                    RegimenMicroEmpresas = false,
                    RUC = "0992352434001",
                    ContribuyenteEspecial = null,
                    //AgenteRetencion = "1",
                };

                var establecimiento = new Establecimiento
                {
                    Codigo = 1,
                    DireccionEstablecimiento = "Mi casa",
                    Emisor = emisor
                };

                var puntoEmision = new PuntoEmision
                {
                    Codigo = 2,
                    Establecimiento = establecimiento
                };

                // 2️⃣ Crear comprobante de retención
                var retencion = new ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion
                {
                    PuntoEmision = puntoEmision,
                    FechaEmision = DateTime.Now,
                    InfoCompRetencion = new ComprobanteRetencion_1_0_0Modelo.InfoCompRetencion
                    {
                        PeriodoFiscal = "10/2025",
                    },
                    Sujeto = new Sujeto
                    {
                        Identificacion = "9999999999999",
                        RazonSocial = "Proveedor Prueba",
                        TipoIdentificador = EnumTipoIdentificacion.RUC
                    },
                    Impuestos = new List<ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion>
            {
                new ComprobanteRetencion_1_0_0Modelo.ImpuestoRenta
                {
                    BaseImponible = 100,
                    Tarifa = 1.75M,
                    Valor = Math.Round(100 * 1.75M / 100, 2),
                    CodigoRetencion = EnumTipoRetencionRenta.Actividadesdeconstruccióndeobramaterialinmuebleurbanizaciónlotizaciónoactividadessimilares,
                    DocumentoSustento = new DocumentoSustento
                    {
                        CodDocumento = EnumTipoDocumento.Factura,
                        NumDocumento = "001001000000001", // quitar los guiones
                        FechaEmisionDocumento = DateTime.Now
                    }
                }
            },
                    InfoAdicional = new List<CampoAdicional>
                    {
                new CampoAdicional { Nombre = "CorreoProveedor", Valor = "proveedor@test.com" },
                new CampoAdicional { Nombre = "Observacion", Valor = "Pago por servicios de octubre 2025" }
            }
                };

                // 3️⃣ Generar InfoTributaria y ClaveAcceso
                retencion.InfoTributaria = new InfoTributaria
                {
                    Secuencial = 4,
                    EnumTipoEmision = EnumTipoEmision.Normal
                };

                retencion.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(
                    retencion.TipoDocumento,
                    retencion.FechaEmision,
                    retencion.PuntoEmision,
                    retencion.InfoTributaria.Secuencial,
                    retencion.InfoTributaria.EnumTipoEmision
                );

                // 4️⃣ Mapear al XSD
                var comprobanteXml = ComprobanteRetencion_1_0_0Mapper.Map(retencion);

                // 5️⃣ Cargar certificado y firmar
                certificadoService.CargarDesdeP12(@"C:\Users\siste\Downloads\signature.p12", "Compus1234");
                var xmlFirmado = certificadoService.FirmarDocumento(comprobanteXml);

                // 6️⃣ Guardar XML firmado
                xmlFirmado.Save("COMPROBANTE_RETENCION_FIRMADO.xml");

                // 7️⃣ Validar con el SRI
                var envio = await webService.ValidarComprobanteAsync(xmlFirmado);
                if (envio.Ok)
                {
                    Console.WriteLine("RECIBIDA");
                    System.Threading.Thread.Sleep(3000);

                    // 8️⃣ Solicitar autorización
                    var auto = await webService.AutorizacionComprobanteAsync(retencion.InfoTributaria.ClaveAcceso);
                    if (auto.Ok)
                    {
                        Console.WriteLine(auto.Ok ? "AUTORIZADO" : auto.Error);
                        // ✅ Generar PDF RIDE de la retención
                        rIDEService.ComprobanteRetencion_1_0_0(retencion, @"C:\Users\siste\Desktop\RETENCION.pdf");
                        Console.WriteLine("PDF generado correctamente");
                        return Ok(auto);
                    }

                    return Ok(auto);
                }

                return Ok(envio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
