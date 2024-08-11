using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Items.Submodels.Requirements
{
	/// <summary>
	/// Object representing a required skill to use an item.
	/// </summary>
	public class ItemRequiredSkill
	{
		/// <summary>
		/// The ID of the skill required to use the item.
		/// </summary>
		[MySqlColumnName("RequiredSkill")]
		public ushort SkillLineId { get; set; }

		/// <summary>
		/// The required skill level required to use the item.
		/// </summary>
		[MySqlColumnName("RequiredSkillRank")]
		public ushort SkillRank { get; set; }
	}
}
