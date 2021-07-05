using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.Core.Helpers
{
    public static class DigitoVerificadorHelper
    {
        public static int ObtenerModulo11(this string cadenaNumeros)
        {
            int baseMax = 7;
            int multiplicador = 2;
            int total = 0;
            string[] substrings = System.Text.RegularExpressions.Regex.Split(cadenaNumeros, "");

            for (int i = substrings.Length - 1; i >= 1; i--)
            {
                if (substrings[i] != "")
                {
                    if (multiplicador > baseMax)
                    {
                        multiplicador = 2;
                    }
                    int numAux = int.Parse(substrings[i]);
                    total += (numAux * multiplicador);
                    multiplicador += 1;
                }
            }

            int verificador = 11 - total % 11;

            return CheckDigitBring(verificador);
        }

        private static int CheckDigitBring(int digit)
        {
            if (digit == 10)
                digit = 1;
            else if (digit == 11)
                    digit = 0;
            return digit;
        }

        public static int ObtenerModulo10(this string cadenaNumeros)
        {
            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int index = 0;
            int suma = 0;
            foreach (char ch in ((IEnumerable<char>)cadenaNumeros.ToCharArray()).Reverse())
            {
                int producto = int.Parse(ch.ToString()) * int.Parse(coeficientes[index].ToString());
                suma += (producto < 10 ? producto : producto - 9);
                index++;
            }
            int residuo = suma % 10;
            return residuo != 0 ? 10 - residuo : 0;
        }
    }
}
