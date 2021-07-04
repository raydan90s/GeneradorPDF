using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachasoft.Sri.Core.Helpers
{
    public static class DigitoVerificadorHelper
    {
        public static long ObtenerModulo11(this string cadenaNumeros)
        {
            string str = "234567";
            int index = -1;
            long num1 = 0;
            foreach (char ch in ((IEnumerable<char>)cadenaNumeros.ToCharArray()).Reverse<char>())
            {
                index = index + 1 >= str.Length ? 0 : index + 1;
                num1 += (long)(int.Parse(ch.ToString()) * int.Parse(str[index].ToString()));
            }
            long num2 = num1 % 11L;
            return num2 != 0L ? 11L - num2 : 0L;
        }

        public static long ObtenerModulo10(this string cadenaNumeros)
        {
            string str = "21";
            int index = -1;
            long num1 = 0;
            foreach (char ch in ((IEnumerable<char>)cadenaNumeros.ToCharArray()).Reverse<char>())
            {
                index = index + 1 >= str.Length ? 0 : index + 1;
                int num2 = int.Parse(ch.ToString()) * int.Parse(str[index].ToString());
                int num3 = num2 < 10 ? num2 : num2 - 9;
                num1 += (long)num3;
            }
            long num4 = num1 % 10L;
            return num4 != 0L ? 10L - num4 : 0L;
        }
    }
}
