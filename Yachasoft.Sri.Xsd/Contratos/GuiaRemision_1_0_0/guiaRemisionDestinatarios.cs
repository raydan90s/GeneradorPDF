// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0.guiaRemisionDestinatarios
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  [Serializable]
  public class guiaRemisionDestinatarios
  {
    private Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0.destinatario[] destinatarioField;

    [XmlElement("destinatario")]
    public Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0.destinatario[] destinatario
    {
      get => this.destinatarioField;
      set => this.destinatarioField = value;
    }
  }
}
