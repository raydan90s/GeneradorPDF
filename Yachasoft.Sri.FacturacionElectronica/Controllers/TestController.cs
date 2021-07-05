using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yachasoft.Sri.Xsd;
using Yachasoft.Sri.Xsd.Map;

namespace Yachasoft.Sri.FacturacionElectronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private Signer.ICertificadoService certificadoService;
        private WebService.ISriWebService webService;
            private Ride.IRIDEService rIDEService;
        public TestController(
            Signer.ICertificadoService certificadoService, 
            WebService.ISriWebService webService, 
            Ride.IRIDEService rIDEService)
        {
            this.certificadoService = certificadoService;
            this.webService = webService;
            this.rIDEService = rIDEService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var emisor = new Modelos.Emisor
            {
                //AgenteRetencion = "", //Llenar solo cuando tenga nro de agente de retencion
                //ContribuyenteEspecial = "", //llenar solo cuando se tenga nro de contribuyente especial
                DireccionMatriz = "Casa",
                EnumTipoAmbiente = Core.Enumerados.EnumTipoAmbiente.Prueba,
                Logo = @"C:\Users\Joseph\Desktop\Logos\logo_yachasoft_2021.png",
                NombreComercial = "Yachasoft pruebas",
                ObligadoContabilidad = false,
                RazonSocial = "Sri pruebas",
                RegimenMicroEmpresas = true,
                RUC = "0918859018001"
            };
            var establecimiento = new Modelos.Establecimiento
            {
                Codigo = 1,
                DireccionEstablecimiento = "Mi casa",
                Emisor = emisor
            };
            var puntoEmision = new Modelos.PuntoEmision
            {
                Codigo = 2,
                Establecimiento = establecimiento
            };
            var autorizacion = new Modelos.Base.Autorizacion
            {
                Fecha = DateTime.Now
            };
            var infoTributaria = new Modelos.Base.InfoTributaria
            {
                //ClaveAcceso = Utils.GenerarClaveAcceso(),
                EnumTipoEmision = Core.Enumerados.EnumTipoEmision.Normal,
                Secuencial = 3
            };
            var pagos = new List<Modelos.Base.Pago>
            {
                new Modelos.Base.Pago { FormaPago = Modelos.Enumerados.EnumFormaPago.SinUtilizarSistemaFinanciero, Plazo = 0, Total = 6, UnidadTiempo = "Dias" }
            };
            var infoFactura = new Modelos.Factura_1_0_0Modelo.InfoFactura
            {
                DireccionComprador = "Direccion cliente",
                //GuiaRemision = "", //llenar solo cuando se tenga numero de guia
                ImporteTotal = 6,
                Moneda = "Dolares",
                Pagos = pagos,
                Placa = "Placa",
                Propina = 0,
                TotalConImpuestos =  new List<Modelos.Base.ImpuestoVenta> 
                { 
                    new Modelos.Base.ImpuestoVentaIVA
                    {
                        Codigo = Modelos.Enumerados.EnumTipoImpuesto.IVA,
                        CodigoPorcentaje = Modelos.Enumerados.EnumTipoImpuestoIVA._0,
                        BaseImponible = 6, Tarifa = 0, Valor = 6
                    } 
                },
                TotalDescuento = 0,
                TotalSinImpuestos = 6,
                TotalSubsidio = 0
            };
            var infoAdicional = new List<Modelos.Base.CampoAdicional>
            {
                new Modelos.Base.CampoAdicional { Nombre = "correo", Valor = "test@test.com" }
            };
            //var sujeto = new Modelos.Base.Sujeto
            //{
            //    Identificacion = "9999999999999",
            //    RazonSocial = "CONSUMIDOR FINAL",
            //    TipoIdentificador = Core.Enumerados.EnumTipoIdentificacion.VentaConsumidorFinal
            //};

            var detalles = new List<Modelos.Base.DetalleDocumentoItemPrecioSubsidio>
            {
                new Modelos.Base.DetalleDocumentoItemPrecioSubsidio
                {
                    Cantidad = 1,
                    Descuento = 0,
                    DetallesAdicionales = new List<Modelos.Base.CampoAdicional>{new Modelos.Base.CampoAdicional { Nombre = "Unidad", Valor = "Und"} },
                    Impuestos = new List<Modelos.Base.Impuesto>{
                new Modelos.Base.ImpuestoIVA
                    {
                        Codigo = Modelos.Enumerados.EnumTipoImpuesto.IVA,
                        CodigoPorcentaje = Modelos.Enumerados.EnumTipoImpuestoIVA._0,
                        BaseImponible = 6, Tarifa = 0, Valor = 6
                    }
                },
                    Item = new Modelos.Base.Item{ CodigoAuxiliar = "P001A", CodigoPrincipal = "P001", Descripcion = "Producto 1"},
                    PrecioUnitario = 6,
                    PrecioTotalSinImpuesto = 6
                }
            };
            var f = new Modelos.Factura_1_0_0Modelo.Factura
            {
                PuntoEmision = puntoEmision,
                Autorizacion = autorizacion,
                FechaEmision = DateTime.Now,
                InfoTributaria = infoTributaria,
                InfoAdicional = infoAdicional,
                InfoFactura = infoFactura,
                Sujeto = Modelos.Factura_1_0_0Modelo.ConsumidorFinal,
                Detalles = detalles,
            };
            f.InfoTributaria.ClaveAcceso = Utils.GenerarClaveAcceso(f.TipoDocumento, f.FechaEmision, f.PuntoEmision, f.InfoTributaria.Secuencial, f.InfoTributaria.EnumTipoEmision);
            this.certificadoService.CargarDesdeP12(@"D:\MyEsign2020\luis_fernando_pinduisaca_acosta.p12", "Joseph.8410");
            var fact = Factura_1_0_0Mapper.Map(f);
            var xml = this.certificadoService.FirmarDocumento(fact);
            xml.Save("FACTURA_FIRMADA.xml");
            this.rIDEService.Factura_1_0_0(f, "FACTURA.pdf");
            var envio = await this.webService.ValidarComprobanteAsync(xml);
            if (envio.Ok)
            {
                Console.WriteLine(envio.Ok ? "RECIBIDA" : envio.Error);
                System.Threading.Thread.Sleep(3000);
                var auto = await this.webService.AutorizacionComprobanteAsync(f.InfoTributaria.ClaveAcceso);
                Console.WriteLine(auto.Ok ? "AUTORIZADO" : auto.Error);
                return Ok(auto);
            }            
            return Ok(envio);
            //return Ok(this.rIDEService.Factura_1_0_0(f, "FACTURA.pdf"));
        }
    }
}
