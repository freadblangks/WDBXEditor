using System;
using System.Collections.Generic;
using System.Text;

namespace WDBXEditor.Extended.Shell.Helpers.Commands
{
	/// <summary>
	/// Helper class for working with commands.
	/// </summary>
	public static class CommandHelper
	{
		private static readonly Dictionary<string, CommandHandler> CommandHandlers = new Dictionary<string, CommandHandler>();
		private delegate object CommandHandler(string[] args);

		/// <summary>
		/// Parses the arguments passed to the main entry point.
		/// </summary>
		/// <param name="args">A list of arguments passed to the main entry point.</param>
		public static void ParseMainArgs(string[] args)
		{

		}

		public static void LoadCommandDefinitions()
		{

		}
	}
}
