// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.LiquidacionCompra_1_0_0.liquidacionCompraInfoLiquidacionCompra
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
  public class liquidacionCompraInfoLiquidacionCompra
  {
    private string fechaEmisionField;
    private string dirEstablecimientoField;
    private string contribuyenteEspecialField;
    private obligadoContabilidad obligadoContabilidadField;
    private bool obligadoContabilidadFieldSpecified;
    private string tipoIdentificacionProveedorField;
    private string razonSocialProveedorField;
    private string identificacionProveedorField;
    private string direccionProveedorField;
    private Decimal totalSinImpuestosField;
    private Decimal totalDescuentoField;
    private string codDocReembolsoField;
    private Decimal totalComprobantesReembolsoField;
    private bool totalComprobantesReembolsoFieldSpecified;
    private Decimal totalBaseImponibleReembolsoField;
    private bool totalBaseImponibleReembolsoFieldSpecified;
    private Decimal totalImpuestoReembolsoField;
    private bool totalImpuestoReembolsoFieldSpecified;
    private liquidacionCompraInfoLiquidacionCompraTotalImpuesto[] totalConImpuestosField;
    private Decimal importeTotalField;
    private string monedaField;
    private pagosPago[] pagosField;

    public string fechaEmision
    {
      get => this.fechaEmisionField;
      set => this.fechaEmisionField = value;
    }

    public string dirEstablecimiento
    {
      get => this.dirEstablecimientoField;
      set => this.dirEstablecimientoField = value;
    }

    public string contribuyenteEspecial
    {
      get => this.contribuyenteEspecialField;
      set => this.contribuyenteEspecialField = value;
    }

    public obligadoContabilidad obligadoContabilidad
    {
      get => this.obligadoContabilidadField;
      set => this.obligadoContabilidadField = value;
    }

    [XmlIgnore]
    public bool obligadoContabilidadSpecified
    {
      get => this.obligadoContabilidadFieldSpecified;
      set => this.obligadoContabilidadFieldSpecified = value;
    }

    public string tipoIdentificacionProveedor
    {
      get => this.tipoIdentificacionProveedorField;
      set => this.tipoIdentificacionProveedorField = value;
    }

    public string razonSocialProveedor
    {
      get => this.razonSocialProveedorField;
      set => this.razonSocialProveedorField = value;
    }

    public string identificacionProveedor
    {
      get => this.identificacionProveedorField;
      set => this.identificacionProveedorField = value;
    }

    public string direccionProveedor
    {
      get => this.direccionProveedorField;
      set => this.direccionProveedorField = value;
    }

    public Decimal totalSinImpuestos
    {
      get => this.totalSinImpuestosField;
      set => this.totalSinImpuestosField = value;
    }

    public Decimal totalDescuento
    {
      get => this.totalDescuentoField;
      set => this.totalDescuentoField = value;
    }

    public string codDocReembolso
    {
      get => this.codDocReembolsoField;
      set => this.codDocReembolsoField = value;
    }

    public Decimal totalComprobantesReembolso
    {
      get => this.totalComprobantesReembolsoField;
      set => this.totalComprobantesReembolsoField = value;
    }

    [XmlIgnore]
    public bool totalComprobantesReembolsoSpecified
    {
      get => this.totalComprobantesReembolsoFieldSpecified;
      set => this.totalComprobantesReembolsoFieldSpecified = value;
    }

    public Decimal totalBaseImponibleReembolso
    {
      get => this.totalBaseImponibleReembolsoField;
      set => this.totalBaseImponibleReembolsoField = value;
    }

    [XmlIgnore]
    public bool totalBaseImponibleReembolsoSpecified
    {
      get => this.totalBaseImponibleReembolsoFieldSpecified;
      set => this.totalBaseImponibleReembolsoFieldSpecified = value;
    }

    public Decimal totalImpuestoReembolso
    {
      get => this.totalImpuestoReembolsoField;
      set => this.totalImpuestoReembolsoField = value;
    }

    [XmlIgnore]
    public bool totalImpuestoReembolsoSpecified
    {
      get => this.totalImpuestoReembolsoFieldSpecified;
      set => this.totalImpuestoReembolsoFieldSpecified = value;
    }

    [XmlArrayItem("totalImpuesto", IsNullable = false)]
    public liquidacionCompraInfoLiquidacionCompraTotalImpuesto[] totalConImpuestos
    {
      get => this.totalConImpuestosField;
      set => this.totalConImpuestosField = value;
    }

    public Decimal importeTotal
    {
      get => this.importeTotalField;
      set => this.importeTotalField = value;
    }

    public string moneda
    {
      get => this.monedaField;
      set => this.monedaField = value;
    }

    [XmlArrayItem("pago", IsNullable = false)]
    public pagosPago[] pagos
    {
      get => this.pagosField;
      set => this.pagosField = value;
    }
  }
}
