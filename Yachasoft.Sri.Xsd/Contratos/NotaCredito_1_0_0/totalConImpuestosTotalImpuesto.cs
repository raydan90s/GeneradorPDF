// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.NotaCredito_1_0_0.totalConImpuestosTotalImpuesto
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.NotaCredito_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  [Serializable]
  public class totalConImpuestosTotalImpuesto
  {
    private string codigoField;
    private string codigoPorcentajeField;
    private Decimal baseImponibleField;
    private Decimal valorField;
    private Decimal valorDevolucionIvaField;
    private bool valorDevolucionIvaFieldSpecified;

    public string codigo
    {
      get => this.codigoField;
      set => this.codigoField = value;
    }

    public string codigoPorcentaje
    {
      get => this.codigoPorcentajeField;
      set => this.codigoPorcentajeField = value;
    }

    public Decimal baseImponible
    {
      get => this.baseImponibleField;
      set => this.baseImponibleField = value;
    }

    public Decimal valor
    {
      get => this.valorField;
      set => this.valorField = value;
    }

    public Decimal valorDevolucionIva
    {
      get => this.valorDevolucionIvaField;
      set => this.valorDevolucionIvaField = value;
    }

    [XmlIgnore]
    public bool valorDevolucionIvaSpecified
    {
      get => this.valorDevolucionIvaFieldSpecified;
      set => this.valorDevolucionIvaFieldSpecified = value;
    }
  }
}
