namespace WDBXEditor.Common.Enums
{
	/// <summary>
	/// The type of compression used to store data in a DBC, DB2, or ADB file.
	/// </summary>
	public enum CompressionType
	{
		// TODO: Provide actually helpful explanations for these.

		/// <summary>
		/// No compression used.
		/// </summary>
		None = 0,

		/// <summary>
		/// Immediate compression.
		/// </summary>
		Immediate = 1,

		/// <summary>
		/// Sparse compression.
		/// </summary>
		Sparse = 2,

		/// <summary>
		/// Pallet compression.
		/// </summary>
		Pallet = 3,

		/// <summary>
		/// Pallet Array compression.
		/// </summary>
		PalletArray = 4,

		/// <summary>
		/// Signed Immediate compression.
		/// </summary>
		SignedImmediate = 5
	}
}
