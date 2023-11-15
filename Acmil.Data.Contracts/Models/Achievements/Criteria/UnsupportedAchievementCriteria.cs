using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement not supported by ACMIL.
	/// </summary>
	public class UnsupportedAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; }

		/// <summary>
		/// An unknown value from the `Asset_Id` column.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint AssetId { get; set; }

		/// <summary>
		/// An unknown value from the `Quantity` column.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Quantity { get; set; }

		/// <summary>
		/// An unknown value from the `Start_Event` column.
		/// </summary>
		[MySqlColumnName("Start_Event")]
		public uint StartEvent { get; set; }

		/// <summary>
		/// An unknown value from the `Start_Asset` column.
		/// </summary>
		[MySqlColumnName("Start_Asset")]
		public uint StartAsset { get; set; }

		/// <summary>
		/// An unknown value from the `Fail_Event` column.
		/// </summary>
		[MySqlColumnName("Fail_Event")]
		public uint FailEvent { get; set; }

		/// <summary>
		/// An unknown value from the `Fail_Asset` column.
		/// </summary>
		[MySqlColumnName("Fail_Asset")]
		public uint FailAsset { get; set; }
	}
}
