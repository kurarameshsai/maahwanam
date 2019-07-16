using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MaaAahwanam.Utility
{
    public class ValidationsUtility
    {
        public static string PatternforPassword()
        {
            //string reg = @"^[a-zA-Z]{1}[a-zA-Z0-9.,#%*!$;]+$";
            //string reg = @"(/^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d][A-Za-z\d!@#$%^&*()_+]{7,19}$/)";
            string reg = @"([a-zA-Z]{1}(?=.*\d)(?=.*[0-9])(?=.*[@#$%]).{7,20})";
            return reg;
        }
    }
}
