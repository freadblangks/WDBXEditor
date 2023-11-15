using Acmil.Data.Contracts.Attributes;


namespace Acmil.Data.Contracts.Models.Achievements
{
	/// <summary>
	/// Object used for defining the category an Achievement should display under in the player's UI.
	/// </summary>
	public class AchievementCategory
	{
		/// <summary>
		/// An ID uniquely identifying the category.
		/// </summary>
		[MySqlColumnName("ID")]
		public short CategoryId { get; internal set; }

		// TODO: Make sure they can't provide a value less than -1.
		/// <summary>
		/// The ID of the category's parent category.
		/// </summary>
		[MySqlColumnName("Parent")]
		public short ParentCategoryId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public AchievementCategory ParentCategory { get; internal set; }

		/// <summary>
		/// The localized name of the category.
		/// </summary>
		[MySqlColumnName("Name", isLocalized:true)]
		public string Name { get; set; }

		/// <summary>
		/// A value used to order the category under its parent. The value should be greater than 0.
		/// </summary>
		[MySqlColumnName("Ui_Order")]
		public sbyte UiOrder { get; set; }
	}
}
