// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.LiquidacionCompra_1_0_0.liquidacionCompraDetalle
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
  public class liquidacionCompraDetalle
  {
    private string codigoPrincipalField;
    private string codigoAuxiliarField;
    private string descripcionField;
    private string unidadMedidaField;
    private Decimal cantidadField;
    private Decimal precioUnitarioField;
    private Decimal precioSinSubsidioField;
    private bool precioSinSubsidioFieldSpecified;
    private Decimal descuentoField;
    private Decimal precioTotalSinImpuestoField;
    private liquidacionCompraDetalleDetAdicional[] detallesAdicionalesField;
    private impuesto[] impuestosField;

    public string codigoPrincipal
    {
      get => this.codigoPrincipalField;
      set => this.codigoPrincipalField = value;
    }

    public string codigoAuxiliar
    {
      get => this.codigoAuxiliarField;
      set => this.codigoAuxiliarField = value;
    }

    public string descripcion
    {
      get => this.descripcionField;
      set => this.descripcionField = value;
    }

    public string unidadMedida
    {
      get => this.unidadMedidaField;
      set => this.unidadMedidaField = value;
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

    public Decimal precioSinSubsidio
    {
      get => this.precioSinSubsidioField;
      set => this.precioSinSubsidioField = value;
    }

    [XmlIgnore]
    public bool precioSinSubsidioSpecified
    {
      get => this.precioSinSubsidioFieldSpecified;
      set => this.precioSinSubsidioFieldSpecified = value;
    }

    public Decimal descuento
    {
      get => this.descuentoField;
      set => this.descuentoField = value;
    }

    public Decimal precioTotalSinImpuesto
    {
      get => this.precioTotalSinImpuestoField;
      set => this.precioTotalSinImpuestoField = value;
    }

    [XmlArrayItem("detAdicional", IsNullable = false)]
    public liquidacionCompraDetalleDetAdicional[] detallesAdicionales
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
