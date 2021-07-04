// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.GuiaRemision_1_0_0.guiaRemisionInfoGuiaRemision
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
  public class guiaRemisionInfoGuiaRemision
  {
    private string dirEstablecimientoField;
    private string dirPartidaField;
    private string razonSocialTransportistaField;
    private string tipoIdentificacionTransportistaField;
    private string rucTransportistaField;
    private string riseField;
    private obligadoContabilidad obligadoContabilidadField;
    private bool obligadoContabilidadFieldSpecified;
    private string contribuyenteEspecialField;
    private string fechaIniTransporteField;
    private string fechaFinTransporteField;
    private string placaField;

    public string dirEstablecimiento
    {
      get => this.dirEstablecimientoField;
      set => this.dirEstablecimientoField = value;
    }

    public string dirPartida
    {
      get => this.dirPartidaField;
      set => this.dirPartidaField = value;
    }

    public string razonSocialTransportista
    {
      get => this.razonSocialTransportistaField;
      set => this.razonSocialTransportistaField = value;
    }

    public string tipoIdentificacionTransportista
    {
      get => this.tipoIdentificacionTransportistaField;
      set => this.tipoIdentificacionTransportistaField = value;
    }

    public string rucTransportista
    {
      get => this.rucTransportistaField;
      set => this.rucTransportistaField = value;
    }

    public string rise
    {
      get => this.riseField;
      set => this.riseField = value;
    }

    public obligadoContabilidad obligadoContabilidad
    {
      get => this.obligadoContabilidadField;
      set => this.obligadoContabilidadField = value;
    }

    [XmlIgnore]
    public bool obligadoContabilidadSpecified
    {
      get => this.obligadoContabilidadFieldSpecified;
      set => this.obligadoContabilidadFieldSpecified = value;
    }

    public string contribuyenteEspecial
    {
      get => this.contribuyenteEspecialField;
      set => this.contribuyenteEspecialField = value;
    }

    public string fechaIniTransporte
    {
      get => this.fechaIniTransporteField;
      set => this.fechaIniTransporteField = value;
    }

    public string fechaFinTransporte
    {
      get => this.fechaFinTransporteField;
      set => this.fechaFinTransporteField = value;
    }

    public string placa
    {
      get => this.placaField;
      set => this.placaField = value;
    }
  }
}
