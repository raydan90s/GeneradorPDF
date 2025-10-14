using System;
using System.Collections.Generic;

namespace Yachasoft.Sri.FacturacionElectronica.DTOs
{
    public class LiquidacionRequestDto
    {
        public EmisorDto Emisor { get; set; }
        public EstablecimientoDto Establecimiento { get; set; }
        public PuntoEmisionDto PuntoEmision { get; set; }
        public ProveedorDto Proveedor { get; set; }
        public InfoLiquidacionDto InfoLiquidacion { get; set; }
        public List<DetalleDto> Detalles { get; set; }
        public List<CampoAdicionalDto> InfoAdicional { get; set; }
        public DateTime FechaEmision { get; set; }
        public int Secuencial { get; set; }
    }

    public class EmisorDto
    {
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string DireccionMatriz { get; set; }
        public bool ObligadoContabilidad { get; set; }
        public bool RegimenMicroEmpresas { get; set; }
        public bool EsAmbientePrueba { get; set; } = true;
    }

    public class EstablecimientoDto
    {
        public int Codigo { get; set; }
        public string Direccion { get; set; }
    }

    public class PuntoEmisionDto
    {
        public int Codigo { get; set; }
    }

    public class ProveedorDto
    {
        public string Identificacion { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public int TipoIdentificacion { get; set; } // 4 = RUC, 5 = Cédula, 6 = Pasaporte, 7 = Consumidor Final
    }

    public class InfoLiquidacionDto
    {
        public decimal TotalSinImpuestos { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal ImporteTotal { get; set; }
        public List<ImpuestoVentaDto> TotalConImpuestos { get; set; }
        public List<PagoDto> Pagos { get; set; }
    }

    public class ImpuestoVentaDto
    {
        public decimal BaseImponible { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Valor { get; set; }
        /// <summary>
        /// Código de porcentaje IVA según EnumTipoImpuestoIVA:
        /// 0 = IVA 0%
        /// 2 = IVA 12%
        /// 3 = IVA 14%
        /// 4 = IVA 15%
        /// 6 = No objeto de impuesto
        /// 7 = Exento de IVA
        /// </summary>
        public int CodigoPorcentaje { get; set; }
    }

    public class PagoDto
    {
        public int FormaPago { get; set; } // 01=Sin utilizar sistema financiero, 16=Tarjeta débito, 19=Tarjeta crédito, 20=Otros
        public decimal Total { get; set; }
        public int Plazo { get; set; }
        public string UnidadTiempo { get; set; } // dias, meses, años
    }

    public class DetalleDto
    {
        public string CodigoPrincipal { get; set; }
        public string CodigoAuxiliar { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioTotalSinImpuesto { get; set; }
        public List<ImpuestoDto> Impuestos { get; set; }
        public List<CampoAdicionalDto> DetallesAdicionales { get; set; }
    }

    public class ImpuestoDto
    {
        public decimal BaseImponible { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Valor { get; set; }
        /// <summary>
        /// Código de porcentaje IVA según EnumTipoImpuestoIVA:
        /// 0 = IVA 0%
        /// 2 = IVA 12%
        /// 3 = IVA 14%
        /// 4 = IVA 15%
        /// 6 = No objeto de impuesto
        /// 7 = Exento de IVA
        /// </summary>
        public int CodigoPorcentaje { get; set; }
    }

    public class CampoAdicionalDto
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}