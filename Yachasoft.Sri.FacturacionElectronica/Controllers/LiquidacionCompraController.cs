using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Sri.Xsd;
using Yachasoft.Sri.Xsd.Contratos.LiquidacionCompra_1_0_0;
using Yachasoft.Sri.Xsd.Map;
using Yachasoft.Sri.FacturacionElectronica.DTOs;

namespace Yachasoft.Sri.FacturacionElectronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiquidacionController : ControllerBase
    {
        private readonly Signer.ICertificadoService certificadoService;
        private readonly WebService.ISriWebService webService;
        private readonly Ride.IRIDEService rIDEService;

        // ⚙️ Configuración hardcoded
        private const string LOGO_PATH = "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/Logo_UTPL.png";
        private const string PDF_OUTPUT_PATH = "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/LIQUIDACION.pdf";
        private const string CERTIFICADO_PATH = "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/signature.p12";
        private const string CERTIFICADO_PASSWORD = "Compus1234";

        public LiquidacionController(
            Signer.ICertificadoService certificadoService,
            WebService.ISriWebService webService,
            Ride.IRIDEService rIDEService)
        {
            this.certificadoService = certificadoService;
            this.webService = webService;
            this.rIDEService = rIDEService;
        }

        /// <summary>
        /// Genera, firma y envía una liquidación de compra al SRI con datos dinámicos (POST)
        /// </summary>
        /// <param name="request">Datos de la liquidación en formato JSON</param>
        /// <returns>Respuesta del SRI con la clave de acceso</returns>
        [HttpPost("Generar")]
        public async Task<IActionResult> GenerarLiquidacionDinamica([FromBody] LiquidacionRequestDto request)
        {
            try
            {
                // 🔍 Validar request
                if (request == null)
                {
                    return BadRequest(new { mensaje = "El cuerpo de la solicitud no puede estar vacío" });
                }

                // 1️⃣ Crear emisor, establecimiento y punto de emisión
                var emisor = new Emisor
                {
                    DireccionMatriz = request.Emisor.DireccionMatriz,
                    EnumTipoAmbiente = request.Emisor.EsAmbientePrueba ? EnumTipoAmbiente.Prueba : EnumTipoAmbiente.Produccion,
                    Logo = LOGO_PATH,
                    NombreComercial = request.Emisor.NombreComercial,
                    ObligadoContabilidad = request.Emisor.ObligadoContabilidad,
                    RazonSocial = request.Emisor.RazonSocial,
                    RegimenMicroEmpresas = request.Emisor.RegimenMicroEmpresas,
                    RUC = request.Emisor.RUC
                };

                var establecimiento = new Establecimiento
                {
                    Codigo = request.Establecimiento.Codigo,
                    DireccionEstablecimiento = request.Establecimiento.Direccion,
                    Emisor = emisor
                };

                var puntoEmision = new PuntoEmision
                {
                    Codigo = request.PuntoEmision.Codigo,
                    Establecimiento = establecimiento
                };

                // 2️⃣ Crear Liquidación de compra
                var liquidacion = new LiquidacionCompra_1_0_0Modelo.LiquidacionCompra
                {
                    PuntoEmision = puntoEmision,
                    FechaEmision = request.FechaEmision,
                    Sujeto = new Sujeto
                    {
                        Identificacion = request.Proveedor.Identificacion,
                        RazonSocial = request.Proveedor.RazonSocial,
                        TipoIdentificador = (EnumTipoIdentificacion)request.Proveedor.TipoIdentificacion
                    },
                    InfoLiquidacionCompra = new LiquidacionCompra_1_0_0Modelo.InfoLiquidacionCompra
                    {
                        TotalSinImpuestos = request.InfoLiquidacion.TotalSinImpuestos,
                        TotalDescuento = request.InfoLiquidacion.TotalDescuento,
                        ImporteTotal = request.InfoLiquidacion.ImporteTotal,
                        DireccionProveedor = request.Proveedor.Direccion,
                        TotalConImpuestos = MapearImpuestosVenta(request.InfoLiquidacion.TotalConImpuestos),
                        Pagos = MapearPagos(request.InfoLiquidacion.Pagos)
                    },
                    Detalles = MapearDetalles(request.Detalles),
                    InfoAdicional = MapearCamposAdicionales(request.InfoAdicional)
                };

                // 3️⃣ Crear InfoTributaria y clave de acceso
                liquidacion.InfoTributaria = new InfoTributaria
                {
                    Secuencial = request.Secuencial,
                    EnumTipoEmision = EnumTipoEmision.Normal
                };

                liquidacion.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(
                    liquidacion.TipoDocumento,
                    liquidacion.FechaEmision,
                    liquidacion.PuntoEmision,
                    liquidacion.InfoTributaria.Secuencial,
                    liquidacion.InfoTributaria.EnumTipoEmision
                );

                // 4️⃣ Mapear al XSD
                var xmlObj = LiquidacionCompra_1_0_0Mapper.Map(liquidacion);

                // 5️⃣ Serializar a XmlDocument (para firmar)
                var xmlDoc = new XmlDocument();
                using (var memoryStream = new MemoryStream())
                {
                    var serializer = new XmlSerializer(typeof(liquidacionCompra));
                    serializer.Serialize(memoryStream, xmlObj);
                    memoryStream.Position = 0;
                    xmlDoc.Load(memoryStream);
                }

                // Forzar Id="comprobante"
                xmlDoc.DocumentElement.SetAttribute("id", "comprobante");

                // 6️⃣ Guardar XML sin firmar (opcional para debug)
                xmlDoc.Save("LIQUIDACION_COMPRA_SIN_FIRMAR.xml");

                // 7️⃣ Firmar
                certificadoService.CargarDesdeP12(CERTIFICADO_PATH, CERTIFICADO_PASSWORD);
                var xmlFirmado = certificadoService.FirmarDocumento(xmlDoc);

                // 8️⃣ Guardar XML firmado
                xmlFirmado.Save("LIQUIDACION_COMPRA_FIRMADO.xml");

                // 9️⃣ Convertir a string para enviar al SRI
                string xmlFirmadoStr = xmlFirmado.OuterXml;
                Console.WriteLine(xmlFirmadoStr.Substring(0, Math.Min(500, xmlFirmadoStr.Length)));

                // 🔟 Enviar al SRI
                var envio = await webService.ValidarComprobanteAsync(xmlFirmadoStr);
                Console.WriteLine("✅ Respuesta SRI: " + Newtonsoft.Json.JsonConvert.SerializeObject(envio));

                if (!envio.Ok)
                {
                    return BadRequest(new
                    {
                        mensaje = "Error al enviar la liquidación al SRI",
                        detalle = envio
                    });
                }

                // 1️⃣1️⃣ Solicitar autorización
                var auto = await webService.AutorizacionComprobanteAsync(liquidacion.InfoTributaria.ClaveAcceso);
                Console.WriteLine("✅ Respuesta de autorización SRI: " + Newtonsoft.Json.JsonConvert.SerializeObject(auto));

                if (!auto.Ok)
                {
                    return Ok(new
                    {
                        mensaje = "Liquidación enviada pero no autorizada",
                        claveAcceso = liquidacion.InfoTributaria.ClaveAcceso,
                        respuestaSri = auto
                    });
                }

                // 1️⃣2️⃣ Generar PDF RIDE
                rIDEService.LiquidacionCompra_1_0_0(liquidacion, PDF_OUTPUT_PATH);
                Console.WriteLine("📄 PDF generado correctamente");

                return Ok(new
                {
                    mensaje = "Liquidación autorizada correctamente",
                    claveAcceso = liquidacion.InfoTributaria.ClaveAcceso,
                    respuestaSri = auto
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR: " + ex.Message);
                Console.WriteLine(ex.StackTrace);

                return BadRequest(new
                {
                    error = ex.Message,
                    stack = ex.StackTrace
                });
            }
        }

        #region Métodos de mapeo privados

        private List<ImpuestoVenta> MapearImpuestosVenta(List<ImpuestoVentaDto> impuestosDto)
        {
            var impuestos = new List<ImpuestoVenta>();
            foreach (var imp in impuestosDto)
            {
                // Mapeo seguro de código a enum
                EnumTipoImpuestoIVA codigoPorcentaje = ObtenerCodigoIVA(imp.CodigoPorcentaje);

                impuestos.Add(new ImpuestoVentaIVA
                {
                    BaseImponible = imp.BaseImponible,
                    Tarifa = imp.Tarifa,
                    Valor = imp.Valor,
                    CodigoPorcentaje = codigoPorcentaje
                });
            }
            return impuestos;
        }

        private List<Pago> MapearPagos(List<PagoDto> pagosDto)
        {
            var pagos = new List<Pago>();
            foreach (var pago in pagosDto)
            {
                pagos.Add(new Pago
                {
                    FormaPago = (EnumFormaPago)pago.FormaPago,
                    Total = pago.Total,
                    Plazo = pago.Plazo,
                    UnidadTiempo = pago.UnidadTiempo
                });
            }
            return pagos;
        }

        private List<DetalleDocumentoItemPrecio> MapearDetalles(List<DetalleDto> detallesDto)
        {
            var detalles = new List<DetalleDocumentoItemPrecio>();
            foreach (var det in detallesDto)
            {
                var detalle = new DetalleDocumentoItemPrecio
                {
                    Item = new Item
                    {
                        CodigoPrincipal = det.CodigoPrincipal,
                        CodigoAuxiliar = det.CodigoAuxiliar,
                        Descripcion = det.Descripcion
                    },
                    Cantidad = (int)det.Cantidad,
                    PrecioUnitario = det.PrecioUnitario,
                    Descuento = det.Descuento,
                    PrecioTotalSinImpuesto = det.PrecioTotalSinImpuesto,
                    Impuestos = MapearImpuestosDetalle(det.Impuestos),
                    DetallesAdicionales = MapearCamposAdicionales(det.DetallesAdicionales)
                };
                detalles.Add(detalle);
            }
            return detalles;
        }

        private List<Impuesto> MapearImpuestosDetalle(List<ImpuestoDto> impuestosDto)
        {
            var impuestos = new List<Impuesto>();
            foreach (var imp in impuestosDto)
            {
                // Mapeo seguro de código a enum
                EnumTipoImpuestoIVA codigoPorcentaje = ObtenerCodigoIVA(imp.CodigoPorcentaje);

                impuestos.Add(new ImpuestoIVA
                {
                    BaseImponible = imp.BaseImponible,
                    Tarifa = imp.Tarifa,
                    Valor = imp.Valor,
                    CodigoPorcentaje = codigoPorcentaje
                });
            }
            return impuestos;
        }

        private List<CampoAdicional> MapearCamposAdicionales(List<CampoAdicionalDto> camposDto)
        {
            if (camposDto == null) return new List<CampoAdicional>();

            var campos = new List<CampoAdicional>();
            foreach (var campo in camposDto)
            {
                campos.Add(new CampoAdicional
                {
                    Nombre = campo.Nombre,
                    Valor = campo.Valor
                });
            }
            return campos;
        }

        /// <summary>
        /// Mapea el código numérico del JSON al enum EnumTipoImpuestoIVA
        /// </summary>
        private EnumTipoImpuestoIVA ObtenerCodigoIVA(int codigo)
        {
            return codigo switch
            {
                0 => EnumTipoImpuestoIVA._0,                 // IVA 0%
                2 => EnumTipoImpuestoIVA._12,                // IVA 12%
                3 => EnumTipoImpuestoIVA._14,                // IVA 14%
                4 => EnumTipoImpuestoIVA._15,                // IVA 15%
                6 => EnumTipoImpuestoIVA.NoObjetoImpuesto,   // No objeto de impuesto
                7 => EnumTipoImpuestoIVA.ExentoIVA,          // Exento de IVA
                _ => throw new ArgumentException($"Código de IVA '{codigo}' no es válido. Códigos válidos: 0, 2, 3, 4, 6, 7")
            };
        }

        #endregion
    }
}