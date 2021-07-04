// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.ComprobanteRetencion_1_0_0.DSAKeyValueType
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.ComprobanteRetencion_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [XmlRoot("DSAKeyValue", IsNullable = false, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [Serializable]
  public class DSAKeyValueType
  {
    private byte[] pField;
    private byte[] qField;
    private byte[] gField;
    private byte[] yField;
    private byte[] jField;
    private byte[] seedField;
    private byte[] pgenCounterField;

    [XmlElement(DataType = "base64Binary")]
    public byte[] P
    {
      get => this.pField;
      set => this.pField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] Q
    {
      get => this.qField;
      set => this.qField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] G
    {
      get => this.gField;
      set => this.gField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] Y
    {
      get => this.yField;
      set => this.yField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] J
    {
      get => this.jField;
      set => this.jField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] Seed
    {
      get => this.seedField;
      set => this.seedField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] PgenCounter
    {
      get => this.pgenCounterField;
      set => this.pgenCounterField = value;
    }
  }
}
