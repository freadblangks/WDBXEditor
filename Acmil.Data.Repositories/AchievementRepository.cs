using Acmil.Common.Utility.Configuration.SettingsModels.Locale.Enums;
using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Localization.Interfaces;
using Acmil.Data.Contracts.Models.Achievements;
using Acmil.Data.Contracts.Models.Achievements.Criteria;
using Acmil.Data.Contracts.Models.Achievements.Rewards;
using Acmil.Data.Contracts.Types.Primitives;
using Acmil.Data.Helpers;
using Acmil.Data.Helpers.Interfaces;
using Acmil.Data.Repositories.Helpers.Interfaces;
using Acmil.Data.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace Acmil.Data.Repositories
{
	public class AchievementRepository : IAchievementRepository
	{
		private IUtilityHelper _utilityHelper;
		private ILocalizationHelper _localizationHelper;
		private IDbContextFactory _dbContextFactory;
		private IAchievementCriteriaFactory _criteriaHelper;

		/// <summary>
		/// Initializes a new instance of <see cref="AchievementRepository"/>.
		/// </summary>
		/// <param name="utilityHelper">An implementation of <see cref="IUtilityHelper"/>.</param>
		/// <param name="dbContextFactory">An implementation of <see cref="IDbContextFactory"/> for interacting with SQL.</param>
		/// <param name="criteriaHelper">An implementation of <see cref="IAchievementCriteriaFactory"/> for interacting withtypes of  Achievement Criteria.</param>
		public AchievementRepository(IUtilityHelper utilityHelper, IDbContextFactory dbContextFactory, IAchievementCriteriaFactory criteriaHelper)
		{
			_utilityHelper = utilityHelper;
			_localizationHelper = _utilityHelper.LocalizationHelper;
			_dbContextFactory = dbContextFactory;
			_criteriaHelper = criteriaHelper;
		}

		public Achievement CreateAchievement(Achievement achievement)
		{
			throw new NotImplementedException();
		}

		public AchievementCategory CreateAchievementCategory(AchievementCategory category)
		{
			throw new NotImplementedException();
		}

		public Achievement ReadAchievement(ushort achievementId)
		{
			#region SQL Query

			string sqlTemplate = @"
				SELECT
					`ID`,
					`Faction`,
					`Instance_Id`,
					`Supercedes`,
					`Title_Lang_xxYY` AS 'Title',
					`Description_Lang_xxYY` AS 'Description',
					`Category`,
					`Points`,
					`Ui_Order`,
					`Flags`,
					`IconID`,
					`Reward_Lang_xxYY` AS 'Reward',
					`Minimum_Criteria`,
					`Shares_Criteria`
				FROM achievement_dbc
				WHERE `ID` = @AchievementId;
			";

			#endregion

			string localizedSql = _localizationHelper.GetLocalizedSqlQueryFromTemplate(sqlTemplate);
			var connectionInfo = _utilityHelper.ConfigurationManager.GetConnectionInfo();
			var worldContext = _dbContextFactory.GetContext(connectionInfo, DbConfigHelper.WorldDatabaseName);

			var sqlParameters = new List<MySqlParameter>()
			{
				// Yes, this should be using the Int32 MySqlDbType
				// even though we're treating the ID as a ushort.
				new MySqlParameter("@AchievementId", MySqlDbType.Int32) { Value = Convert.ToInt32(achievementId) }
			};

			Achievement result = worldContext.ExecuteSqlStatementAsObject(
				localizedSql,
				dataReader => ConvertReadResponseToAchievement(dataReader),
				sqlParameters
			);
			return result;
		}

		public IReadOnlyList<Achievement> ReadAchievements()
		{
			throw new NotImplementedException();
		}

		public AchievementCategory ReadAchievementCategory(ushort categoryId)
		{

			#region SQL Query

			//string sqlTemplate = @"
			//	SELECT
			//		`ID`,
			//		`Parent`,
			//		`Name_Lang_xxYY` AS 'Name',
			//		`Ui_Order`
			//	FROM achievement_category_dbc;
			//	WHERE `ID` = @CategoryId;
			//";

			// This craziness is needed to get both the category and its parent in the same query.
			string sqlTemplate = @"
				WITH RECURSIVE cte (`ID`, `Parent`, `Name`, `Ui_Order`) AS
				(
					SELECT
						ac.`ID`,
						ac.`Parent` parentId,
						ac.`Name_Lang_xxYY` AS 'Name',
						ac.`Ui_Order`
					FROM achievement_category_dbc ac
					WHERE ac.`ID` = @CategoryId

					UNION ALL

					SELECT
						ac.`ID`,
						ac.`Parent` parentId,
						ac.`Name_Lang_xxYY` AS 'Name',
						ac.`Ui_Order`
					FROM achievement_category_dbc ac
					INNER JOIN cte WHERE ac.`ID` = cte.`Parent`
				)
				SELECT * FROM cte;
			";

			#endregion

			string localizedSql = _localizationHelper.GetLocalizedSqlQueryFromTemplate(sqlTemplate);
			var connectionInfo = _utilityHelper.ConfigurationManager.GetConnectionInfo();
			var worldContext = _dbContextFactory.GetContext(connectionInfo, DbConfigHelper.WorldDatabaseName);

			var sqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@CategoryId", MySqlDbType.Int32) { Value = categoryId }
			};

			// TODO: Figure out if you can have categories nested beyond one level (you probably shouldn't be able to, but who knows).
			var results = worldContext.ExecuteSqlStatementAsList(
				localizedSql,
				dataReader => ConvertReadResponseToAchievementCategory(dataReader),
				sqlParameters
			);

			return (results.Count() < 2)
				? results.ElementAtOrDefault(0)	// Either the category is at the top of the hierarchy or we have no results.
				: CombineCategoryWithParent(results);	// The category has a parent.
		}

		public List<BaseAchievementCriteria> ReadAchievementCriteria(ushort achievementId)
		{
			#region SQL Query

			string sqlTemplate = @"

				SELECT
					`ID`,
					`Achievement_Id`,
					ac.`Type` AS 'CriteriaType',
					`Asset_Id`,
					`Quantity`,
					`Start_Event`,
					`Start_Asset`,
					`Fail_Event`,
					`Fail_Asset`,
					`Description_Lang_xxYY` AS 'Description',
					`Flags`,
					`Timer_Start_Event`,
					`Timer_Asset_Id`,
					`Timer_Time`,
					`Ui_Order`,
					acd.`type` AS 'DataType',
					`value1`,
					`value2`,
					`ScriptName`
				FROM acore_world.achievement_criteria_dbc ac
				LEFT OUTER JOIN acore_world.achievement_criteria_data acd ON ac.`ID` = acd.`criteria_id`
				WHERE `Achievement_Id` = @AchievementId;
			";

			#endregion

			string localizedSql = _localizationHelper.GetLocalizedSqlQueryFromTemplate(sqlTemplate);
			var connectionInfo = _utilityHelper.ConfigurationManager.GetConnectionInfo();
			var worldContext = _dbContextFactory.GetContext(connectionInfo, DbConfigHelper.WorldDatabaseName);

			var sqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@AchievementId", MySqlDbType.Int32) { Value = achievementId }
			};

			var results = worldContext.ExecuteSqlStatementAsList(
				localizedSql,
				dataReader => ConvertReadResponseToAchievementCriteria(dataReader),
				sqlParameters
			);

			return results;
		}

		public BaseAchievementReward ReadAchievementReward(ushort achievementId, string localeCode = "")
		{
			if (string.IsNullOrWhiteSpace(localeCode))
			{
				localeCode = _localizationHelper.GetConfiguredLocaleCode();
			}

			#region SQL Query

			string sqlQuery = "";
			if (localeCode == LocaleCode.enUS.ToString())
			{
				sqlQuery = @"
					SELECT
						`ID`,
						`TitleA`,
						`TitleH`,
						`ItemID`,
						`Sender`,
						'enUS' AS 'Locale',
						`Subject`,
						`Body`,
						`MailTemplateID`
					FROM achievement_reward
					WHERE `ID` = @AchievementId;
				";
			}

			// Localized text for locales other than enUS is stored in its own table.
			else
			{
				// Explanation for the madness that is this query's WHERE clause:
				//    If we have a Sender (i.e. this is an item where we need localized text for a letter), that's when we care about the locale.
				//    Otherwise, we just care that we meet the minimum criteria for a title reward.
				sqlQuery = @"
					SELECT
						ar.`ID`,
						`TitleA`,
						`TitleH`,
						`ItemID`,
						`Sender`,
						`Locale`,
						arl.`Subject`,
						arl.`Text` AS 'Body',
						`MailTemplateID`
					FROM achievement_reward ar
					LEFT OUTER JOIN achievement_reward_locale arl ON ar.`ID` = arl.`ID`
					WHERE ar.`ID` = @AchievementId AND (`Sender` > 0 AND (`Locale` = @LocaleCode OR `MailTemplateID` > 0) OR `TitleA` > 0 OR `TitleH` > 0);
				";
			}

			#endregion

			var connectionInfo = _utilityHelper.ConfigurationManager.GetConnectionInfo();
			var worldContext = _dbContextFactory.GetContext(connectionInfo, DbConfigHelper.WorldDatabaseName);

			var sqlParameters = new List<MySqlParameter>()
			{
				new MySqlParameter("@AchievementId", MySqlDbType.UInt24) { Value = achievementId },
				new MySqlParameter("@LocaleCode", MySqlDbType.VarChar) { Value = localeCode}
			};

			BaseAchievementReward result = worldContext.ExecuteSqlStatementAsObject(
				sqlQuery,
				dataReader => ConvertReadResponseToAchievementReward(dataReader),
				sqlParameters
			);

			return result;
		}

		public Achievement UpdateAchievementCategory(ushort achievementId, short categoryId)
		{
			throw new NotImplementedException();
		}

		public bool DeleteAchievement(ushort achievementId)
		{
			throw new NotImplementedException();
		}

		private Achievement ConvertReadResponseToAchievement(MySqlDataReader dataReader)
		{
			var achievement = new Achievement()
			{
				AchievementId = Convert.ToUInt16((int)dataReader["ID"]),
				Faction = Convert.ToSByte((int)dataReader["Faction"]),
				InstanceId = Convert.ToInt16((int)dataReader["Instance_Id"]),
				PreviousAchievementId = Convert.ToUInt16((int)dataReader["Supercedes"]),
				Name = (string)dataReader["Title"],
				Description = (string)dataReader["Description"],
				CategoryId = Convert.ToUInt16((int)dataReader["Category"]),
				Points = Convert.ToSByte((int)dataReader["Points"]),
				UiOrder = Convert.ToUInt16((int)dataReader["Ui_Order"]),
				FlagsMask = (UInt24)Convert.ToUInt32((int)dataReader["Flags"]),
				IconId = (UInt24)Convert.ToUInt32((int)dataReader["IconID"]),
				RewardDescription = (string)dataReader["Reward"],
				MinimumCriteriaRequired = Convert.ToSByte((int)dataReader["Minimum_Criteria"]),
				LinkedAchievementId = Convert.ToUInt16((int)dataReader["Shares_Criteria"])
			};

			return achievement;
		}

		private AchievementCategory ConvertReadResponseToAchievementCategory(MySqlDataReader dataReader)
		{
			var category = new AchievementCategory()
			{
				CategoryId = Convert.ToInt16((int)dataReader["ID"]),
				ParentCategoryId = Convert.ToInt16((int)dataReader["Parent"]),
				Name = (string)dataReader["Name"],
				UiOrder = Convert.ToSByte((int)dataReader["Ui_Order"])
			};

			return category;
		}

		private BaseAchievementCriteria ConvertReadResponseToAchievementCriteria(MySqlDataReader dataReader)
		{
			var criteria = _criteriaHelper.GetAchievementCriteriaInstance(
				Convert.ToByte((int)dataReader["CriteriaType"]),
				Convert.ToUInt32((int)dataReader["Asset_Id"]),
				Convert.ToUInt32((int)dataReader["Quantity"]),
				Convert.ToUInt32((int)dataReader["Start_Event"]),
				Convert.ToUInt32((int)dataReader["Start_Asset"]),
				Convert.ToUInt32((int)dataReader["Fail_Event"]),
				Convert.ToUInt32((int)dataReader["Fail_Asset"])
			);

			criteria.CriteriaId = (Int24)(int)dataReader["ID"];
			criteria.AchievementId = Convert.ToUInt16((int)dataReader["Achievement_Id"]);
			criteria.Description = (string)dataReader["Description"];
			criteria.Flags = Convert.ToUInt32((int)dataReader["Flags"]);
			criteria.TimerStartEvent = Convert.ToUInt32((int)dataReader["Timer_Start_Event"]);
			criteria.TimerAssetId = Convert.ToUInt32((int)dataReader["Timer_Asset_Id"]);
			criteria.TimerTime = Convert.ToUInt32((int)dataReader["Timer_Time"]);
			criteria.UiOrder = Convert.ToUInt16((int)dataReader["Ui_Order"]);

			// TODO: Add support for criteria data.

			return criteria;
		}

		private AchievementCategory CombineCategoryWithParent(IReadOnlyList<AchievementCategory> categories)
		{
			var parentCategory = categories.First(cat => cat.ParentCategoryId == -1);
			var childCategory = categories.First(cat => cat.ParentCategoryId == parentCategory.CategoryId);
			childCategory.ParentCategory = parentCategory;

			return childCategory;
		}

		private BaseAchievementReward ConvertReadResponseToAchievementReward(MySqlDataReader dataReader)
		{
			// TODO: Check if this throws when MailTemplateID is NULL.
			var senderId = (UInt24)(uint)dataReader["Sender"];
			var mailTemplateId = (UInt24)(uint)dataReader["MailTemplateID"];

			BaseAchievementReward reward;
			if (senderId > 0 || mailTemplateId > 0)
			{
				reward = new ItemAchievementReward()
				{
					AchievementId = (UInt24)(uint)dataReader["ID"],
					ItemId = (UInt24)(uint)dataReader["ItemID"],
					SenderId = senderId,
					Locale = (string)dataReader["Locale"],
					MessageSubject = (string)dataReader["Subject"],
					MessageBody = (string)dataReader["Body"],
					MailTemplateId = mailTemplateId
				};
			}
			else
			{
				reward = new TitleAchievementReward()
				{
					AchievementId = (UInt24)(uint)dataReader["ID"],
					TitleAlliance = (UInt24)(uint)dataReader["TitleA"],
					TitleHorde = (UInt24)(uint)dataReader["TitleH"]
				};
			}

			return reward;
		}
	}
}
