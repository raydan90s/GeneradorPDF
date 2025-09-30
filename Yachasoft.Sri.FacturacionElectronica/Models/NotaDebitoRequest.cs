using System;
using System.Collections.Generic;

namespace Yachasoft.Sri.FacturacionElectronica.Models.Request
{
    public class NotaDebitoRequest
    {
        public DocumentInfo DocumentInfo { get; set; } // ya existente
        public Customer Customer { get; set; }         // ya existente
        public Payment Payment { get; set; }           // ya existente
        public DebitNoteData DebitNoteData { get; set; } // NUEVO para débito
        public List<Detail> Details { get; set; }     // ya existente
        public List<AdditionalInfo> AdditionalInfo { get; set; } // ya existente
        public Authorization Autorizacion { get; set; } // ya existente
        public List<TotalsWithTax> TotalsWithTax { get; set; } // ya existente
    }

    public class DebitNoteData
    {
        public string ModifiedDocCode { get; set; }
        public string ModifiedDocNumber { get; set; }
        public string ModifiedDocDate { get; set; }
        public string Reason { get; set; }
        public string Value {get; set;}
    }

    
}
