using Acmil.Common.Utility.Configuration.SettingsModels.Locale.Enums;
using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements.Rewards
{
	/// <summary>
	/// An Achievement Reward where the player receives an item in the mail.
	/// </summary>
	public class ItemAchievementReward : BaseAchievementReward
	{
		/// <summary>
		/// The ID of the item to be mailed to players who earn the associated Achievement.
		/// </summary>
		[MySqlColumnName("ItemID")]
		public UInt24 ItemId { get; set; }

		/// <summary>
		/// The ID of the creature/NPC who will send the reward item to the player.
		/// </summary>
		/// <remarks>
		/// This ID is the `entry` column in the `creature_template` table.
		/// </remarks>
		[MySqlColumnName("Sender")]
		public UInt24 SenderId { get; set; }

		/// <summary>
		/// The locale associated with 
		/// </summary>
		[MySqlColumnName("Locale")]
		[EnumType(typeof(LocaleCode))]
		[AllowEnumConversionOverride(true)]
		public string Locale { get; set; }

		/// <summary>
		/// The text in the message's subject line 
		/// </summary>
		[MySqlColumnName("Subject")]
		public string MessageSubject { get; set; }

		/// <summary>
		/// The text in the body of the message.
		/// </summary>
		/// <remarks>To insert a line break, use '$B'.</remarks>
		[MySqlColumnName("Body")]
		public string MessageBody { get; set; }

		/// <summary>
		/// The ID of the mail template to use for the message.
		/// The <see cref="MessageSubject"/> and <see cref="MessageBody"/> properties must be empty for this to work.
		/// </summary>
		/// <remarks>
		/// The IDs are taken from MailTemplate.dbc.
		/// </remarks>
		[MySqlColumnName("MailTemplateID")]
		public UInt24 MailTemplateId { get; set; }

		public override string BuildRewardDescription(string rewardName) => $"Reward: {rewardName}";
	}
}
