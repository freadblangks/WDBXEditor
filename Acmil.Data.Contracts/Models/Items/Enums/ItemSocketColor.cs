namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// The different possible colors for a gem socket on an item.
	/// </summary>
	/// TODO: Determine if this is meant to be a bitmask.
	public enum ItemSocketColor : sbyte
	{
		/// <summary>
		/// The socket is meant for Meta gems.
		/// </summary>
		Meta = 1,

		/// <summary>
		/// The socket is meant for Red gems.
		/// </summary>
		Red = 2,

		/// <summary>
		/// The socket is meant for Yellow gems.
		/// </summary>
		Yellow = 4,

		/// <summary>
		/// The socket is meant for Blue gems.
		/// </summary>
		Blue = 8
	}
}
