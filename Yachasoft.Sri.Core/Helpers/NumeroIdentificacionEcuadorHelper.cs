using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachasoft.Sri.Core.Enumerados;

namespace Yachasoft.Sri.Core.Helpers
{
    public static class NumeroIdentificacionEcuadorHelper
    {
        public static bool EsValida(
          string Identificacion,
          EnumTipoIdentificacionSinConsumidorFinal tipoIdentificacion,
          out string MensajeError)
        {
            MensajeError = "";
            string Identificacion1 = Identificacion.ToString().Trim();
            try
            {
                if (tipoIdentificacion == EnumTipoIdentificacionSinConsumidorFinal.Cedula && Identificacion1.Length != 10)
                    throw new Exception("Cédula inválida");
                if (tipoIdentificacion == EnumTipoIdentificacionSinConsumidorFinal.RUC && Identificacion1.Length != 13)
                    throw new Exception("RUC inválido");
                //string MensajeError1;
                if (!EsValida(Identificacion1, out string MensajeError1))
                    throw new Exception(MensajeError1);
                return true;
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
            }
            return false;
        }

        public static bool EsValida(string Identificacion, out string MensajeError)
        {
            MensajeError = null;
            string str = Identificacion.ToString().Trim();
            try
            {
                if (!str.All<char>(new Func<char, bool>(char.IsDigit)) || str.Length != 10 && str.Length != 13)
                    return true;
                byte num1 = Convert.ToByte(str.Substring(0, 2));
                if (num1 < 1 || num1 > 24)
                    throw new Exception("Identificación inválida");
                byte num2 = Convert.ToByte(str.Substring(2, 1));
                if (str.Length == 13)
                {
                    if (num2 == 9)
                    {
                        RucJuridico(str, "RUC jurídico o extranjeros inválido");
                        return true;
                    }
                    if (num2 == 6)
                    {
                        RucPublico(str, "RUC público inválido");
                        return true;
                    }
                    RucPersonaNatural(str, "RUC persona natural inválido");
                    return true;
                }
                PersonaNatural(str, "Cédula inválida");
                return true;
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
            }
            return false;
        }

        private static void RucJuridico(string id, string mensajeError)
        {
            if (Convert.ToInt16(id.Substring(10, 3)) == 0)
                throw new Exception(mensajeError ?? "");
            if (id.Substring(0, 9).ObtenerModulo11().ToString() != id.Substring(9, 1))
                throw new Exception(mensajeError ?? "");
        }

        private static void RucPublico(string id, string mensajeError)
        {
            if (Convert.ToInt16(id.Substring(9, 4)) == 0)
                throw new Exception(mensajeError ?? "");
            if (id.Substring(0, 8).ObtenerModulo11().ToString() != id.Substring(8, 1))
                throw new Exception(mensajeError ?? "");
        }

        private static void RucPersonaNatural(string id, string mensajeError)
        {
            PersonaNatural(id, mensajeError);
            if (Convert.ToInt16(id.Substring(10, 3)) == 0)
                throw new Exception(mensajeError ?? "");
        }

        private static void PersonaNatural(string id, string mensajeError)
        {
            if (Convert.ToByte(id.Substring(2, 1)) >= 6)
                throw new Exception(mensajeError ?? "");
            if (id.Substring(0, 9).ObtenerModulo10().ToString() != id.Substring(9, 1))
                throw new Exception(mensajeError ?? "");
        }
    }
}
