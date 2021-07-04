// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.NotaCredito_1_0_0.notaCreditoInfoNotaCredito
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
  public class notaCreditoInfoNotaCredito
  {
    private string fechaEmisionField;
    private string dirEstablecimientoField;
    private string tipoIdentificacionCompradorField;
    private string razonSocialCompradorField;
    private string identificacionCompradorField;
    private string contribuyenteEspecialField;
    private obligadoContabilidad obligadoContabilidadField;
    private bool obligadoContabilidadFieldSpecified;
    private string riseField;
    private string codDocModificadoField;
    private string numDocModificadoField;
    private string fechaEmisionDocSustentoField;
    private Decimal totalSinImpuestosField;
    private compensacion[] compensacionesField;
    private Decimal valorModificacionField;
    private string monedaField;
    private totalConImpuestosTotalImpuesto[] totalConImpuestosField;
    private string motivoField;

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

    public string tipoIdentificacionComprador
    {
      get => this.tipoIdentificacionCompradorField;
      set => this.tipoIdentificacionCompradorField = value;
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

    public string rise
    {
      get => this.riseField;
      set => this.riseField = value;
    }

    public string codDocModificado
    {
      get => this.codDocModificadoField;
      set => this.codDocModificadoField = value;
    }

    public string numDocModificado
    {
      get => this.numDocModificadoField;
      set => this.numDocModificadoField = value;
    }

    public string fechaEmisionDocSustento
    {
      get => this.fechaEmisionDocSustentoField;
      set => this.fechaEmisionDocSustentoField = value;
    }

    public Decimal totalSinImpuestos
    {
      get => this.totalSinImpuestosField;
      set => this.totalSinImpuestosField = value;
    }

    [XmlArrayItem(IsNullable = false)]
    public compensacion[] compensaciones
    {
      get => this.compensacionesField;
      set => this.compensacionesField = value;
    }

    public Decimal valorModificacion
    {
      get => this.valorModificacionField;
      set => this.valorModificacionField = value;
    }

    public string moneda
    {
      get => this.monedaField;
      set => this.monedaField = value;
    }

    [XmlArrayItem("totalImpuesto", IsNullable = false)]
    public totalConImpuestosTotalImpuesto[] totalConImpuestos
    {
      get => this.totalConImpuestosField;
      set => this.totalConImpuestosField = value;
    }

    public string motivo
    {
      get => this.motivoField;
      set => this.motivoField = value;
    }
  }
}
