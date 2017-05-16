using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using RegexConsoleTester.Utils;

namespace Contracts
{

    //How to decide when to use abstract class or interface.
    //if you are creating something that provides common functionality to unrelated classes, use an interface.
    //If you are creating something for objects that are closely related in a hierarchy, use an abstract class.

    public abstract class RegexBase
    {

        public string RegexName { get; set; }

        [Required(ErrorMessage = "Enter Regex ")]
        [Display(Name = "Regex Code")]
        public string RegexCodeRQ { get; set; }

        [Required(ErrorMessage = "Enter Text")]
        [Display(Name = "Text")]
        public string RegexTextRQ { get; set; }

        public RegexBase()
        {
            //Abstrack Factrory Method Pattern :
            //Define an interface for creating an object, but let subclasses decide which class to instantiate. Factory Method lets a class defer instantiation to subclasses.
            ExecPreReq();
        }

        public abstract void ExecPreReq();
        public abstract void ExecRegex();


        /// <summary>
        /// Show regex result
        /// </summary>
        /// <param name="regexCode"></param>
        /// <param name="orginalText"></param>
        /// <param name="extra"></param>
        public virtual void ShowResult(string regexCode, string orginalText, string afterRegexText, Dictionary<string, string> extra = null)
        {

            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine(string.Format(ConsoleWriteTemplate.TitleNewLine, "Regex Code", regexCode));             
            Console.WriteLine(string.Format(ConsoleWriteTemplate.TitleNewLine, "Text", orginalText)); 
            Console.WriteLine(string.Format(ConsoleWriteTemplate.Title, "Result", afterRegexText)); 
            
            if (extra!=null && extra.Count>0)
            {

                foreach (KeyValuePair<string, string> item in extra)
                {
                    Console.WriteLine(string.Format(ConsoleWriteTemplate.Title, item.Key, item.Value));                     
                }
                
            }

        }

    }
}
