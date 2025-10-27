using System;
using System.Collections.Generic;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.FacturacionElectronica.Models.Request
{
    public class LiquidacionRequest
    {
        public EmisorRequest Emisor { get; set; }
        public int CodigoEstablecimiento { get; set; }
        public int CodigoPuntoEmision { get; set; }
        public DateTime FechaEmision { get; set; }
        public ClienteRequest Cliente { get; set; }
        public InfoLiquidacionRequest InfoLiquidacion { get; set; }
        public List<DetalleRequest> Detalles { get; set; }
        public List<CampoAdicional> InfoAdicional { get; set; }
        public int Secuencial { get; set; }
        public string EnumTipoEmision { get; set; }
    }

    public class InfoLiquidacionRequest
    {
        public decimal TotalSinImpuestos { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal ImporteTotal { get; set; }
        public List<ImpuestoVentaRequest> TotalConImpuestos { get; set; }
        public List<PagoRequest> Pagos { get; set; }
    }

    public class ImpuestoVentaRequest
    {
        public decimal BaseImponible { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Valor { get; set; }
        public string CodigoPorcentaje { get; set; }
    }

    public class PagoRequest
    {
        public string FormaPago { get; set; }
        public decimal Total { get; set; }
        public int Plazo { get; set; }
        public string UnidadTiempo { get; set; }
    }

    public class DetalleRequest
    {
        public string CodigoPrincipal { get; set; }
        public string CodigoAuxiliar { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioTotalSinImpuesto { get; set; }
        public List<ImpuestoRequest> Impuestos { get; set; }
        public List<CampoAdicional> DetallesAdicionales { get; set; }
    }

    public class ImpuestoRequest
    {
        public decimal BaseImponible { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Valor { get; set; }
        public string CodigoPorcentaje { get; set; }
    }
}