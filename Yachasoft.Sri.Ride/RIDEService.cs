// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Ride.RIDEService
// Assembly: Yachasoft.Sri.Ride, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 1BF1C7CF-2C44-4106-9FF6-9D28D32C53F0
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.ride\1.1.5\lib\net5.0\Yachasoft.Sri.Ride.dll

using Yachasoft.Sri.DocumentosElectronicos.Configuracion;
using Yachasoft.Sri.Modelos;
using System.IO;
using System.Text;

namespace Yachasoft.Sri.Ride
{
  public class RIDEService : IRIDEService
  {
    private SRIDocumentosElectronicosOptions _options;

    public RIDEService(SRIDocumentosElectronicosOptions options)
    {
      this._options = options;
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public string Factura_1_0_0(Factura_1_0_0Modelo.Factura factura, string rutaArchivoAGenerar = null) => new Yachasoft.Sri.Ride.Documentos.Factura_1_0_0().GenerarRIDE(factura, rutaArchivoAGenerar);

    public MemoryStream Factura_1_0_0(Factura_1_0_0Modelo.Factura factura) => new Yachasoft.Sri.Ride.Documentos.Factura_1_0_0().GenerarRIDE(factura);

    public string ComprobanteRetencion_1_0_0(
      ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion retencion,
      string rutaArchivoAGenerar = null)
    {
      return new Yachasoft.Sri.Ride.Documentos.ComprobanteRetencion_1_0_0().GenerarRIDE(retencion, rutaArchivoAGenerar);
    }

    public MemoryStream ComprobanteRetencion_1_0_0(
      ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion retencion)
    {
      return new Yachasoft.Sri.Ride.Documentos.ComprobanteRetencion_1_0_0().GenerarRIDE(retencion);
    }

    public string NotaDebito_1_0_0(
      NotaDebito_1_0_0Modelo.NotaDebito notaDebito,
      string rutaArchivoAGenerar = null)
    {
      return new Yachasoft.Sri.Ride.Documentos.NotaDebito_1_0_0().GenerarRIDE(notaDebito, rutaArchivoAGenerar);
    }

    public MemoryStream NotaDebito_1_0_0(NotaDebito_1_0_0Modelo.NotaDebito notaDebito) => new Yachasoft.Sri.Ride.Documentos.NotaDebito_1_0_0().GenerarRIDE(notaDebito);

    public string NotaCredito_1_0_0(
      NotaCredito_1_0_0Modelo.NotaCredito notaCredito,
      string rutaArchivoAGenerar = null)
    {
      return new Yachasoft.Sri.Ride.Documentos.NotaCredito_1_0_0().GenerarRIDE(notaCredito, rutaArchivoAGenerar);
    }

    public MemoryStream NotaCredito_1_0_0(
      NotaCredito_1_0_0Modelo.NotaCredito notaCredito)
    {
      return new Yachasoft.Sri.Ride.Documentos.NotaCredito_1_0_0().GenerarRIDE(notaCredito);
    }

    public string GuiaRemision_1_0_0(
      GuiaRemision_1_0_0Modelo.GuiaRemision guiaRemision,
      string rutaArchivoAGenerar = null)
    {
      return new Yachasoft.Sri.Ride.Documentos.GuiaRemision_1_0_0().GenerarRIDE(guiaRemision, rutaArchivoAGenerar);
    }

    public MemoryStream GuiaRemision_1_0_0(
      GuiaRemision_1_0_0Modelo.GuiaRemision guiaRemision)
    {
      return new Yachasoft.Sri.Ride.Documentos.GuiaRemision_1_0_0().GenerarRIDE(guiaRemision);
    }

    public string LiquidacionCompra_1_0_0(
      LiquidacionCompra_1_0_0Modelo.LiquidacionCompra guiaRemision,
      string rutaArchivoAGenerar = null)
    {
      return new Yachasoft.Sri.Ride.Documentos.LiquidacionCompra_1_0_0().GenerarRIDE(guiaRemision, rutaArchivoAGenerar);
    }

    public MemoryStream LiquidacionCompra_1_0_0(
      LiquidacionCompra_1_0_0Modelo.LiquidacionCompra guiaRemision)
    {
      return new Yachasoft.Sri.Ride.Documentos.LiquidacionCompra_1_0_0().GenerarRIDE(guiaRemision);
    }
  }
}
