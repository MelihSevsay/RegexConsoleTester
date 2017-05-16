using System;
using System.Reflection;
using System.Linq;
using PreDefinedRegex;
using Contracts;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Concurrent;
using RegexConsoleTester.Utils;
using System.Text.RegularExpressions;
using RegexConsoleTester.ConsoleHelper;

namespace RegexConsoleTester
{

    public class Program
    {

        public static ConcurrentDictionary<int, string> CommandClassList { get; set; }

        public static int Main(String[] args)
        {
            //Farklı karakterlerde çıktı basabilmek için.
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //Get ReferanceTypeRegexs
            CommandClassList = Helper.GetRefranceTypeCommands();

            try
            {

                ProcessArgs(new string[] { "--help" });

                int exitCode = 0;
                while (exitCode == 0)
                {                    
                    var values = Console.ReadLine();
                    if (values != null && !string.IsNullOrEmpty(values))
                    {
                        exitCode = ProcessArgs(new string[] { values });
                    }
                }

                return exitCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return 1;
            }

        }

        internal static int ProcessArgs(string[] args)
        {

            var command = string.Empty;
            var lastArg = 0;
            for (; lastArg < args.Length; lastArg++)
            {

                var cmdClassKeyValueItem = IsReferanceTypeCmd(args[lastArg]);
                if (cmdClassKeyValueItem.Value != null)
                {
                    string className = (cmdClassKeyValueItem.Value);
                    Type type = Type.GetType("PreDefinedRegex." + className);

                    //Create RegexCommand Class object to Run desired package regex.
                    Activator.CreateInstance(type);

                    return 0; //Repeat
                }
                else
                {

                    var isPrmtv = IsPrimitiveCmd(args[lastArg]);

                    switch (args[lastArg])
                    {
                        
                        case "exit":
                        case "-ext":
                        case "--exit":
                            Environment.Exit(0);
                            break;
                        case "cls":
                        case "-cls":
                        case "--clear":
                            Console.Clear();
                            break;
                        case "-h":
                        case "--help":
                            Console.Clear();
                            Helper.PrintHelp(CommandClassList);
                            break;
                        default:
                            Helper.NotValidArgument();                            
                            break;
                    }

                    return 0;
                }


            }

            return -1;
        }
                
        #region ProcessArgs Fnc

        public static KeyValuePair<int, string> IsReferanceTypeCmd(string gArg)
        {

            KeyValuePair<int, string> cmdClassKeyValueItem;
            bool isInt = Regex.IsMatch(gArg, RegexCodes.Integer);
            if (isInt)
            {
                //entered value is ineger
                cmdClassKeyValueItem = CommandClassList.Where(x => x.Key == Convert.ToInt32(gArg)).FirstOrDefault();
            }
            else
            {
                //entered valur is string
                gArg = gArg.Replace("-", "");                 
                cmdClassKeyValueItem = CommandClassList.Where(x => x.Value == gArg).FirstOrDefault();
            }

            return cmdClassKeyValueItem;

        }

        public static bool IsPrimitiveCmd(string gArg)
        {
            return gArg.Length > 0 && gArg.Contains("--");
        }

        #endregion
        
    }






}