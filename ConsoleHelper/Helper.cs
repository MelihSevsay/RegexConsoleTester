using Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RegexConsoleTester.ConsoleHelper
{
    public static class Helper
    {

        public static void NotValidArgument()
        {
            Console.WriteLine("Please enter valid options...");
        }

        public static void PrintHelp(ConcurrentDictionary<int, string> cmdClassList)
        {
            Console.WriteLine("########## RegexConsoleTester #############");
            Console.WriteLine("########## Options #############");
            
            //Center the text
            //Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));

            Console.WriteLine(ComposeHelp(cmdClassList));
            Console.WriteLine("################################");
        }

        public static string ComposeHelp(ConcurrentDictionary<int, string> cmdClassList)
        {
            StringBuilder commanText = new StringBuilder();

            commanText.Append("\n");
            int i = 0;
            foreach (KeyValuePair<int, string> item in cmdClassList)
            {
                commanText.Append($"[{i}] \t | --{item.Value}\n");
                i++;
            }

            commanText.Append($"[-h] \t | --help\n");
            commanText.Append($"[-cls] \t | --clear\n");
            commanText.Append($"[-ext] \t | --exit\n");

            return (commanText.ToString());
        }

        /// <summary>
        /// Collect referance type executeable regex options
        /// </summary>
        /// <returns></returns>
        public static ConcurrentDictionary<int, string> GetRefranceTypeCommands()
        {

            var CommandDictionary = new ConcurrentDictionary<int, string>();
            StringBuilder commanText = new StringBuilder();
            var instances = Assembly.GetEntryAssembly().GetTypes().Where(t => typeof(RegexBase).IsAssignableFrom(t) && t != typeof(RegexBase)).Select(x => x.Name).OrderBy(x => x).ToArray();


            for (int i = 0; i < instances.Length; i++)
            {
                var tt = instances[i];
                CommandDictionary.TryAdd(i, instances[i]);
            }

            return CommandDictionary;

        }


    }
}
