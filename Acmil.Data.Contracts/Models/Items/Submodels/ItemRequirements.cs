using Acmil.Data.Contracts.Models.Items.Submodels.Requirements;
using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Items.Submodels
{
	/// <summary>
	/// Object used for defining item requirements.
	/// </summary>
	public class ItemRequirements
	{
		/// <summary>
		/// The level required to use the item.
		/// </summary>
		[MySqlColumnName("RequiredLevel")]
		public byte RequiredLevel { get; set; } = 0;

		/// <summary>
		/// The skill and skill level required to use the item.
		/// </summary>
		[CompoundField]
		public ItemRequiredSkill RequiredSkill { get; set; } = new ItemRequiredSkill();

		/// <summary>
		/// The ID of the spell required to use the item.
		/// </summary>
		[MySqlColumnName("requiredspell")]
		public UInt24 RequiredSpellId { get; set; } = 0;

		[CompoundField]
		public ItemRequiredFaction RequiredFaction { get; set; } = new ItemRequiredFaction();
	}
}
