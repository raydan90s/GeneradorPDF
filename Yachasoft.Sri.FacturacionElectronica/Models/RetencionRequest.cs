using System;
using System.Collections.Generic;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.FacturacionElectronica.Models.Request
{
    public class RetencionRequest
    {
        public EmisorRequest Emisor { get; set; }
        public int CodigoEstablecimiento { get; set; }
        public int CodigoPuntoEmision { get; set; }
        public DateTime FechaEmision { get; set; }
        public string PeriodoFiscal { get; set; }
        public ClienteRequest Sujeto { get; set; }  
        public List<ImpuestoRetencionRequest> Impuestos { get; set; }
        public List<CampoAdicional> InfoAdicional { get; set; }
        public int Secuencial { get; set; }
        public string EnumTipoEmision { get; set; }
    }

    public class ImpuestoRetencionRequest
    {
        public decimal BaseImponible { get; set; }
        public decimal Tarifa { get; set; }
        public string CodigoRetencion { get; set; }
        public DocumentoSustentoRequest DocumentoSustento { get; set; }
    }

    public class DocumentoSustentoRequest
    {
        public string CodDocumento { get; set; }
        public string NumDocumento { get; set; }
        public DateTime FechaEmisionDocumento { get; set; }
    }
}