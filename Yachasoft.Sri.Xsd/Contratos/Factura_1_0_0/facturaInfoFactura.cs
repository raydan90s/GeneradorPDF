// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0.facturaInfoFactura
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  [Serializable]
  public class facturaInfoFactura
  {
    private string fechaEmisionField;
    private string dirEstablecimientoField;
    private string contribuyenteEspecialField;
    private obligadoContabilidad obligadoContabilidadField;
    private bool obligadoContabilidadFieldSpecified;
    private string comercioExteriorField;
    private string incoTermFacturaField;
    private string lugarIncoTermField;
    private string paisOrigenField;
    private string puertoEmbarqueField;
    private string puertoDestinoField;
    private string paisDestinoField;
    private string paisAdquisicionField;
    private string tipoIdentificacionCompradorField;
    private string guiaRemisionField;
    private string razonSocialCompradorField;
    private string identificacionCompradorField;
    private string direccionCompradorField;
    private Decimal totalSinImpuestosField;
    private Decimal totalSubsidioField;
    private bool totalSubsidioFieldSpecified;
    private string incoTermTotalSinImpuestosField;
    private Decimal totalDescuentoField;
    private string codDocReembolsoField;
    private Decimal totalComprobantesReembolsoField;
    private bool totalComprobantesReembolsoFieldSpecified;
    private Decimal totalBaseImponibleReembolsoField;
    private bool totalBaseImponibleReembolsoFieldSpecified;
    private Decimal totalImpuestoReembolsoField;
    private bool totalImpuestoReembolsoFieldSpecified;
    private facturaInfoFacturaTotalImpuesto[] totalConImpuestosField;
    private compensacion[] compensacionesField;
    private Decimal propinaField;
    private bool propinaFieldSpecified;
    private Decimal fleteInternacionalField;
    private bool fleteInternacionalFieldSpecified;
    private Decimal seguroInternacionalField;
    private bool seguroInternacionalFieldSpecified;
    private Decimal gastosAduanerosField;
    private bool gastosAduanerosFieldSpecified;
    private Decimal gastosTransporteOtrosField;
    private bool gastosTransporteOtrosFieldSpecified;
    private Decimal importeTotalField;
    private string monedaField;
    private string placaField;
    private pagosPago[] pagosField;
    private Decimal valorRetIvaField;
    private bool valorRetIvaFieldSpecified;
    private Decimal valorRetRentaField;
    private bool valorRetRentaFieldSpecified;

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

    public string comercioExterior
    {
      get => this.comercioExteriorField;
      set => this.comercioExteriorField = value;
    }

    public string incoTermFactura
    {
      get => this.incoTermFacturaField;
      set => this.incoTermFacturaField = value;
    }

    public string lugarIncoTerm
    {
      get => this.lugarIncoTermField;
      set => this.lugarIncoTermField = value;
    }

    public string paisOrigen
    {
      get => this.paisOrigenField;
      set => this.paisOrigenField = value;
    }

    public string puertoEmbarque
    {
      get => this.puertoEmbarqueField;
      set => this.puertoEmbarqueField = value;
    }

    public string puertoDestino
    {
      get => this.puertoDestinoField;
      set => this.puertoDestinoField = value;
    }

    public string paisDestino
    {
      get => this.paisDestinoField;
      set => this.paisDestinoField = value;
    }

    public string paisAdquisicion
    {
      get => this.paisAdquisicionField;
      set => this.paisAdquisicionField = value;
    }

    public string tipoIdentificacionComprador
    {
      get => this.tipoIdentificacionCompradorField;
      set => this.tipoIdentificacionCompradorField = value;
    }

    public string guiaRemision
    {
      get => this.guiaRemisionField;
      set => this.guiaRemisionField = value;
    }

    public string razonSocialComprador
    {
      get => this.razonSocialCompradorField;
      set => this.razonSocialCompradorField = value;
    }

    public string identificacionComprador
    {
      get => this.identificacionCompradorField;
      set => this.identificacionCompradorField = value;
    }

    public string direccionComprador
    {
      get => this.direccionCompradorField;
      set => this.direccionCompradorField = value;
    }

    public Decimal totalSinImpuestos
    {
      get => this.totalSinImpuestosField;
      set => this.totalSinImpuestosField = value;
    }

    public Decimal totalSubsidio
    {
      get => this.totalSubsidioField;
      set => this.totalSubsidioField = value;
    }

    [XmlIgnore]
    public bool totalSubsidioSpecified
    {
      get => this.totalSubsidioFieldSpecified;
      set => this.totalSubsidioFieldSpecified = value;
    }

    public string incoTermTotalSinImpuestos
    {
      get => this.incoTermTotalSinImpuestosField;
      set => this.incoTermTotalSinImpuestosField = value;
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
    public facturaInfoFacturaTotalImpuesto[] totalConImpuestos
    {
      get => this.totalConImpuestosField;
      set => this.totalConImpuestosField = value;
    }

    [XmlArrayItem(IsNullable = false)]
    public compensacion[] compensaciones
    {
      get => this.compensacionesField;
      set => this.compensacionesField = value;
    }

    public Decimal propina
    {
      get => this.propinaField;
      set => this.propinaField = value;
    }

    [XmlIgnore]
    public bool propinaSpecified
    {
      get => this.propinaFieldSpecified;
      set => this.propinaFieldSpecified = value;
    }

    public Decimal fleteInternacional
    {
      get => this.fleteInternacionalField;
      set => this.fleteInternacionalField = value;
    }

    [XmlIgnore]
    public bool fleteInternacionalSpecified
    {
      get => this.fleteInternacionalFieldSpecified;
      set => this.fleteInternacionalFieldSpecified = value;
    }

    public Decimal seguroInternacional
    {
      get => this.seguroInternacionalField;
      set => this.seguroInternacionalField = value;
    }

    [XmlIgnore]
    public bool seguroInternacionalSpecified
    {
      get => this.seguroInternacionalFieldSpecified;
      set => this.seguroInternacionalFieldSpecified = value;
    }

    public Decimal gastosAduaneros
    {
      get => this.gastosAduanerosField;
      set => this.gastosAduanerosField = value;
    }

    [XmlIgnore]
    public bool gastosAduanerosSpecified
    {
      get => this.gastosAduanerosFieldSpecified;
      set => this.gastosAduanerosFieldSpecified = value;
    }

    public Decimal gastosTransporteOtros
    {
      get => this.gastosTransporteOtrosField;
      set => this.gastosTransporteOtrosField = value;
    }

    [XmlIgnore]
    public bool gastosTransporteOtrosSpecified
    {
      get => this.gastosTransporteOtrosFieldSpecified;
      set => this.gastosTransporteOtrosFieldSpecified = value;
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

    public string placa
    {
      get => this.placaField;
      set => this.placaField = value;
    }

    [XmlArrayItem("pago", IsNullable = false)]
    public pagosPago[] pagos
    {
      get => this.pagosField;
      set => this.pagosField = value;
    }

    public Decimal valorRetIva
    {
      get => this.valorRetIvaField;
      set => this.valorRetIvaField = value;
    }

    [XmlIgnore]
    public bool valorRetIvaSpecified
    {
      get => this.valorRetIvaFieldSpecified;
      set => this.valorRetIvaFieldSpecified = value;
    }

    public Decimal valorRetRenta
    {
      get => this.valorRetRentaField;
      set => this.valorRetRentaField = value;
    }

    [XmlIgnore]
    public bool valorRetRentaSpecified
    {
      get => this.valorRetRentaFieldSpecified;
      set => this.valorRetRentaFieldSpecified = value;
    }
  }
}
