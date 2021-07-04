// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.IRIDEService
// Assembly: Yachasoft.Sri.Ride, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 1BF1C7CF-2C44-4106-9FF6-9D28D32C53F0
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.ride\1.1.5\lib\net5.0\Yachasoft.Sri.Ride.dll

using Yachasoft.Sri.Modelos;
using System.IO;

namespace Yachasoft.Sri.Ride
{
  public interface IRIDEService
  {
    string Factura_1_0_0(Factura_1_0_0Modelo.Factura factura, string rutaArchivoAGenerar = null);

    string ComprobanteRetencion_1_0_0(
      ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion retencion,
      string rutaArchivoAGenerar = null);

    string NotaDebito_1_0_0(
      NotaDebito_1_0_0Modelo.NotaDebito notaDebito,
      string rutaArchivoAGenerar = null);

    string NotaCredito_1_0_0(
      NotaCredito_1_0_0Modelo.NotaCredito notaCredito,
      string rutaArchivoAGenerar = null);

    string GuiaRemision_1_0_0(
      GuiaRemision_1_0_0Modelo.GuiaRemision guiaRemision,
      string rutaArchivoAGenerar = null);

    string LiquidacionCompra_1_0_0(
      LiquidacionCompra_1_0_0Modelo.LiquidacionCompra guiaRemision,
      string rutaArchivoAGenerar = null);

    MemoryStream Factura_1_0_0(Factura_1_0_0Modelo.Factura factura);

    MemoryStream ComprobanteRetencion_1_0_0(
      ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion retencion);

    MemoryStream NotaDebito_1_0_0(NotaDebito_1_0_0Modelo.NotaDebito notaDebito);

    MemoryStream NotaCredito_1_0_0(NotaCredito_1_0_0Modelo.NotaCredito notaCredito);

    MemoryStream GuiaRemision_1_0_0(GuiaRemision_1_0_0Modelo.GuiaRemision guiaRemision);

    MemoryStream LiquidacionCompra_1_0_0(
      LiquidacionCompra_1_0_0Modelo.LiquidacionCompra guiaRemision);
  }
}
