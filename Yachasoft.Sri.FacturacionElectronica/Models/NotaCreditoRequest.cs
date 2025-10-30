using System;
using System.Collections.Generic;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.FacturacionElectronica.Models.Request
{
    // ========== NOTA DE CRÉDITO REQUEST ==========
    public class NotaCreditoRequest
    {
        public EmisorRequest Emisor { get; set; }
        public int CodigoEstablecimiento { get; set; }
        public int CodigoPuntoEmision { get; set; }
        public DateTime FechaEmision { get; set; }
        public int Secuencial { get; set; }
        public string EnumTipoEmision { get; set; }
        public ClienteRequest Cliente { get; set; }
        public DocumentoModificadoRequest DocumentoModificado { get; set; }
        public List<DetalleNotaCreditoRequest> Detalles { get; set; }
        public InfoNotaCreditoRequest InfoNotaCredito { get; set; }
        public List<CampoAdicional> InfoAdicional { get; set; }
    }

    // ========== INFO NOTA CRÉDITO (ESPECÍFICO) ==========
    public class InfoNotaCreditoRequest
    {
        public string Motivo { get; set; }
        public decimal TotalSinImpuestos { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ImpuestoVentaRequest> Impuestos { get; set; }
    }

    // ========== DETALLE NOTA CRÉDITO (ESPECÍFICO) ==========
    public class DetalleNotaCreditoRequest
    {
        public string CodigoInterno { get; set; }
        public string CodigoAdicional { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioTotalSinImpuesto { get; set; }
        public List<ImpuestoVentaRequest> Impuestos { get; set; }
    }

    // NO INCLUIR:
    // - EmisorRequest (ya está en SharedRequests.cs)
    // - ClienteRequest (ya está en SharedRequests.cs)
    // - DocumentoModificadoRequest (ya está en NotaDebitoRequest.cs o SharedRequests.cs)
    // - ImpuestoVentaRequest (ya debe estar en SharedRequests.cs)
}