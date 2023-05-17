using Acmil.Data.Contracts.Models.Items.Enums;
using Acmil.Data.Contracts.Attributes;
using Acmil.Common.Utility.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Items.Submodels
{
	/// <summary>
	/// An object representing a Spell associated with an Item.
	/// </summary>
	public class ItemSpell
	{
		/// <summary>
		/// The ID of the Spell that the Item can cast, trigger, or teach.
		/// </summary>
		[MySqlColumnNameTemplate("spellid_[1:5]")]
		public Int24 Id { get; set; } = 0;

		/// <summary>
		/// A <see cref="ItemSpellTrigger"/> value indicating how the Spell specified by <see cref="Id"/> should be triggered.
		/// </summary>
		[MySqlColumnNameTemplate("spelltrigger_[1:5]")]
		[EnumType(typeof(ItemSpellTrigger))]
		[AllowEnumConversionOverride(false)]
		public byte Trigger { get; set; }

		/// <summary>
		/// The number of times that the Item can cast the Spell.
		/// If 0, infinite charges are possible.
		/// If negative, after the number of charges are depleted, the item is deleted as well.
		/// If positive, the item is not deleted after all the charges are spent.
		/// </summary>
		[MySqlColumnNameTemplate("spellcharges_[1:5]")]
		public short Charges { get; set; }

		/// <summary>
		/// The proc per minute rate controlling how often the spell is triggered if <see cref="Trigger"/> is set to <see cref="ItemSpellTrigger.ChanceOnHit"/>.
		/// </summary>
		[MySqlColumnNameTemplate("spellppmRate_[1:5]")]
		public float PpmRate { get; set; }

		/// <summary>
		/// The cooldown time (in milliseconds) for the spell specified by <see cref="Id"/>, controlling how often it can be used.
		/// Use -1 for the default Spell cooldown.
		/// </summary>
		/// <remarks>
		/// NOTE: <see cref="Cooldown"/> and <see cref="CategoryCooldown"/> are not mutually exclusive.
		/// </remarks>
		[MySqlColumnNameTemplate("spellcooldown_[1:5]")]
		public int Cooldown { get; set; }

		/// <summary>
		/// The category that the spell is grouped in.
		/// </summary>
		[MySqlColumnNameTemplate("spellcategory_[1:5]")]
		public ushort Category { get; set; }

		/// <summary>
		/// The cooldown time (in milliseconds) that is applied to all other spells in the category that the triggered spell is in.
		/// Use -1 for the default Spell cooldown.
		/// </summary>
		/// <remarks>
		/// NOTE: <see cref="Cooldown"/> and <see cref="CategoryCooldown"/> are not mutually exclusive.
		/// </remarks>
		[MySqlColumnNameTemplate("spellcategorycooldown_[1:5]")]
		public int CategoryCooldown { get; set; }
	}
}
