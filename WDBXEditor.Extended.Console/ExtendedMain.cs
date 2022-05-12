using System;
using System.Collections.Generic;
using System.Text;
using WDBXEditor.Extended.Api;
using WDBXEditor.Extended.Api.Managers;

namespace WDBXEditor.Extended.Shell
{
	public class ExtendedMain
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello good world");
			var api = new ItemTemplateManager();
			api.TestGetItemTemplate();
		}
	}
}
