// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.NotaDebito_1_0_0.KeyInfoType
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.NotaDebito_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [XmlRoot("KeyInfo", IsNullable = false, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [Serializable]
  public class KeyInfoType
  {
    private object[] itemsField;
    private ItemsChoiceType2[] itemsElementNameField;
    private string[] textField;
    private string idField;

    [XmlAnyElement]
    [XmlElement("KeyName", typeof (string))]
    [XmlElement("KeyValue", typeof (KeyValueType))]
    [XmlElement("MgmtData", typeof (string))]
    [XmlElement("PGPData", typeof (PGPDataType))]
    [XmlElement("RetrievalMethod", typeof (RetrievalMethodType))]
    [XmlElement("SPKIData", typeof (SPKIDataType))]
    [XmlElement("X509Data", typeof (X509DataType))]
    [XmlChoiceIdentifier("ItemsElementName")]
    public object[] Items
    {
      get => this.itemsField;
      set => this.itemsField = value;
    }

    [XmlElement("ItemsElementName")]
    [XmlIgnore]
    public ItemsChoiceType2[] ItemsElementName
    {
      get => this.itemsElementNameField;
      set => this.itemsElementNameField = value;
    }

    [XmlText]
    public string[] Text
    {
      get => this.textField;
      set => this.textField = value;
    }

    [XmlAttribute(DataType = "ID")]
    public string Id
    {
      get => this.idField;
      set => this.idField = value;
    }
  }
}
