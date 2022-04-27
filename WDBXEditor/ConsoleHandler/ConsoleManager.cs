using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDBXEditor.Common;
using WDBXEditor.Storage;

namespace WDBXEditor.ConsoleHandler
{
    public static class ConsoleManager
    {
        public static bool ConsoleMode { get; set; } = false;

        public static Dictionary<string, HandleCommand> CommandHandlers = new Dictionary<string, HandleCommand>();
        public delegate void HandleCommand(string[] args);

        public static void ConsoleMain(string[] args)
        {
            Database.LoadDefinitions().Wait();

            if (CommandHandlers.ContainsKey(args[0].ToLower()))
                InvokeHandler(args[0], args.Skip(1).ToArray());
        }

        public static bool InvokeHandler(string command, params string[] args)
        {
            try
            {
                if (!Directory.Exists(Constants.TEMP_FOLDER))
                    Directory.CreateDirectory(Constants.TEMP_FOLDER);

                command = command.ToLower();
                if (CommandHandlers.ContainsKey(command))
                {
                    CommandHandlers[command].Invoke(args);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("");
                return false;
            }
        }

        public static void LoadCommandDefinitions()
        {
            //Argument commands
            //DefineCommand("-console", ConsoleManager.LoadConsoleMode);
            DefineCommand("-export", ConsoleCommands.ExportArgCommand);
            DefineCommand("-sqldump", ConsoleCommands.SqlDumpArgCommand);
            DefineCommand("-extract", ConsoleCommands.ExtractCommand);
            DefineCommand("-sqlimport", ConsoleCommands.SqlImportArgCommand);
        }

        /// <summary>
        /// Converts args into keyvalue pair
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseCommand(string[] args)
        {
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                if (i == args.Length - 1)
                    break;

                string key = args[i].ToLower();
                string value = args[++i];
                if (value[0] == '"' && value[value.Length - 1] == '"')
                    value = value.Substring(1, value.Length - 2);

                keyvalues.Add(key, value);
            }

            return keyvalues;
        }

        private static void DefineCommand(string command, HandleCommand handler)
        {
            CommandHandlers[command.ToLower()] = handler;
        }
    }
}
