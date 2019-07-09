using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Utilidades
{
    public class HexadecimalNotation
    {
        public string StringAHexa(string input)
        {
            string hexOutput = "";
            try
            {
                //string input = userCliente[0].CodigoCliente + "-" + userCliente[0].CodigoEntidad + "-" + DateTime.Now;

                char[] values = input.ToCharArray();
                foreach (char letter in values)
                {
                    // Get the integral value of the character.
                    int value = Convert.ToInt32(letter);
                    // Convert the decimal value to a hexadecimal value in string form.
                    hexOutput = hexOutput + String.Format("{0:X}", value);
                }
            }
            catch
            {
                return "-1";
            }
            return hexOutput;
        }

        public string HexaAString(string HexValue)
        {
            string StrValue = "";
            try
            {
                while (HexValue.Length > 0)
                {
                    StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                    HexValue = HexValue.Substring(2, HexValue.Length - 2);
                }
            }
            catch
            {
                return "-1";
            }
            return StrValue;
        }

    }

   
}
