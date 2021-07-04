// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.NotaCredito_1_0_0.ReferenceType
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
  [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [XmlRoot("Reference", IsNullable = false, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [Serializable]
  public class ReferenceType
  {
    private TransformType[] transformsField;
    private DigestMethodType digestMethodField;
    private byte[] digestValueField;
    private string idField;
    private string uRIField;
    private string typeField;

    [XmlArrayItem("Transform", IsNullable = false)]
    public TransformType[] Transforms
    {
      get => this.transformsField;
      set => this.transformsField = value;
    }

    public DigestMethodType DigestMethod
    {
      get => this.digestMethodField;
      set => this.digestMethodField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] DigestValue
    {
      get => this.digestValueField;
      set => this.digestValueField = value;
    }

    [XmlAttribute(DataType = "ID")]
    public string Id
    {
      get => this.idField;
      set => this.idField = value;
    }

    [XmlAttribute(DataType = "anyURI")]
    public string URI
    {
      get => this.uRIField;
      set => this.uRIField = value;
    }

    [XmlAttribute(DataType = "anyURI")]
    public string Type
    {
      get => this.typeField;
      set => this.typeField = value;
    }
  }
}
