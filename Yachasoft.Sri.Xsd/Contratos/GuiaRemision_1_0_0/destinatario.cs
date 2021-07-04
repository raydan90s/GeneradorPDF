// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0.destinatario
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class destinatario
  {
    private string identificacionDestinatarioField;
    private string razonSocialDestinatarioField;
    private string dirDestinatarioField;
    private string motivoTrasladoField;
    private string docAduaneroUnicoField;
    private string codEstabDestinoField;
    private string rutaField;
    private string codDocSustentoField;
    private string numDocSustentoField;
    private string numAutDocSustentoField;
    private string fechaEmisionDocSustentoField;
    private destinatarioDetalles detallesField;

    public string identificacionDestinatario
    {
      get => this.identificacionDestinatarioField;
      set => this.identificacionDestinatarioField = value;
    }

    public string razonSocialDestinatario
    {
      get => this.razonSocialDestinatarioField;
      set => this.razonSocialDestinatarioField = value;
    }

    public string dirDestinatario
    {
      get => this.dirDestinatarioField;
      set => this.dirDestinatarioField = value;
    }

    public string motivoTraslado
    {
      get => this.motivoTrasladoField;
      set => this.motivoTrasladoField = value;
    }

    public string docAduaneroUnico
    {
      get => this.docAduaneroUnicoField;
      set => this.docAduaneroUnicoField = value;
    }

    public string codEstabDestino
    {
      get => this.codEstabDestinoField;
      set => this.codEstabDestinoField = value;
    }

    public string ruta
    {
      get => this.rutaField;
      set => this.rutaField = value;
    }

    public string codDocSustento
    {
      get => this.codDocSustentoField;
      set => this.codDocSustentoField = value;
    }

    public string numDocSustento
    {
      get => this.numDocSustentoField;
      set => this.numDocSustentoField = value;
    }

    public string numAutDocSustento
    {
      get => this.numAutDocSustentoField;
      set => this.numAutDocSustentoField = value;
    }

    public string fechaEmisionDocSustento
    {
      get => this.fechaEmisionDocSustentoField;
      set => this.fechaEmisionDocSustentoField = value;
    }

    public destinatarioDetalles detalles
    {
      get => this.detallesField;
      set => this.detallesField = value;
    }
  }
}
