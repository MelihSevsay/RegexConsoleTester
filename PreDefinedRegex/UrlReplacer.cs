using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Contracts;
using RegexConsoleTester.Utils;

namespace PreDefinedRegex
{


    public class UrlReplacer : RegexBase
    {

        //Facytory Method
        public override void ExecPreReq()
        {
            RegexCodeRQ = RegexCodes.Url;
            RegexTextRQ = "merhaba arkdaslar bu örnek metin wwww.google.com somtimes heyecan dolu olursunuz sometimes unhappy http://www.jijoe.gt.uk/sali-oklava-2.html olsunmu olmasın mı bu dert sana uğramasın...";
        } 

        public UrlReplacer()
        {
            Console.WriteLine("Enter Text (If u wanna override text type else press enter) :");
            var value = Console.ReadLine();

            if (value != null && !string.IsNullOrEmpty(value))
            {
                RegexTextRQ = value;
            }

            ExecRegex();
        }
        
        public override void ExecRegex()
        {

            string replacedText = string.Empty;
            string reReplacedText = string.Empty;

            var htmlUrlReplacerOn = false;
            var urlReplaceTuple = GetUrlReplaced(RegexTextRQ, RegexCodeRQ);

            if (urlReplaceTuple?.Item1 != null && urlReplaceTuple.Item1.Count > 0)
            {
                htmlUrlReplacerOn = true;
                replacedText = urlReplaceTuple.Item2;
            }

            if (htmlUrlReplacerOn)
                reReplacedText = SetUrlReplaced(urlReplaceTuple);


            var extraOutput = new Dictionary<string, string>();
            extraOutput.Add("ReReplaced Text", reReplacedText);

            
            ShowResult(RegexCodeRQ, RegexTextRQ, replacedText, extraOutput);

        }
        
        #region UrlReplacer

        public static Tuple<Dictionary<int, string>, string> GetUrlReplaced(string text, string regex)
        {
            var urlRE = new Regex(regex);
            Match match = null;
            var i = 0;

            Dictionary<int, string> urlDictionary = new Dictionary<int, string>();

            do
            {
                match = urlRE.Match(text);
                if (!match.Success)
                    break;

                urlDictionary.Add(i, match.Value);
                text = text.Replace(match.Value, $"[~{i}~]");
                i++;
            }
            while (match.Success);

            return new Tuple<Dictionary<int, string>, string>(urlDictionary, text);

        }

        public static string SetUrlReplaced(Tuple<Dictionary<int, string>, string> tt)
        {

            if (tt?.Item1 != null)
            {
                var result = tt.Item2;
                for (int i = 0; i < tt.Item1.Count; i++)
                {
                    result = result.Replace($"[~{i}~]", tt.Item1[i]);
                }

                return result;
            }
            return "";

        }

        #endregion

    }

}
