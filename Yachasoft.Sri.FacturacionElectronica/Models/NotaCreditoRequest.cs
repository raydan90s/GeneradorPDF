using System;
using System.Collections.Generic;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.FacturacionElectronica.Models.Request
{
    public class NotaCreditoRequest
    {
        public EmisorRequest Emisor { get; set; }
        public int CodigoEstablecimiento { get; set; }
        public int CodigoPuntoEmision { get; set; }
        public DateTime FechaEmision { get; set; }
        public ClienteRequest Cliente { get; set; }
        public InfoNotaCreditoRequest InfoNotaCredito { get; set; }
        public DocumentoModificadoRequest DocumentoModificado { get; set; }
        public List<MotivoRequest> Motivos { get; set; }
        public List<CampoAdicional> InfoAdicional { get; set; }
        public int Secuencial { get; set; }
        public string EnumTipoEmision { get; set; }
    }

    public class InfoNotaCreditoRequest
    {
        public decimal TotalSinImpuestos { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ImpuestoVentaRequest> Impuestos { get; set; }
        public List<PagoRequest> Pagos { get; set; }
    }

    public class DocumentoModificadoRequest
    {
        public string CodDocumento { get; set; }  // "01" para factura
        public string NumDocumento { get; set; }  // 001-001-000000123
        public DateTime FechaEmisionDocumento { get; set; }
    }

    public class MotivoRequest
    {
        public string Razon { get; set; }
        public decimal Valor { get; set; }
    }

    public class EmisorRequest
    {
        public string DireccionMatriz { get; set; }
        public string DireccionEstablecimiento { get; set; }
        public string EnumTipoAmbiente { get; set; }
        public string NombreComercial { get; set; }
        public bool? ObligadoContabilidad { get; set; }
        public string RazonSocial { get; set; }
        public bool? RegimenMicroEmpresas { get; set; }
        public string RUC { get; set; }
        public string ContribuyenteEspecial { get; set; }
        public bool? AgenteRetencion { get; set; }
    }
}
