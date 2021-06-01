using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityComponents.RES
{
    public class convert_bool_int
    {
        public convert_bool_int() { }
        public static int intReturn(string boInputValue)
        {
            int intOutValue = 0;
            if (boInputValue == "True")
                intOutValue = 1;
            return intOutValue;
        }
    }
}
