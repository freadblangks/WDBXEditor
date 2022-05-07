using System;

namespace WDBXEditor.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies the name of the associated column in the MySQL database.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class MySqlColumnNameAttribute : Attribute
	{
		public readonly string Name;

		public MySqlColumnNameAttribute(string name)
		{
			Name = name;
		}
	}
}
