// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.NotaCredito_1_0_0.notaCreditoDetalle
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
  public class notaCreditoDetalle
  {
    private string codigoInternoField;
    private string codigoAdicionalField;
    private string descripcionField;
    private Decimal cantidadField;
    private Decimal precioUnitarioField;
    private Decimal descuentoField;
    private bool descuentoFieldSpecified;
    private Decimal precioTotalSinImpuestoField;
    private notaCreditoDetalleDetAdicional[] detallesAdicionalesField;
    private impuesto[] impuestosField;

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

    public Decimal precioUnitario
    {
      get => this.precioUnitarioField;
      set => this.precioUnitarioField = value;
    }

    public Decimal descuento
    {
      get => this.descuentoField;
      set => this.descuentoField = value;
    }

    [XmlIgnore]
    public bool descuentoSpecified
    {
      get => this.descuentoFieldSpecified;
      set => this.descuentoFieldSpecified = value;
    }

    public Decimal precioTotalSinImpuesto
    {
      get => this.precioTotalSinImpuestoField;
      set => this.precioTotalSinImpuestoField = value;
    }

    [XmlArrayItem("detAdicional", IsNullable = false)]
    public notaCreditoDetalleDetAdicional[] detallesAdicionales
    {
      get => this.detallesAdicionalesField;
      set => this.detallesAdicionalesField = value;
    }

    [XmlArrayItem(IsNullable = false)]
    public impuesto[] impuestos
    {
      get => this.impuestosField;
      set => this.impuestosField = value;
    }
  }
}
