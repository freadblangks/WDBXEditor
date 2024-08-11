using Acmil.Data.Contracts.Models.Items.Enums;
using Acmil.Data.Contracts.Models.Items.Submodels;
using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Items
{
	/// <summary>
	/// Item template for equipable items that are held by the character, instead of worn.
	/// </summary>
	public class HeldArmamentEquipableItemTemplate : BaseArmamentEquipableItemTemplate
	{
		/// <summary>
		/// ID of a ??? used to override the sound the item (if it's a weapon) makes on impact.
		/// </summary>
		/// TODO: Figure out what IDs are used here.
		[MySqlColumnName("SoundOverrideSubclass")]
		public sbyte SoundOverrideSubclass { get; set; } = -1;

		/// <summary>
		/// An array of <see cref="ItemDamageDefinition"/> objects defining the damage dealt
		/// by the item if it's a weapon.
		/// </summary>
		[CompoundField(2)]
		public ItemDamageDefinition[] DamageDefinitions { get; set; } = new ItemDamageDefinition[2];

		/// <summary>
		/// The time (in milliseconds) between successive attacks.
		/// </summary>
		/// TODO: Determine if this affects ranged weapons.
		[MySqlColumnName("delay")]
		public ushort DelayBetweenAttacksInMilliseconds { get; set; } = 1000;

		/// <summary>
		/// An <see cref="ItemAmmoType"/> value indicating the type of ammunition the item uses.
		/// </summary>
		[MySqlColumnName("ammo_type")]
		[EnumType(typeof(ItemAmmoType))]
		[AllowEnumConversionOverride(false)]
		public byte AmmoType { get; set; } = 0;

		/// <summary>
		/// The range modifier for the weapon.
		/// </summary>
		/// TODO: Figure out what this means and what it's measured in.
		[MySqlColumnName("RangedModRange")]
		public float RangedModifier { get; set; } = 0;

		/// <summary>
		/// An <see cref="ItemSheathAnimation"/> value indicating the animation used when the player sheathes/unsheathes the item.
		/// </summary>
		[MySqlColumnName("sheath")]
		[EnumType(typeof(ItemSheathAnimation))]
		[AllowEnumConversionOverride(false)]
		public byte SheathAnimationId { get; set; } = 0;

		/// <summary>
		/// If the item is a shield , the block chance of the shield.
		/// </summary>
		/// TODO: Figure out what this means so we can explain what kind of value you need to pass.
		[MySqlColumnName("block")]
		public UInt24 BlockChance { get; set; } = 0;
	}
}
