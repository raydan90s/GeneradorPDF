// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0.detalle
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [Serializable]
  public class detalle
  {
    private string codigoInternoField;
    private string codigoAdicionalField;
    private string descripcionField;
    private Decimal cantidadField;
    private detalleDetAdicional[] detallesAdicionalesField;

    public string codigoInterno
    {
      get => this.codigoInternoField;
      set => this.codigoInternoField = value;
    }

    public string codigoAdicional
    {
      get => this.codigoAdicionalField;
      set => this.codigoAdicionalField = value;
    }

    public string descripcion
    {
      get => this.descripcionField;
      set => this.descripcionField = value;
    }

    public Decimal cantidad
    {
      get => this.cantidadField;
      set => this.cantidadField = value;
    }

    [XmlArrayItem("detAdicional", IsNullable = false)]
    public detalleDetAdicional[] detallesAdicionales
    {
      get => this.detallesAdicionalesField;
      set => this.detallesAdicionalesField = value;
    }
  }
}
