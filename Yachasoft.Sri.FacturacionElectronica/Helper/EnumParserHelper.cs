using System;
using System.Reflection;
using Yachasoft.Sri.Core.Atributos;
using Yachasoft.Sri.Core.Enumerados;
using Yachasoft.Sri.Modelos.Enumerados;
using Yachasoft.Core.Extensions;

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
            throw new ArgumentException($"Forma de pago inválida: {formaPago}");
        }

        public static EnumTipoImpuestoIVA ParseCodigoIVA(string codigo)
        {
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoImpuestoIVA>(codigo);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }

            if (Enum.TryParse<EnumTipoImpuestoIVA>(codigo, true, out var resultado))
            {
                return resultado;
            }

            throw new ArgumentException($"Código de IVA inválido: {codigo}");
        }


        public static EnumTipoDocumento ParseTipoDocumento(string tipoDocumento)
        {
            var enumValue = BuscarEnumPorSRICodigo<EnumTipoDocumento>(tipoDocumento);
            if (enumValue.HasValue)
            {
                return enumValue.Value;
            }

            if (Enum.TryParse<EnumTipoDocumento>(tipoDocumento, true, out var resultado))
            {
                return resultado;
            }

            throw new ArgumentException($"Tipo de documento inválido: {tipoDocumento}");
        }
    }
}