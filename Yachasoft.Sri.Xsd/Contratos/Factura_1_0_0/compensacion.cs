// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0.compensacion
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class compensacion
  {
    private string codigoField;
    private Decimal tarifaField;
    private Decimal valorField;

    public string codigo
    {
      get => this.codigoField;
      set => this.codigoField = value;
    }

    public Decimal tarifa
    {
      get => this.tarifaField;
      set => this.tarifaField = value;
    }

    public Decimal valor
    {
      get => this.valorField;
      set => this.valorField = value;
    }
  }
}
