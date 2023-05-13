namespace WDBXEditor.Core.Common.Enums
{
	/// <summary>
	/// Import flags used by CSV import.
	/// </summary>
	public enum ImportFlags
	{
		// TODO: Determine if the [Flags] attribute can be added to this for clarity.

		/// <summary>
		/// No flags set. This is not an option in the UI and exists purely as a default value.
		/// </summary>
		None = 0,

		/// <summary>
		/// Flag indicating that duplicate IDs are to be handled by assigning the row a new ID.
		/// </summary>
		FixIds = 1,

		/// <summary>
		/// Flag indicating that duplicate IDs are to be handled by taking the newer row.
		/// </summary>
		TakeNewest = 2
	}
}
