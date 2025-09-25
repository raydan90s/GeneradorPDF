using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yachasoft.Sri.Xsd;
using Yachasoft.Sri.Xsd.Map;
using Yachasoft.Sri.FacturacionElectronica.Models.Request;
using Newtonsoft.Json;
using System.IO;
using System.Globalization;
using Yachasoft.Sri.Modelos.Base;
using Yachasoft.Sri.Modelos.Enumerados;

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
        public async Task<IActionResult> Get(){
            var emisor = new Modelos.Emisor
            {
                //AgenteRetencion = "", //Llenar solo cuando tenga nro de agente de retencion
                //ContribuyenteEspecial = "", //llenar solo cuando se tenga nro de contribuyente especial
                DireccionMatriz = "Casa",
                EnumTipoAmbiente = Core.Enumerados.EnumTipoAmbiente.Prueba,
                Logo = @"C:\Users\siste\Downloads\Logo_UTPL.png",
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
                TotalConImpuestos = new List<Modelos.Base.ImpuestoVenta>
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
            this.certificadoService.CargarDesdeP12(@"C:\Users\siste\Downloads\signature.p12", "Compus1234");
            var fact = Factura_1_0_0Mapper.Map(f);
            var xml = this.certificadoService.FirmarDocumento(fact);
            xml.Save("FACTURA_FIRMADA.xml");
            this.rIDEService.Factura_1_0_0(f, @"C:\Users\siste\Desktop\FACTURA.pdf");
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

        private Modelos.Enumerados.EnumFormaPago ObtenerFormaPago(string codigo){
            foreach (Modelos.Enumerados.EnumFormaPago fp in Enum.GetValues(typeof(Modelos.Enumerados.EnumFormaPago)))
            {
                var attr = fp.GetType()
                             .GetField(fp.ToString())
                             .GetCustomAttributes(typeof(Yachasoft.Sri.Core.Atributos.SRICodigoAttribute), false)
                             .FirstOrDefault() as Yachasoft.Sri.Core.Atributos.SRICodigoAttribute;

                if (attr != null && attr.Code == codigo) // ✅ usar Code
                    return fp;
            }

            // Valor por defecto si no coincide
            return Modelos.Enumerados.EnumFormaPago.SinUtilizarSistemaFinanciero;
        }


        //GENERAR PDF DESDE XML
[HttpPost("GenerarPdfDesdeJson")]
public IActionResult GenerarPdfDesdeJson([FromBody] FacturaRequest request)
{
    if (request == null)
        return BadRequest(new { error = "Request body vacío" });
        
    Modelos.Factura_1_0_0Modelo.Factura factura;
    
    try
    {
        factura = MapFacturaRequestToFactura(request);

        //AQUI DEBO PASARLE LA AUTORIZACION
        if (factura.Autorizacion == null)
            factura.Autorizacion = new Modelos.Base.Autorizacion();
        factura.Autorizacion.Numero ??= "SIN AUTORIZACION";

        // Imprimir factura para debug
        Console.WriteLine("Factura mapeada: " +
            System.Text.Json.JsonSerializer.Serialize(factura, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
    }
    catch (Exception ex)
    {
        return BadRequest(new { error = "Error al mapear factura: " + ex.Message });
    }
    // Usar el AccessKey del documento como nombre de archivo
    var accessKey = request.DocumentInfo?.AccessKey ?? "SINCLAVE";
    var pdfFileName = $"Factura_{accessKey}.pdf"; // O solo "Factura.pdf"
    var pdfPath = Path.Combine("/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica", pdfFileName);


    try
    {        
        // Verificar permisos del directorio
        var directory = Path.GetDirectoryName(pdfPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        // Verificar permisos de escritura
        var testFile = Path.Combine(directory, "test_write.tmp");
        System.IO.File.WriteAllText(testFile, "test");
        System.IO.File.Delete(testFile);
        
        this.rIDEService.Factura_1_0_0(factura, pdfPath);
        
        return Ok(new { message = "PDF generado correctamente", pdfPath });
    }
    catch (UnauthorizedAccessException ex)
    {
        return BadRequest(new { error = "Error de permisos: " + ex.Message });
    }
    catch (DirectoryNotFoundException ex)
    {
        return BadRequest(new { error = "Directorio no encontrado: " + ex.Message });
    }
    catch (Exception ex)
    {
        return BadRequest(new { error = "Error al generar PDF: " + ex.Message });
    }
}

// Mapper JSON -> Factura (sin firmar)
private Modelos.Factura_1_0_0Modelo.Factura MapFacturaRequestToFactura(FacturaRequest request)
{
    try
    {
        // --- Emisor ---
        var logoPath = "/home/bitnami/GeneradorPDF/Yachasoft.Sri.FacturacionElectronica/Logo_UTPL.png";
        
        var emisor = new Modelos.Emisor
        {
            RUC = request.DocumentInfo?.RucBusiness,
            RazonSocial = request.DocumentInfo?.BusinessName,
            NombreComercial = request.DocumentInfo?.CommercialName,
            DireccionMatriz = request.DocumentInfo?.BusinessAddress,
            EnumTipoAmbiente = request.DocumentInfo?.Environment == "1"
                ? Core.Enumerados.EnumTipoAmbiente.Prueba
                : Core.Enumerados.EnumTipoAmbiente.Produccion,
            ObligadoContabilidad = request.DocumentInfo?.ObligatedAccounting?.ToUpper() == "SI",
            Logo = logoPath
        };
        
        // Verificar que el logo existe
        if (!System.IO.File.Exists(logoPath))
        {
            Console.WriteLine("⚠️ Logo no encontrado en: " + logoPath);
            // Usa un logo por defecto o continúa sin logo
            emisor.Logo = null;
        }
        
        // --- Establecimiento ---
        var establecimiento = new Modelos.Establecimiento
        {
            Codigo = int.Parse(request.DocumentInfo?.Establishment ?? throw new Exception("Establishment no puede ser nulo")),
            DireccionEstablecimiento = request.DocumentInfo?.EstablishmentAddress ?? throw new Exception("DireccionEstablecimiento no puede ser nula"),
            Emisor = emisor
        };

        // --- Punto de Emisión ---
        var puntoEmision = new Modelos.PuntoEmision
        {
            Codigo = int.Parse(request.DocumentInfo?.EmissionPoint ?? throw new Exception("EmissionPoint no puede ser nulo")),
            Establecimiento = establecimiento
        };

        // --- Info Tributaria ---
        var infoTributaria = new Modelos.Base.InfoTributaria
        {
            EnumTipoEmision = Core.Enumerados.EnumTipoEmision.Normal,
            Secuencial = int.Parse(request.DocumentInfo?.Sequential ?? throw new Exception("Sequential no puede ser nulo")),
            ClaveAcceso = string.IsNullOrEmpty(request.DocumentInfo?.AccessKey)
                ? throw new Exception("AccessKey no puede ser nulo o vacío")
                : request.DocumentInfo.AccessKey
        };

        // --- Detalles ---
        var detalles = request.Details?.Select(d => new Modelos.Base.DetalleDocumentoItemPrecioSubsidio
        {
            Cantidad = d.Quantity,
            PrecioUnitario = decimal.TryParse(d.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out var p) ? p : 0,
            PrecioTotalSinImpuesto = decimal.TryParse(d.SubTotal, NumberStyles.Any, CultureInfo.InvariantCulture, out var st) ? st : 0,
            Descuento = decimal.TryParse(d.Discount, NumberStyles.Any, CultureInfo.InvariantCulture, out var ds) ? ds : 0,
            Item = new Modelos.Base.Item
            {
                CodigoPrincipal = string.IsNullOrEmpty(d.ProductCode) ? "SIN CODIGO" : d.ProductCode,
                Descripcion = string.IsNullOrEmpty(d.Description) ? (d.ProductName ?? "SIN NOMBRE") : d.Description,
                CodigoAuxiliar = null
            },
            Impuestos = new List<Modelos.Base.Impuesto>
            {
                new Modelos.Base.ImpuestoIVA
                {
                    Codigo = Modelos.Enumerados.EnumTipoImpuesto.IVA,
                    CodigoPorcentaje = d.PercentageCode switch
                    {
                        "0" => EnumTipoImpuestoIVA._0,
                        "2" => EnumTipoImpuestoIVA._12,
                        "3" => EnumTipoImpuestoIVA._14,
                        "4" => EnumTipoImpuestoIVA._15,
                        "6" => EnumTipoImpuestoIVA.NoObjetoImpuesto,
                        "7" => EnumTipoImpuestoIVA.ExentoIVA,
                        _   => EnumTipoImpuestoIVA._0
                    },
                    BaseImponible = decimal.TryParse(d.TaxableBaseTax, NumberStyles.Any, CultureInfo.InvariantCulture, out var bt) ? bt : 0,
                    Valor = decimal.TryParse(d.TaxValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var tv) ? tv : 0,
                    Tarifa = decimal.TryParse(d.Rate, NumberStyles.Any, CultureInfo.InvariantCulture, out var rate) ? rate : 0
                }
            },
            DetallesAdicionales = new List<Modelos.Base.CampoAdicional>()
        }).ToList();

        // --- Pagos ---
        var pagos = new List<Modelos.Base.Pago>
        {
            new Modelos.Base.Pago
            {
                FormaPago = ObtenerFormaPago(request.Payment?.PaymentMethodCode),
                Plazo = 0,
                Total = decimal.TryParse(request.Payment?.TotalAmount, NumberStyles.Any, CultureInfo.InvariantCulture, out var t) ? t : 0,
                UnidadTiempo = "Dias"
            }
        };

        // --- Factura ---
        return new Modelos.Factura_1_0_0Modelo.Factura
        {
            PuntoEmision = puntoEmision,
            FechaEmision = DateTime.Now,
            InfoTributaria = infoTributaria,
            InfoFactura = new Modelos.Factura_1_0_0Modelo.InfoFactura
            {
                TotalSinImpuestos = string.IsNullOrEmpty(request.Payment?.TotalWithoutTaxes) ? 0
                    : decimal.Parse(request.Payment.TotalWithoutTaxes, CultureInfo.InvariantCulture),
                TotalDescuento = string.IsNullOrEmpty(request.Payment?.TotalDiscount) ? 0
                    : decimal.Parse(request.Payment.TotalDiscount, CultureInfo.InvariantCulture),
                ImporteTotal = string.IsNullOrEmpty(request.Payment?.TotalAmount) ? 0
                    : decimal.Parse(request.Payment.TotalAmount, CultureInfo.InvariantCulture),
                Moneda = request.Payment?.Currency ?? "DOLAR",

                TotalConImpuestos = detalles
                    .SelectMany(d => d.Impuestos.OfType<ImpuestoIVA>(), (d, i) => new ImpuestoVentaIVA
                    {
                        BaseImponible = i.BaseImponible,
                        Tarifa = i.CodigoPorcentaje switch
                        {
                            EnumTipoImpuestoIVA._0 => 0m,
                            EnumTipoImpuestoIVA._12 => 12m,
                            EnumTipoImpuestoIVA._14 => 14m,
                            EnumTipoImpuestoIVA._15 => 15m,
                            _ => 0m
                        },
                        Valor = i.Valor,
                        ValorDevolucionIVA = 0,
                        DescuentoAdicional = 0,
                        Codigo = i.Codigo,
                        CodigoPorcentaje = i.CodigoPorcentaje
                    })
                    .ToList<ImpuestoVenta>(),
                Pagos = pagos
            },
            Detalles = detalles,
            InfoAdicional = request.AdditionalInfo?.Select(a => new Modelos.Base.CampoAdicional
            {
                Nombre = string.IsNullOrEmpty(a.Name) ? null : a.Name,
                Valor = a.Value
            }).ToList() ?? new List<Modelos.Base.CampoAdicional>(),

            Sujeto = new Modelos.Base.Sujeto
            {
                Identificacion = request.Customer?.CustomerDni,
                RazonSocial = request.Customer?.CustomerName,
                TipoIdentificador = request.Customer?.IdentificationType switch
                {
                    "04" => Core.Enumerados.EnumTipoIdentificacion.RUC,
                    "05" => Core.Enumerados.EnumTipoIdentificacion.Cedula,
                    "06" => Core.Enumerados.EnumTipoIdentificacion.Pasaporte,
                    "07" => Core.Enumerados.EnumTipoIdentificacion.VentaConsumidorFinal,
                    "08" => Core.Enumerados.EnumTipoIdentificacion.IdentificacionExterior,
                    _ => Core.Enumerados.EnumTipoIdentificacion.VentaConsumidorFinal // valor por defecto
                }
            },

            Autorizacion = new Modelos.Base.Autorizacion
            {
                Numero = request.Autorizacion?.NumeroAutorizacion ?? "SIN AUTORIZACION",
                Fecha = request.Autorizacion?.FechaAutorizacion ?? DateTime.Now
            }
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error en MapFacturaRequestToFactura: " + ex.Message);
        throw;
    }
}


    }

}
