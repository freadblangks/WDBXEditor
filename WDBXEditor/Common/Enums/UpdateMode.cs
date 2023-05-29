namespace Acmil.Common.Enums
{
	/// <summary>
	/// Contains the different update strategies when importing a CSV or SQL table.
	/// </summary>
	public enum UpdateMode
	{
		/// <summary>
		/// Import new records only ("Import New" in the UI).
		/// </summary>
		Insert = 0,

		/// <summary>
		/// Import new records and update any existing ones that are different ("Update Existing" in the UI).
		/// </summary>
		Update = 1,

		/// <summary>
		/// Replace all data for the currently loaded DBC ("Override All" in the UI).
		/// </summary>
		Replace = 2
	}
}
