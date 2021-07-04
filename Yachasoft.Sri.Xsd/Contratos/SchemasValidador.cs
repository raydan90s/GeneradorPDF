// Decompiled with JetBrains decompiler
// Type: Yachasoft.Sri.Xsd.Contratos.SchemasValidador
// Assembly: Yachasoft.Sri.Xsd, Version=1.1.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F09EA61F-E573-4F61-BA4F-32EF8FF0528A
// Assembly location: C:\Users\Joseph\.nuget\packages\infoware.sri.xsds\1.1.5\lib\net5.0\Yachasoft.Sri.Xsd.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Yachasoft.Sri.Xsd.Contratos
{
  public static class SchemasValidador
  {
    public static void Factura_1_0_0(
      XmlDocument firmado,
      ValidationEventHandler validationEventHandler)
    {
      XmlSchema schema1 = XmlSchema.Read(SchemasValidador.GetSchemaStream("xmldsig-core-schema.xsd"), validationEventHandler);
      XmlSchema schema2 = XmlSchema.Read(SchemasValidador.GetSchemaStream("factura_V1.0.0.xsd"), validationEventHandler);
      firmado.Schemas.Add(schema1);
      firmado.Schemas.Add(schema2);
      firmado.Validate(validationEventHandler);
    }

    public static void GuiaRemision_1_0_0(
      XmlDocument firmado,
      ValidationEventHandler validationEventHandler)
    {
      XmlSchema schema1 = XmlSchema.Read(SchemasValidador.GetSchemaStream("xmldsig-core-schema.xsd"), validationEventHandler);
      XmlSchema schema2 = XmlSchema.Read(SchemasValidador.GetSchemaStream("guiaRemision_V1.0.0.xsd"), validationEventHandler);
      firmado.Schemas.Add(schema1);
      firmado.Schemas.Add(schema2);
      firmado.Validate(validationEventHandler);
    }

    public static void ComprobanteRetencion_1_0_0(
      XmlDocument firmado,
      ValidationEventHandler validationEventHandler)
    {
      XmlSchema schema1 = XmlSchema.Read(SchemasValidador.GetSchemaStream("xmldsig-core-schema.xsd"), validationEventHandler);
      XmlSchema schema2 = XmlSchema.Read(SchemasValidador.GetSchemaStream("comprobanteRetencion_V1.0.0.xsd"), validationEventHandler);
      firmado.Schemas.Add(schema1);
      firmado.Schemas.Add(schema2);
      firmado.Validate(validationEventHandler);
    }

    public static void NotaDebito_1_0_0(
      XmlDocument firmado,
      ValidationEventHandler validationEventHandler)
    {
      XmlSchema schema1 = XmlSchema.Read(SchemasValidador.GetSchemaStream("xmldsig-core-schema.xsd"), validationEventHandler);
      XmlSchema schema2 = XmlSchema.Read(SchemasValidador.GetSchemaStream("notaDebito_V1.0.0.xsd"), validationEventHandler);
      firmado.Schemas.Add(schema1);
      firmado.Schemas.Add(schema2);
      firmado.Validate(validationEventHandler);
    }

    public static void NotaCredito_1_0_0(
      XmlDocument firmado,
      ValidationEventHandler validationEventHandler)
    {
      XmlSchema schema1 = XmlSchema.Read(SchemasValidador.GetSchemaStream("xmldsig-core-schema.xsd"), validationEventHandler);
      XmlSchema schema2 = XmlSchema.Read(SchemasValidador.GetSchemaStream("notaCredito_V1.0.0.xsd"), validationEventHandler);
      firmado.Schemas.Add(schema1);
      firmado.Schemas.Add(schema2);
      firmado.Validate(validationEventHandler);
    }

    public static void LiquidacionCompra_1_0_0(
      XmlDocument firmado,
      ValidationEventHandler validationEventHandler)
    {
      XmlSchema schema1 = XmlSchema.Read(SchemasValidador.GetSchemaStream("xmldsig-core-schema.xsd"), validationEventHandler);
      XmlSchema schema2 = XmlSchema.Read(SchemasValidador.GetSchemaStream("liquidacionCompra_V1.0.0.xsd"), validationEventHandler);
      firmado.Schemas.Add(schema1);
      firmado.Schemas.Add(schema2);
      firmado.Validate(validationEventHandler);
    }

    internal static Stream GetSchemaStream(string relativeFileName)
    {
      string name = ((IEnumerable<string>) Assembly.GetExecutingAssembly().GetManifestResourceNames()).FirstOrDefault<string>((Func<string, bool>) (p => p.EndsWith(relativeFileName)));
      return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
    }
  }
}
