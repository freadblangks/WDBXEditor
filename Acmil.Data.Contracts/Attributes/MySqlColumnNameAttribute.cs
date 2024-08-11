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
		/// <param name="isLocalized">True if the column is for a localized string. Otherwise, false.</param>
		public MySqlColumnNameAttribute(string name, bool isLocalized = false)
		{
			Name = name;
			IsLocalized = isLocalized;
		}

		/// <summary>
		/// The name of the column in MySQL associated with the field or property.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// True if the column is for a localized string. Otherwise, false.
		/// </summary>
		/// <remarks>
		/// This will cause the column name to be interpreted as
		/// "&lt;ColumnName&gt;_Lang_&lt;LocaleCode (specified by "locale.localeCode" in config.json)&gt;"
		/// </remarks>
		public bool IsLocalized { get; private set; }
	}
}
