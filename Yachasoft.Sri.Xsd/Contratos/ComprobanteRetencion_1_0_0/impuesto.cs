// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.ComprobanteRetencion_1_0_0.impuesto
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
  public class impuesto
  {
    private string codigoField;
    private string codigoRetencionField;
    private Decimal baseImponibleField;
    private Decimal porcentajeRetenerField;
    private Decimal valorRetenidoField;
    private string codDocSustentoField;
    private string numDocSustentoField;
    private string fechaEmisionDocSustentoField;

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string codigo
    {
      get => this.codigoField;
      set => this.codigoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string codigoRetencion
    {
      get => this.codigoRetencionField;
      set => this.codigoRetencionField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public Decimal baseImponible
    {
      get => this.baseImponibleField;
      set => this.baseImponibleField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public Decimal porcentajeRetener
    {
      get => this.porcentajeRetenerField;
      set => this.porcentajeRetenerField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public Decimal valorRetenido
    {
      get => this.valorRetenidoField;
      set => this.valorRetenidoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string codDocSustento
    {
      get => this.codDocSustentoField;
      set => this.codDocSustentoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string numDocSustento
    {
      get => this.numDocSustentoField;
      set => this.numDocSustentoField = value;
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string fechaEmisionDocSustento
    {
      get => this.fechaEmisionDocSustentoField;
      set => this.fechaEmisionDocSustentoField = value;
    }
  }
}
