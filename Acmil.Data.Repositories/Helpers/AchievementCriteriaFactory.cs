using Acmil.Common.Utility.Extensions;
using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Battlegrounds;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Deaths;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Quests;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Skills;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Helpers.Mapping.Interfaces;
using Acmil.Data.Repositories.Helpers.Interfaces;

namespace Acmil.Data.Repositories.Helpers
{
	public class AchievementCriteriaFactory : IAchievementCriteriaFactory
	{

		#region Criteria Class Dictionary

		private static readonly Dictionary<AchievementCriteriaType, Type> _CLASS_DICT = new Dictionary<AchievementCriteriaType, Type>()
		{
			{ AchievementCriteriaType.KillCreature, typeof(KillCreatureAchievementCriteria) },
			{ AchievementCriteriaType.WinBattleground, typeof(WinBattlegroundAchievementCriteria) },
			{ AchievementCriteriaType.ReachLevel, typeof(ReachLevelAchievementCriteria) },
			{ AchievementCriteriaType.ReachSkillLevel, typeof(ReachSkillLevelAchievementCriteria) },
			{ AchievementCriteriaType.CompleteAchievement, typeof(CompleteAchievementAchievementCriteria) },
			{ AchievementCriteriaType.CompleteQuestCount, typeof(CompleteQuestCountAchievementCriteria) },
			{ AchievementCriteriaType.CompleteDailyQuestDaily, typeof(CompleteDailyQuestDailyAchievementCriteria) },
			{ AchievementCriteriaType.CompleteQuestsInZone, typeof(CompleteQuestsInZoneAchievementCriteria) },
			{ AchievementCriteriaType.DamageDone, typeof(DamageDoneAchievementCriteria) },
			{ AchievementCriteriaType.CompleteDailyQuest, typeof(CompleteDailyQuestDailyAchievementCriteria) },
			{ AchievementCriteriaType.CompleteBattleground, typeof(CompleteBattlegroundAchievementCriteria) },
			{ AchievementCriteriaType.DeathAtMap, typeof(DeathAtMapAchievementCriteria) },
			{ AchievementCriteriaType.DeathInDungeon, typeof(DeathInDungeonAchievementCriteria) },
			{ AchievementCriteriaType.CompleteDungeonOrRaidOfMaxPlayerCount, typeof(CompleteDungeonOrRaidOfMaxPlayerCountAchievementCriteria) },
			{ AchievementCriteriaType.KilledByCreature, typeof(KilledByCreatureAchievementCriteria) },
			{ AchievementCriteriaType.FallWithoutDying, typeof(FallWithoutDyingAchievementCriteria) },
			{ AchievementCriteriaType.DeathsFromEnvironmentalDamage, typeof(DeathsFromEnvironmentalDamageAchievementCriteria) },
			{ AchievementCriteriaType.CompleteQuest, typeof(CompleteQuestAchievementCriteria) },
		};

		#endregion

		private readonly IModelCustomAttributeHelper _customAttributeHelper;

		public AchievementCriteriaFactory(IModelCustomAttributeHelper customAttributeHelper)
		{
			_customAttributeHelper = customAttributeHelper;
		}

