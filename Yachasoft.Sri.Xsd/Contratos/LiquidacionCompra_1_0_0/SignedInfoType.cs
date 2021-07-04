// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.LiquidacionCompra_1_0_0.SignedInfoType
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
  [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [XmlRoot("SignedInfo", IsNullable = false, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [Serializable]
  public class SignedInfoType
  {
    private CanonicalizationMethodType canonicalizationMethodField;
    private SignatureMethodType signatureMethodField;
    private ReferenceType[] referenceField;
    private string idField;

    public CanonicalizationMethodType CanonicalizationMethod
    {
      get => this.canonicalizationMethodField;
      set => this.canonicalizationMethodField = value;
    }

    public SignatureMethodType SignatureMethod
    {
      get => this.signatureMethodField;
      set => this.signatureMethodField = value;
    }

    [XmlElement("Reference")]
    public ReferenceType[] Reference
    {
      get => this.referenceField;
      set => this.referenceField = value;
    }

    [XmlAttribute(DataType = "ID")]
    public string Id
    {
      get => this.idField;
      set => this.idField = value;
    }
  }
}
