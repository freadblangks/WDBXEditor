using System;

namespace WDBXEditor.Data.Contexts
{
	/// <summary>
	/// Settings for a MySQL bulk-load operation.
	/// </summary>
	public class MySqlBulkLoadSettings
	{
		/// <summary>
		/// The name of the table to bulk load into.
		/// </summary>
		public string TableName { get; set; }

		/// <summary>
		/// The character or string used to delimit fields in the file.
		/// </summary>
		public string FieldTerminator { get; set; }

		/// <summary>
		/// The character or string used to indicate line endings in the file.
		/// </summary>
		public string LineTerminator { get; set; } = Environment.NewLine;

		/// <summary>
		/// The number of lines to skip at the start of the file being loaded.
		/// </summary>
		public int NumberOfLinesToSkip { get; set; } = 0;

		/// <summary>
		/// The path to the file to load.
		/// </summary>
		public string FilePath { get; set; }

		/// <summary>
		/// The character to use for quotes.
		/// </summary>
		public char FieldQuotationCharacter { get; set; } = '"';

		/// <summary>
		/// The character set to use when loading the file. Defaults to "UTF8".
		/// </summary>
		public string CharacterSet { get; set; } = "UTF8";
	}
}
