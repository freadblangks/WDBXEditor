using Acmil.Data.Contracts.Models.Achievements;

namespace Acmil.Data.Repositories.Interfaces
{
	/// <summary>
	/// Interface describing a repository class for interacting with Achievements.
	/// </summary>
	public interface IAchievementRepository
	{
		/// <summary>
		/// Reads an Achievement from the database.
		/// </summary>
		/// <param name="achievementId">The ID of the Achievement.</param>
		/// <returns>An object representing the Achievement.</returns>
		public Achievement ReadAchievement(ushort achievementId);
	}
}
