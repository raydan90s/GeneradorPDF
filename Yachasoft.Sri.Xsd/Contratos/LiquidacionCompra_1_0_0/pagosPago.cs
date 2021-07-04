// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.LiquidacionCompra_1_0_0.pagosPago
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.LiquidacionCompra_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  [Serializable]
  public class pagosPago
  {
    private string formaPagoField;
    private Decimal totalField;
    private Decimal plazoField;
    private bool plazoFieldSpecified;
    private string unidadTiempoField;

    public string formaPago
    {
      get => this.formaPagoField;
      set => this.formaPagoField = value;
    }

    public Decimal total
    {
      get => this.totalField;
      set => this.totalField = value;
    }

    public Decimal plazo
    {
      get => this.plazoField;
      set => this.plazoField = value;
    }

    [XmlIgnore]
    public bool plazoSpecified
    {
      get => this.plazoFieldSpecified;
      set => this.plazoFieldSpecified = value;
    }

    public string unidadTiempo
    {
      get => this.unidadTiempoField;
      set => this.unidadTiempoField = value;
    }
  }
}
