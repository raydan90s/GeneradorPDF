// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.ComprobanteRetencion_1_0_0.infoTributaria
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
  [Serializable]
  public class infoTributaria
  {
    private string ambienteField;
    private string tipoEmisionField;
    private string razonSocialField;
    private string nombreComercialField;
    private string rucField;
    private string claveAccesoField;
    private string codDocField;
    private string estabField;
    private string ptoEmiField;
    private string secuencialField;
    private string dirMatrizField;
    private string regimenMicroempresasField;
    private string agenteRetencionField;

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string ambiente
    {
      get => this.ambienteField;
      set => this.ambienteField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string tipoEmision
    {
      get => this.tipoEmisionField;
      set => this.tipoEmisionField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string razonSocial
    {
      get => this.razonSocialField;
      set => this.razonSocialField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string nombreComercial
    {
      get => this.nombreComercialField;
      set => this.nombreComercialField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string ruc
    {
      get => this.rucField;
      set => this.rucField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string claveAcceso
    {
      get => this.claveAccesoField;
      set => this.claveAccesoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string codDoc
    {
      get => this.codDocField;
      set => this.codDocField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string estab
    {
      get => this.estabField;
      set => this.estabField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string ptoEmi
    {
      get => this.ptoEmiField;
      set => this.ptoEmiField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string secuencial
    {
      get => this.secuencialField;
      set => this.secuencialField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string dirMatriz
    {
      get => this.dirMatrizField;
      set => this.dirMatrizField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string regimenMicroempresas
    {
      get => this.regimenMicroempresasField;
      set => this.regimenMicroempresasField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string agenteRetencion
    {
      get => this.agenteRetencionField;
      set => this.agenteRetencionField = value;
    }
  }
}
