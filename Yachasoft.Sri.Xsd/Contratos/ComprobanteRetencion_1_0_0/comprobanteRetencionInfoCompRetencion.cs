// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.ComprobanteRetencion_1_0_0.comprobanteRetencionInfoCompRetencion
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.ComprobanteRetencion_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  [Serializable]
  public class comprobanteRetencionInfoCompRetencion
  {
    private string fechaEmisionField;
    private string dirEstablecimientoField;
    private string contribuyenteEspecialField;
    private obligadoContabilidad obligadoContabilidadField;
    private bool obligadoContabilidadFieldSpecified;
    private string tipoIdentificacionSujetoRetenidoField;
    private string razonSocialSujetoRetenidoField;
    private string identificacionSujetoRetenidoField;
    private string periodoFiscalField;

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string fechaEmision
    {
      get => this.fechaEmisionField;
      set => this.fechaEmisionField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string dirEstablecimiento
    {
      get => this.dirEstablecimientoField;
      set => this.dirEstablecimientoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string contribuyenteEspecial
    {
      get => this.contribuyenteEspecialField;
      set => this.contribuyenteEspecialField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
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

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string tipoIdentificacionSujetoRetenido
    {
      get => this.tipoIdentificacionSujetoRetenidoField;
      set => this.tipoIdentificacionSujetoRetenidoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string razonSocialSujetoRetenido
    {
      get => this.razonSocialSujetoRetenidoField;
      set => this.razonSocialSujetoRetenidoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string identificacionSujetoRetenido
    {
      get => this.identificacionSujetoRetenidoField;
      set => this.identificacionSujetoRetenidoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string periodoFiscal
    {
      get => this.periodoFiscalField;
      set => this.periodoFiscalField = value;
    }
  }
}
