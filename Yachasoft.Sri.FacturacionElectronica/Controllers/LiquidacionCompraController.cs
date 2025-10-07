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

namespace Yachasoft.Sri.FacturacionElectronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiquidacionController : ControllerBase
    {
        private readonly Signer.ICertificadoService certificadoService;
        private readonly WebService.ISriWebService webService;
        private readonly Ride.IRIDEService rIDEService;

        public LiquidacionController(
            Signer.ICertificadoService certificadoService,
            WebService.ISriWebService webService,
            Ride.IRIDEService rIDEService)
        {
            this.certificadoService = certificadoService;
            this.webService = webService;
            this.rIDEService = rIDEService;
        }

        [HttpGet("GenerarLiquidacion")]
       public async Task<IActionResult> GenerarLiquidacion()
{
    try
    {
        // 1️⃣ Crear emisor, establecimiento y punto de emisión
        var emisor = new Emisor
        {
            DireccionMatriz = "Casa",
            EnumTipoAmbiente = EnumTipoAmbiente.Prueba,
            Logo = @"C:\Users\siste\Downloads\Logo_UTPL.png",
            NombreComercial = "Yachasoft pruebas",
            ObligadoContabilidad = false,
            RazonSocial = "Sri pruebas",
            RegimenMicroEmpresas = false,
            RUC = "0992352434001"
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

        // 2️⃣ Crear Liquidación de compra (datos de ejemplo)
        var liquidacion = new LiquidacionCompra_1_0_0Modelo.LiquidacionCompra
        {
            PuntoEmision = puntoEmision,
            FechaEmision = DateTime.Now,
            Sujeto = new Sujeto
            {
                Identificacion = "1799999999001",
                RazonSocial = "Proveedor Ejemplo",
                TipoIdentificador = EnumTipoIdentificacion.RUC
            },
            InfoLiquidacionCompra = new LiquidacionCompra_1_0_0Modelo.InfoLiquidacionCompra
            {
                TotalSinImpuestos = 100m,
                TotalDescuento = 0m,
                ImporteTotal = 112m,
                DireccionProveedor = "Av. Amazonas y NNUU, Quito",
                TotalConImpuestos = new List<ImpuestoVenta>
                {
                    new ImpuestoVentaIVA
                    {
                        BaseImponible = 100m,
                        Tarifa = 15m,
                        Valor = 15m,
                        CodigoPorcentaje = EnumTipoImpuestoIVA._15
                    }
                },
                Pagos = new List<Pago>
                {
                    new Pago
                    {
                        FormaPago = EnumFormaPago.SinUtilizarSistemaFinanciero,
                        Total = 112m,
                        Plazo = 30,
                        UnidadTiempo = "dias"
                    }
                }
            },
            Detalles = new List<DetalleDocumentoItemPrecio>
            {
                new DetalleDocumentoItemPrecio
                {
                    Item = new Item
                    {
                        CodigoPrincipal = "P001",
                        CodigoAuxiliar = "P001-EXT",
                        Descripcion = "Servicio de mantenimiento"
                    },
                    Cantidad = 1,
                    PrecioUnitario = 100m,
                    Descuento = 0m,
                    PrecioTotalSinImpuesto = 100m,
                    Impuestos = new List<Impuesto>
                    {
                        new ImpuestoIVA
                        {
                            BaseImponible = 100m,
                            Tarifa = 15m,
                            Valor = 15m,
                            CodigoPorcentaje = EnumTipoImpuestoIVA._15
                        }
                    },
                    DetallesAdicionales = new List<CampoAdicional>
                    {
                        new CampoAdicional { Nombre = "Detalle", Valor = "Servicio realizado en sitio" }
                    }
                }
            },
            InfoAdicional = new List<CampoAdicional>
            {
                new CampoAdicional { Nombre = "Email", Valor = "proveedor@ejemplo.com" }
            }
        };

        // 3️⃣ Crear InfoTributaria y clave de acceso
        liquidacion.InfoTributaria = new InfoTributaria
        {
            Secuencial = 1,
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

        // 2️⃣ Serializar a XmlDocument (para firmar)
        var xmlDoc = new XmlDocument();
        using (var memoryStream = new MemoryStream())
        {
            var serializer = new XmlSerializer(typeof(liquidacionCompra));
            serializer.Serialize(memoryStream, xmlObj);
            memoryStream.Position = 0;
            xmlDoc.Load(memoryStream);
        }

        // Forzar Id="comprobante" (por seguridad)
        xmlDoc.DocumentElement.SetAttribute("id", "comprobante");

        // 3️⃣ Guardar XML sin firmar para depuración
        xmlDoc.Save("LIQUIDACION_COMPRA_SIN_FIRMAR.xml");

        // 4️⃣ Firmar
        certificadoService.CargarDesdeP12(@"C:\Users\siste\Downloads\signature.p12", "Compus1234");
        var xmlFirmado = certificadoService.FirmarDocumento(xmlDoc);

        // 5️⃣ Guardar XML firmado
        xmlFirmado.Save("LIQUIDACION_COMPRA_FIRMADO.xml");

        // 6️⃣ Convertir a string para enviar al SRI
        string xmlFirmadoStr = xmlFirmado.OuterXml;
        Console.WriteLine(xmlFirmadoStr.Substring(0, Math.Min(500, xmlFirmadoStr.Length)));

        // 6️⃣ Enviar al SRI
        var envio = await webService.ValidarComprobanteAsync(xmlFirmadoStr);

        // 🔹 Debug de envío
        Console.WriteLine("✅ Respuesta SRI: " + Newtonsoft.Json.JsonConvert.SerializeObject(envio));

        if (!envio.Ok)
        {
            return BadRequest(new
            {
                mensaje = "Error al enviar la liquidación al SRI",
                detalle = envio
            });
        }

        // 7️⃣ Solicitar autorización
        var auto = await webService.AutorizacionComprobanteAsync(liquidacion.InfoTributaria.ClaveAcceso);

        // 🔹 Debug de autorización
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

        // 8️⃣ Generar PDF RIDE
        rIDEService.LiquidacionCompra_1_0_0(
            liquidacion,
            @"C:\Users\siste\Desktop\LIQUIDACION.pdf"
        );
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
        // 🔹 Guardar log detallado
        Console.WriteLine("❌ ERROR: " + ex.Message);
        Console.WriteLine(ex.StackTrace);

        return BadRequest(new
        {
            error = ex.Message,
            stack = ex.StackTrace
        });
    }
}


    }
}
