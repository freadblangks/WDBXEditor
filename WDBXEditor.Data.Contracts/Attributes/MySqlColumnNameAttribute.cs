using System;

namespace Acmil.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies the name of the associated column in the MySQL database.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class MySqlColumnNameAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of <see cref="MySqlColumnNameAttribute"/>.
		/// </summary>
		/// <param name="name">The name of the corresponding MySQL column (case-sensitive).</param>
		public MySqlColumnNameAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// The name of the column in MySQL associated with the field or property.
		/// </summary>
		public string Name { get; private set; }
	}
}
