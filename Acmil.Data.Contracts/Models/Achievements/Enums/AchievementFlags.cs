using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Achievements.Enums
{
	/// <summary>
	/// Flags that are used to define specific aspects of an Achievement's or Statistic's behavior.
	/// </summary>
	[Flags]
	public enum AchievementFlags : uint
	{
		/// <summary>
		/// The Achievement is a statistic.
		/// </summary>
		Statistic = 0x001,

		// TODO: Figure out what the hell this means. It might not be implemented.
		/// <summary>
		/// The Achievement is client-side only.
		/// </summary>
		Hidden = 0x002,

		/// <summary>
		/// The normal "Achievement Earned" notification is not played.
		/// </summary>
		NoToast = 0x004,

		/// <summary>
		/// The value displayed is the total of all criteria met from all requirements.
		/// </summary>
		Cumulative = 0x008,

		/// <summary>
		/// Whether the player's highest achieved meeting of the criteria should be displayed.
		/// </summary>
		/// <remarks>
		/// This is used for Statistics and Achievements where you want to display the player's
		/// "high score" (e.g. "Wrecking Ball").
		/// </remarks>
		DisplayHighest = 0x010,

		// TODO: Figure out why this is used for statistics.
		/// <summary>
		/// The Achievement requires some but not all of its criteria to be met.
		/// </summary>
		/// <remarks>
		/// This is also used for some Statistics, but its purpose is unknown.
		/// </remarks>
		CriteriaCount = 0x020,

		/// <summary>
		/// The Statistic is an average value (i.e. value / time in days).
		/// </summary>
		AveragePerDay = 0x040,

		/// <summary>
		/// The Achievement shows progress with a progress bar.
		/// </summary>
		HasProgressBar = 0x080,

		/// <summary>
		/// The Achievement is a "Realm First!" Achievement.
		/// It can only be earned by one player per realm.
		/// </summary>
		RealmFirst = 0x100,

		// TODO: Determine if this applies to achievements that aren't
		// for killing raid bosses.
		/// <summary>
		/// The Achievement is a "Realm First!" kill Achievement.
		/// It can only be earned by one instance group per realm.
		/// </summary>
		RealmFirstKill = 0x200,

		// Not sure, but this is possibly for Realm First! Achievements.
		/// <summary>
		/// (NOT IMPLEMENTED IN 3.3.5a)
		/// </summary>
		[NotImplemented]
		HideNameInTie = 0x400,

		/// <summary>
		/// (NOT IMPLEMENTED IN 3.3.5a) The Achievement is hidden in the UI until awarded.
		/// </summary>
		[NotImplemented]
		HiddenTillAwarded = 0x800
	}
}
