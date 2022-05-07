using System;
using System.Collections.Generic;
using System.Text;
using WDBXEditor.Data.Contracts.Attributes;

namespace WDBXEditor.Data.Contracts.Models.Items.Submodels
{
	/// <summary>
	/// Object containing the different levels of magic resistance
	/// an item provides when equipped.
	/// </summary>
	public class ItemResistances
	{
		/// <summary>
		/// The amount of resistance to Holy damage the item provides when equipped.
		/// </summary>
		/// TODO: Determine if this is actually implemented.
		[MySqlColumnName("holy_res")]
		public byte Holy { get; set; } = 0;

		/// <summary>
		/// The amount of resistance to Fire damage the item provides when equipped.
		/// </summary>
		[MySqlColumnName("fire_res")]
		public byte Fire { get; set; } = 0;

		/// <summary>
		/// The amount of resistance to Nature damage the item provides when equipped.
		/// </summary>
		[MySqlColumnName("nature_res")]
		public byte Nature { get; set; } = 0;

		/// <summary>
		/// The amount of resistance to Frost damage the item provides when equipped.
		/// </summary>
		[MySqlColumnName("frost_res")]
		public byte Frost { get; set; } = 0;

		/// <summary>
		/// The amount of resistance to Shadow damage the item provides when equipped.
		/// </summary>
		[MySqlColumnName("shadow_res")]
		public byte Shadow { get; set; } = 0;

		/// <summary>
		/// The amount of resistance to Arcane damage the item provides when equipped.
		/// </summary>
		[MySqlColumnName("arcane_res")]
		public byte Arcane { get; set; } = 0;
	}
}
