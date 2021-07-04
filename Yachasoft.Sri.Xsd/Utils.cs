using System;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Core.Helpers;
using Yachasoft.Sri.Modelos;
using Yachasoft.Sri.Modelos.Enumerados;

namespace Yachasoft.Sri.Xsd
{
  public static class Utils
  {
    public static string GenerarClaveAcceso(
      EnumTipoDocumento tipoDocumento,
      DateTime fechaEmision,
      PuntoEmision puntoEmision,
      long secuencial,
      EnumTipoEmision enumTipoEmision)
    {
      string cadenaNumeros = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", fechaEmision.ToString("ddMMyyyy"), tipoDocumento.ObtenerSRICodigo(), puntoEmision.Establecimiento.Emisor.RUC, puntoEmision.Establecimiento.Emisor.EnumTipoAmbiente.ObtenerSRICodigo(), puntoEmision.Establecimiento.Codigo.ToString("D3"), puntoEmision.Codigo.ToString("D3"), secuencial.ToString("D9"), "12345678", enumTipoEmision.ObtenerSRICodigo());
      return string.Format("{0}{1:D1}", cadenaNumeros, cadenaNumeros.ObtenerModulo11());
    }
  }
}
