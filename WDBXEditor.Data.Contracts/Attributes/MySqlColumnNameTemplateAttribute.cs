using System;

namespace WDBXEditor.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies a string template for the name of the associated columns in the MySQL database.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class MySqlColumnNameTemplateAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of <see cref="MySqlColumnNameTemplateAttribute"/>.
		/// </summary>
		/// <param name="templateString">
		/// A string template for the associated MySQL column names (case-sensitive).
		/// Templatized numbers are specified with [m:n], where m is the number in the first column name and n is the number in the last column name.
		/// </param>
		public MySqlColumnNameTemplateAttribute(string templateString)
		{
			TemplateString = templateString;
		}

		/// <summary>
		/// A string specifying the shape of the corresponding column names in MySQL.
		/// </summary>
		public string TemplateString { get; private set; }
	}
}
