// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.NotaCredito_1_0_0.SPKIDataType
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.NotaCredito_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [XmlRoot("SPKIData", IsNullable = false, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
  [Serializable]
  public class SPKIDataType
  {
    private byte[][] sPKISexpField;
    private XmlElement anyField;

    [XmlElement("SPKISexp", DataType = "base64Binary")]
    public byte[][] SPKISexp
    {
      get => this.sPKISexpField;
      set => this.sPKISexpField = value;
    }

    [XmlAnyElement]
    public XmlElement Any
    {
      get => this.anyField;
      set => this.anyField = value;
    }
  }
}
