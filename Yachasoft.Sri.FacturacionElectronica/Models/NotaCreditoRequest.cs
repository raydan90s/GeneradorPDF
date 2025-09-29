using System;
using System.Collections.Generic;

namespace Yachasoft.Sri.FacturacionElectronica.Models.Request
{
    public class NotaCreditoRequest
    {
        public DocumentInfo DocumentInfo { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public CreditNoteData CreditNoteData { get; set; }  // <-- AGREGAR ESTA
        public List<Detail> Details { get; set; }
        public List<AdditionalInfo> AdditionalInfo { get; set; }
        public Authorization Autorizacion { get; set; }
        public List<TotalsWithTax> TotalsWithTax { get; set; }

    }

    public class CreditNoteData
    {
    public string ModifiedDocCode { get; set; }
    public string ModifiedDocNumber { get; set; }
    public string ModifiedDocDate { get; set; }
    public string Reason { get; set; }
    }

    public class ModifiedDocument
    {
        public string Code { get; set; }
        public string Number { get; set; }
        public DateTime? IssueDate { get; set; }
    }
    public class DocumentInfo
    {
        public string AccessKey { get; set; }
        public string BusinessName { get; set; }
        public string CommercialName { get; set; }
        public string BusinessAddress { get; set; }
        public string CodDoc { get; set; }
        public string RucBusiness { get; set; }
        public string Environment { get; set; }
        public string TypeEmission { get; set; }
        public string Establishment { get; set; }
        public string EstablishmentAddress { get; set; }
        public string EmissionPoint { get; set; }
        public string Sequential { get; set; }
        public string ObligatedAccounting { get; set; }
    }

    public class Customer
    {
        public string IdentificationType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDni { get; set; }
        public string CustomerAddress { get; set; }
    }

    public class Payment
    {
        public string TotalWithoutTaxes { get; set; }
        public string TotalDiscount { get; set; }
        public string Gratuity { get; set; }
        public string TotalAmount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethodCode { get; set; }
        public string TotalPayment { get; set; }
    }

    public class TotalsWithTax
{
    public string TaxCode { get; set; }
    public string PercentageCode { get; set; }
    public string TaxableBase { get; set; }
    public string TaxValue { get; set; }
}
    public class Detail
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string SubTotal { get; set; }
        public string TaxTypeCode { get; set; }
        public string PercentageCode { get; set; }
        public string Rate { get; set; }
        public string TaxableBaseTax { get; set; }
        public string TaxValue { get; set; }
    }

    public class AdditionalInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Authorization
    {
        public string EstadoAutorizacion { get; set; }
        public string NumeroAutorizacion { get; set; }
        public DateTime? FechaAutorizacion { get; set; }
    }


}

