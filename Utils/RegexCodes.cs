using System;
using System.Collections.Generic;
using System.Text;

namespace RegexConsoleTester.Utils
{
    public static class RegexCodes
    {
        
        public const string Integer = "^[0-9]*$"; //integer
        public const string Url = "([a-zA-Z0-9]+://)?([a-zA-Z0-9_]+:[a-zA-Z0-9_]+@)?([a-zA-Z0-9.-]+\\.[A-Za-z]{2,4})(:[0-9]+)?([^ ])+"; //Url, www.google.com, https://google.com?ty=1 etc.
        

    }
}