		public BaseAchievementCriteria GetAchievementCriteriaInstance(byte typeId, uint assetId = 0, uint quantity = 0, uint startEvent = 0, uint startAsset = 0, uint failEvent = 0, uint failAsset = 0)
		{
			Type criteriaClass = Enum.IsDefined(typeof(AchievementCriteriaType), typeId)
				? _CLASS_DICT[(AchievementCriteriaType)typeId]
				: typeof(UnsupportedAchievementCriteria);


			dynamic criteriaInstance = Activator.CreateInstance(criteriaClass);
			var propsWithColumnNames = criteriaClass.GetProperties().Where(prop => prop.HasCustomAttribute(typeof(MySqlColumnNameAttribute))).ToList();
			propsWithColumnNames.ForEach(prop =>
			{
				var attr = _customAttributeHelper.GetPropertyCustomAttribute<MySqlColumnNameAttribute>(prop);
				switch (attr.Name)
				{
					case "Asset_Id":
						prop.SetValue(criteriaInstance, assetId);
						break;
					case "Quantity":
						prop.SetValue(criteriaInstance, quantity);
						break;
					case "Start_Event":
						prop.SetValue(criteriaInstance, startEvent);
						break;
					case "Start_Asset":
						prop.SetValue(criteriaInstance, startAsset);
						break;
					case "Fail_Event":
						prop.SetValue(criteriaInstance, failEvent);
						break;
					case "Fail_Asset":
						prop.SetValue(criteriaInstance, failAsset);
						break;
					default:
						break;	// Ignore any unexpected columns.
				}
			});

			return criteriaInstance;
			

			//switch (type)
			//{
			//	case AchievementCriteriaType.KillCreature:
			//	case AchievementCriteriaType.WinBattleground:
			//	case AchievementCriteriaType.ReachLevel:
			//	case AchievementCriteriaType.ReachSkillLevel:
			//	case AchievementCriteriaType.CompleteAchievement:
			//	case AchievementCriteriaType.CompleteQuestCount:
			//	case AchievementCriteriaType.CompleteDailyQuestDaily:
			//	case AchievementCriteriaType.CompleteQuestsInZone:
			//	case AchievementCriteriaType.DamageDone:
			//	case AchievementCriteriaType.CompleteDailyQuest:
			//	case AchievementCriteriaType.CompleteBattleground:
			//	case AchievementCriteriaType.DeathAtMap:
			//	case AchievementCriteriaType.DeathInDungeon:
			//	case AchievementCriteriaType.CompleteRaid:
			//	case AchievementCriteriaType.KilledByCreature:
			//	case AchievementCriteriaType.FallWithoutDying:
			//	case AchievementCriteriaType.DeathsFrom:
			//	case AchievementCriteriaType.CompleteQuest:
			//	case AchievementCriteriaType.BeSpellTarget:
			//	case AchievementCriteriaType.CastSpellExtended:
			//	case AchievementCriteriaType.BattlegroundObjectiveCapture:
			//	case AchievementCriteriaType.HonorableKillAtArea:
			//	case AchievementCriteriaType.WinArena:
			//	case AchievementCriteriaType.PlayArena:
			//	case AchievementCriteriaType.LearnSpell:
			//	case AchievementCriteriaType.OwnItem:
			//	case AchievementCriteriaType.WinRatedArena:
			//	case AchievementCriteriaType.HighestTeamRating:
			//	case AchievementCriteriaType.ReachTeamRating:
			//	case AchievementCriteriaType.LearnSkillLevel:
			//	case AchievementCriteriaType.UseItem:
			//	case AchievementCriteriaType.LootItem:
			//	case AchievementCriteriaType.ExploreArea:
			//	case AchievementCriteriaType.OwnRank:
			//	case AchievementCriteriaType.BuyBankSlot:
			//	case AchievementCriteriaType.GainReputation:
			//	case AchievementCriteriaType.GainExaltedReputation:
			//	case AchievementCriteriaType.VisitBarberShop:
			//	case AchievementCriteriaType.EquipEpicItem:
			//	case AchievementCriteriaType.RollNeedOnLoot:
			//	case AchievementCriteriaType.RollGreedOnLoot:
			//	case AchievementCriteriaType.HkClass:
			//	case AchievementCriteriaType.HkRace:
			//	case AchievementCriteriaType.DoEmote:
			//	case AchievementCriteriaType.HealingDone:
			//	case AchievementCriteriaType.GetKillingBlows:
			//	case AchievementCriteriaType.EquipItem:
			//	case AchievementCriteriaType.MoneyFromQuestReward:
			//	case AchievementCriteriaType.LootMoney:
			//	case AchievementCriteriaType.UseGameobject:
			//	case AchievementCriteriaType.BeSpellTargetExtended:
			//	case AchievementCriteriaType.SpecialPvpKill:
			//	case AchievementCriteriaType.FishInGameobject:
			//	case AchievementCriteriaType.LearnSkillLineSpells:
			//	case AchievementCriteriaType.WinDuel:
			//	case AchievementCriteriaType.HighestPower:
			//	case AchievementCriteriaType.HighestStat:
			//	case AchievementCriteriaType.HighestSpellpower:
			//	case AchievementCriteriaType.HighestRating:
			//	case AchievementCriteriaType.LootType:
			//	case AchievementCriteriaType.CastSpell:
			//	case AchievementCriteriaType.LearnSkillLine:
			//	case AchievementCriteriaType.EarnHonorableKill:
			//	case AchievementCriteriaType.AcceptedSummons:
			//	case AchievementCriteriaType.AchivementpointsReached:
			//	case AchievementCriteriaType.RandomDungeonPlayercount:
			//		throw new NotImplementedException($"No type currently implemented for criteria type '{type}'.");
			//	default:
			//		criteriaInstance = new UnsupportedAchievementCriteria()
			//		{
			//			Type = (byte)type,
			//			AssetId = assetId,
			//			Quantity = quantity,
			//			StartEvent = startEvent,
			//			StartAsset = startAsset,

			//		};
			//		break;
			//	}
		}
	}
}
