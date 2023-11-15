using Acmil.Data.Contracts.Models.Achievements.Criteria;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Helpers.Mapping;
using Acmil.Data.Repositories.Helpers;
using NUnit.Framework;

namespace Acmil.Data.Repositories.NUnit.Helpers
{
	[TestFixture]
	public class AchievementCriteriaTypeHelperTests
	{
		private AchievementCriteriaFactory _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new AchievementCriteriaFactory(new ModelCustomAttributeHelper());
		}

		[Test]
		public void AchievementCriteriaTypeHelper_GetAchievementCriteriaInstance_CorrectlyPopulatesOnlyQuantity()
		{
			// ARRANGE //
			byte expectedTypeId = (byte)AchievementCriteriaType.ReachLevel;
			uint expectedQuantity = 80;

			// ACT //
			var actualResult = (ReachLevelAchievementCriteria)_sut.GetAchievementCriteriaInstance(expectedTypeId, 0, expectedQuantity);

			// ASSERT //
			Assert.That(actualResult.Type, Is.EqualTo(expectedTypeId));
			Assert.That(actualResult.Level, Is.EqualTo(expectedQuantity));
		}

		[Test]
		public void AchievementCriteriaTypeHelper_GetAchievementCriteriaInstance_CorrectlyPopulatesAssetIdAndQuantity()
		{
			// ARRANGE //
			byte expectedTypeId = (byte)AchievementCriteriaType.KillCreature;
			uint expectedAssetId = 123456;
			uint expectedQuantity = 46;

			// ACT //
			var actualResult = (KillCreatureAchievementCriteria)_sut.GetAchievementCriteriaInstance(expectedTypeId, expectedAssetId, expectedQuantity);

			// ASSERT //
			Assert.That(actualResult.Type, Is.EqualTo(expectedTypeId));
			Assert.That(actualResult.CreatureId, Is.EqualTo(expectedAssetId));
			Assert.That(actualResult.Count, Is.EqualTo(expectedQuantity));
		}
	}
}
