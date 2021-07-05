using System;
using System.Linq;
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
                if (!EsValida(Identificacion1, out MensajeError))
                    throw new Exception(MensajeError);
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
            string identificacion = Identificacion.ToString().Trim();
            try
            {
                if (!identificacion.All(new Func<char, bool>(char.IsDigit)) || identificacion.Length != 10 && identificacion.Length != 13)
                    return true;
                byte provincia = Convert.ToByte(identificacion.Substring(0, 2));
                if (provincia < 1 || provincia > 24)
                    throw new Exception("Identificación inválida");
                if (identificacion.Length == 13)
                {
                    switch (Convert.ToByte(identificacion.Substring(2, 1)))
                    {
                        case 6: RucPublico(identificacion, "RUC público inválido"); break;
                        case 9: RucJuridico(identificacion, "RUC jurídico o extranjeros inválido"); break;
                        default: RucPersonaNatural(identificacion, "RUC persona natural inválido"); break;
                    }
                    return true;
                }
                PersonaNatural(identificacion, "Cédula inválida");
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
