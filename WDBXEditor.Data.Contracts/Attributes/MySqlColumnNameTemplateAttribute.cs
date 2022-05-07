using System;

namespace WDBXEditor.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies a string template for the name of the associated columns in the MySQL database.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class MySqlColumnNameTemplateAttribute : Attribute
	{
		public readonly string TemplateString;

		public MySqlColumnNameTemplateAttribute(string templateString)
		{
			TemplateString = templateString;
		}
	}
}
