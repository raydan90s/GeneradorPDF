using System;
using System.Reflection;
using Yachasoft.Sri.Core.Atributos;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Enumerados;


namespace Yachasoft.Sri.FacturacionElectronica.Services
{
    public static class EnumParserHelper
    {
        public static T? BuscarEnumPorSRICodigo<T>(string codigo) where T : struct, Enum
        {
            var enumType = typeof(T);
            foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var atributo = field.GetCustomAttribute<SRICodigoAttribute>();
                if (atributo != null && atributo.Code == codigo)
                {
                    return (T)field.GetValue(null);
                }
            }
            return null;
        }
        public static EnumTipoDocumento ParseTipoDocumento(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código de documento no puede estar vacío");

            return codigo switch
            {
                "01" => EnumTipoDocumento.Factura,
                "04" => EnumTipoDocumento.NotaCredito,
                "05" => EnumTipoDocumento.NotaDebito,
                "06" => EnumTipoDocumento.GuiaRemision,
                "07" => EnumTipoDocumento.ComprobanteRetencion,
                _ => throw new ArgumentException($"Código de documento no válido: {codigo}")
            };
        }

        public static EnumTipoAmbiente ParseTipoAmbiente(string tipoAmbiente)
        {
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoAmbiente>(tipoAmbiente);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }
            if (Enum.TryParse<EnumTipoAmbiente>(tipoAmbiente, true, out var resultado))
            {
                return resultado;
            }

            throw new ArgumentException($"Tipo de ambiente inválido: {tipoAmbiente}");
        }

        public static EnumTipoIdentificacion ParseTipoIdentificacion(string tipoIdentificacion)
        {
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoIdentificacion>(tipoIdentificacion);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }
            if (Enum.TryParse<EnumTipoIdentificacion>(tipoIdentificacion, true, out var resultado))
            {
                return resultado;
            }
            throw new ArgumentException($"Tipo de identificación inválido: {tipoIdentificacion}");
        }

        public static EnumTipoEmision ParseTipoEmision(string tipoEmision)
        {
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoEmision>(tipoEmision);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }
            if (Enum.TryParse<EnumTipoEmision>(tipoEmision, true, out var resultado))
            {
                return resultado;
            }
            throw new ArgumentException($"Tipo de emisión inválido: {tipoEmision}");
        }

        public static object ParseCodigoRetencion(string codigoRetencion)
        {
            var enumIVA = BuscarEnumPorSRICodigo<EnumTipoRetencionIVA>(codigoRetencion);
            if (enumIVA.HasValue)
            {
                Console.WriteLine($"Encontrado en IVA: {enumIVA.Value}");
                return enumIVA.Value;
            }
            var enumRenta = BuscarEnumPorSRICodigo<EnumTipoRetencionRenta>(codigoRetencion);
            if (enumRenta.HasValue)
            {
                Console.WriteLine($"Encontrado en Renta: {enumRenta.Value}");
                return enumRenta.Value;
            }
            if (Enum.TryParse<EnumTipoRetencionIVA>(codigoRetencion, true, out var iva))
            {
                return iva;
            }

            if (Enum.TryParse<EnumTipoRetencionRenta>(codigoRetencion, true, out var renta))
            {
                return renta;
            }
            throw new ArgumentException($"Código de retención inválido: '{codigoRetencion}'. No se encontró en ningún enum de retención.");
        }

        public static EnumTipoImpuestoIVA ParseCodigoIVA(string codigoIVA)
        {
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoImpuestoIVA>(codigoIVA);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }
            if (Enum.TryParse<EnumTipoImpuestoIVA>(codigoIVA, true, out var resultado))
            {
                return resultado;
            }
            throw new ArgumentException($"Código IVA inválido: '{codigoIVA}'");
        }

        public static EnumFormaPago ParseFormaPago(string formaPago)
        {
            var enumValue = BuscarEnumPorSRICodigo<EnumFormaPago>(formaPago);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }
            if (Enum.TryParse<EnumFormaPago>(formaPago, true, out var resultado))
            {
                return resultado;
            }
            throw new ArgumentException($"Forma de pago inválida: '{formaPago}'");
        }

    }
}