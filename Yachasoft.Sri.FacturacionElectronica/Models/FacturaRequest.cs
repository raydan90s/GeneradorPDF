using System;
using System.Collections.Generic;

namespace Yachasoft.Sri.FacturacionElectronica.Models.Request
{
    public class FacturaRequest
    {
        public DocumentInfoRequest DocumentInfo { get; set; }
        public CustomerRequest Customer { get; set; }
        public PaymentRequest Payment { get; set; }
        public List<DetailRequest> Details { get; set; }
        public List<AdditionalInfoRequest> AdditionalInfo { get; set; }
        public List<TotalsWithTaxRequest> TotalsWithTax { get; set; }
        public AutorizacionRequest Autorizacion { get; set; }

    }

    public class DocumentInfoRequest
    {
        public string AccessKey { get; set; }
        public string BusinessName { get; set; }
        public string CommercialName { get; set; }
        public string BusinessAddress { get; set; }
        public string DayEmission { get; set; }
        public string MonthEmission { get; set; }
        public string YearEmission { get; set; }
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

    public class CustomerRequest
    {
        public string IdentificationType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDni { get; set; }
        public string CustomerAddress { get; set; }
    }

    public class PaymentRequest
    {
        public string TotalWithoutTaxes { get; set; }
        public string TotalDiscount { get; set; }
        public string Gratuity { get; set; }
        public string TotalAmount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethodCode { get; set; }
        public string TotalPayment { get; set; }
    }

    public class DetailRequest
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string SubTotal { get; set; }
        public string TaxTypeCode { get; set; }
        public string PercentageCode { get; set; }
        public string Rate { get; set; }
        public string TaxableBaseTax { get; set; }
        public string TaxValue { get; set; }
    }

    public class AdditionalInfoRequest
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class TotalsWithTaxRequest
    {
        public string TaxCode { get; set; }
        public string PercentageCode { get; set; }
        public string TaxableBase { get; set; }
        public string TaxValue { get; set; }
    }
    public class AutorizacionRequest
    {
        public string NumeroAutorizacion { get; set; }
        public DateTime? FechaAutorizacion { get; set; }
    }

}
