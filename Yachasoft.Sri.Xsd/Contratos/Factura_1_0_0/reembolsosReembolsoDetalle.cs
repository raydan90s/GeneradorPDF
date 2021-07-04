// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0.reembolsosReembolsoDetalle
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Xsd.Contratos.Factura_1_0_0
{
  [GeneratedCode("xsd", "4.8.3928.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  [Serializable]
  public class reembolsosReembolsoDetalle
  {
    private string tipoIdentificacionProveedorReembolsoField;
    private string identificacionProveedorReembolsoField;
    private string codPaisPagoProveedorReembolsoField;
    private string tipoProveedorReembolsoField;
    private string codDocReembolsoField;
    private string estabDocReembolsoField;
    private string ptoEmiDocReembolsoField;
    private string secuencialDocReembolsoField;
    private string fechaEmisionDocReembolsoField;
    private string numeroautorizacionDocReembField;
    private detalleImpuestosDetalleImpuesto[] detalleImpuestosField;
    private compensacion[] compensacionesReembolsoField;

    public string tipoIdentificacionProveedorReembolso
    {
      get => this.tipoIdentificacionProveedorReembolsoField;
      set => this.tipoIdentificacionProveedorReembolsoField = value;
    }

    public string identificacionProveedorReembolso
    {
      get => this.identificacionProveedorReembolsoField;
      set => this.identificacionProveedorReembolsoField = value;
    }

    public string codPaisPagoProveedorReembolso
    {
      get => this.codPaisPagoProveedorReembolsoField;
      set => this.codPaisPagoProveedorReembolsoField = value;
    }

    public string tipoProveedorReembolso
    {
      get => this.tipoProveedorReembolsoField;
      set => this.tipoProveedorReembolsoField = value;
    }

    public string codDocReembolso
    {
      get => this.codDocReembolsoField;
      set => this.codDocReembolsoField = value;
    }

    public string estabDocReembolso
    {
      get => this.estabDocReembolsoField;
      set => this.estabDocReembolsoField = value;
    }

    public string ptoEmiDocReembolso
    {
      get => this.ptoEmiDocReembolsoField;
      set => this.ptoEmiDocReembolsoField = value;
    }

    public string secuencialDocReembolso
    {
      get => this.secuencialDocReembolsoField;
      set => this.secuencialDocReembolsoField = value;
    }

    public string fechaEmisionDocReembolso
    {
      get => this.fechaEmisionDocReembolsoField;
      set => this.fechaEmisionDocReembolsoField = value;
    }

    public string numeroautorizacionDocReemb
    {
      get => this.numeroautorizacionDocReembField;
      set => this.numeroautorizacionDocReembField = value;
    }

    [XmlArrayItem("detalleImpuesto", IsNullable = false)]
    public detalleImpuestosDetalleImpuesto[] detalleImpuestos
    {
      get => this.detalleImpuestosField;
      set => this.detalleImpuestosField = value;
    }

    [XmlArrayItem("compensacionReembolso", IsNullable = false)]
    public compensacion[] compensacionesReembolso
    {
      get => this.compensacionesReembolsoField;
      set => this.compensacionesReembolsoField = value;
    }
  }
}
