// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.NotaDebito_1_0_0.notaDebito
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
  [XmlType(AnonymousType = true)]
  [XmlRoot(IsNullable = false, Namespace = "")]
  [Serializable]
  public class notaDebito
  {
    private infoTributaria infoTributariaField;
    private notaDebitoInfoNotaDebito infoNotaDebitoField;
    private notaDebitoMotivos motivosField;
    private maquinaFiscal maquinaFiscalField;
    private notaDebitoCampoAdicional[] infoAdicionalField;
    private SignatureType signatureField;
    private notaDebitoID idField;
    private bool idFieldSpecified;
    private string versionField;

    public infoTributaria infoTributaria
    {
      get => this.infoTributariaField;
      set => this.infoTributariaField = value;
    }

    public notaDebitoInfoNotaDebito infoNotaDebito
    {
      get => this.infoNotaDebitoField;
      set => this.infoNotaDebitoField = value;
    }

    public notaDebitoMotivos motivos
    {
      get => this.motivosField;
      set => this.motivosField = value;
    }

    public maquinaFiscal maquinaFiscal
    {
      get => this.maquinaFiscalField;
      set => this.maquinaFiscalField = value;
    }

    [XmlArrayItem("campoAdicional", IsNullable = false)]
    public notaDebitoCampoAdicional[] infoAdicional
    {
      get => this.infoAdicionalField;
      set => this.infoAdicionalField = value;
    }

    [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public SignatureType Signature
    {
      get => this.signatureField;
      set => this.signatureField = value;
    }

    [XmlAttribute]
    public notaDebitoID id
    {
      get => this.idField;
      set => this.idField = value;
    }

    [XmlIgnore]
    public bool idSpecified
    {
      get => this.idFieldSpecified;
      set => this.idFieldSpecified = value;
    }

    [XmlAttribute(DataType = "NMTOKEN")]
    public string version
    {
      get => this.versionField;
      set => this.versionField = value;
    }
  }
}
