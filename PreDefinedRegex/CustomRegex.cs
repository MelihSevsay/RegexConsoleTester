using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Contracts;

namespace PreDefinedRegex
{
    public class CustomRegex : RegexBase
    {

        public CustomRegex()
        {
            ExecRegex();
        }

        public CustomRegex(string regexName, string regexCode, string text)
        {

            RegexName = regexName;
            RegexCodeRQ = regexCode;
            RegexTextRQ = text;

            ExecRegex();

        }
                
        public override void ExecPreReq()
        {
            //gets the required properties to able get user input.
            var requiredVariablesList = base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.GetCustomAttribute<RequiredAttribute>() != null).ToList();

            //get user input those marked as Required.
            foreach (var item in requiredVariablesList)
            {

                var propDisplayName = item.GetCustomAttribute<DisplayAttribute>()?.GetName();
                var requiredMessage = item.GetCustomAttribute<RequiredAttribute>()?.ErrorMessage;

                Console.WriteLine($"{requiredMessage} : ");
                var value = Console.ReadLine();

                item.SetValue(this, value);

            }

        }

        public override void ExecRegex()
        {
            
            var urlRE = new Regex(RegexCodeRQ);
            Match match = urlRE.Match(RegexTextRQ);

            ShowResult(RegexCodeRQ, RegexTextRQ, match.Success.ToString());

        }

    }
}
