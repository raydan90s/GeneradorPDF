using System;
using System.Collections.Generic;
using Yachasoft.Sri.Modelos.Base;

namespace Yachasoft.Sri.FacturacionElectronica.Models.Request
{
    public class EmisorRequest
    {
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string DireccionMatriz { get; set; }
        public string DireccionEstablecimiento { get; set; }
        public bool ObligadoContabilidad { get; set; }
        public bool RegimenMicroEmpresas { get; set; }
        public string EnumTipoAmbiente { get; set; }
        public string ContribuyenteEspecial { get; set; }
        public string AgenteRetencion { get; set; }
    }

     public class ClienteRequest
    {
        public string Identificacion { get; set; }
        public string RazonSocial { get; set; }
        public string TipoIdentificador { get; set; }
    }


}