using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contexts;
using WDBXEditor.Data.Contracts.Models.Items;
using WDBXEditor.Data.Contracts.Models.Items.Submodels;
using WDBXEditor.Data.Contracts.Models.Items.Submodels.Requirements;
using WDBXEditor.Data.Helpers.Mapping;
using WDBXEditor.Data.Services.Interfaces;

namespace WDBXEditor.Data.Services
{
    /// <summary>
    /// Service for interacting with Item Templates.
    /// </summary>
	public class ItemTemplateService : IItemTemplateService
	{
		private IDbContext _worldContext;

		public ItemTemplateService(IDbContext worldContext)
		{
			_worldContext = worldContext;
		}

		public CompleteItemTemplate ReadItemTemplate(UInt24 entryId)
		{
			#region Long Boi
			string sql = @"
                SELECT
                    `entry`,
                    `class`,
                    `subclass`,
                    `SoundOverrideSubclass`,
                    `name`,
                    `displayid`,
                    `Quality`,
                    `Flags`,
                    `FlagsExtra`,
                    `BuyCount`,
                    `BuyPrice`,
                    `SellPrice`,
                    `InventoryType`,
                    `AllowableClass`,
                    `AllowableRace`,
                    `ItemLevel`,
                    `RequiredLevel`,
                    `RequiredSkill`,
                    `RequiredSkillRank`,
                    `requiredspell`,
                    `requiredhonorrank`,
                    `RequiredCityRank`,
                    `RequiredReputationFaction`,
                    `RequiredReputationRank`,
                    `maxcount`,
                    `stackable`,
                    `ContainerSlots`,
                    `StatsCount`,
                    `stat_type1`,
                    `stat_value1`,
                    `stat_type2`,
                    `stat_value2`,
                    `stat_type3`,
                    `stat_value3`,
                    `stat_type4`,
                    `stat_value4`,
                    `stat_type5`,
                    `stat_value5`,
                    `stat_type6`,
                    `stat_value6`,
                    `stat_type7`,
                    `stat_value7`,
                    `stat_type8`,
                    `stat_value8`,
                    `stat_type9`,
                    `stat_value9`,
                    `stat_type10`,
                    `stat_value10`,
                    `ScalingStatDistribution`,
                    `ScalingStatValue`,
                    `dmg_min1`,
                    `dmg_max1`,
                    `dmg_type1`,
                    `dmg_min2`,
                    `dmg_max2`,
                    `dmg_type2`,
                    `armor`,
                    `holy_res`,
                    `fire_res`,
                    `nature_res`,
                    `frost_res`,
                    `shadow_res`,
                    `arcane_res`,
                    `delay`,
                    `ammo_type`,
                    `RangedModRange`,
                    `spellid_1`,
                    `spelltrigger_1`,
                    `spellcharges_1`,
                    `spellppmRate_1`,
                    `spellcooldown_1`,
                    `spellcategory_1`,
                    `spellcategorycooldown_1`,
                    `spellid_2`,
                    `spelltrigger_2`,
                    `spellcharges_2`,
                    `spellppmRate_2`,
                    `spellcooldown_2`,
                    `spellcategory_2`,
                    `spellcategorycooldown_2`,
                    `spellid_3`,
                    `spelltrigger_3`,
                    `spellcharges_3`,
                    `spellppmRate_3`,
                    `spellcooldown_3`,
                    `spellcategory_3`,
                    `spellcategorycooldown_3`,
                    `spellid_4`,
                    `spelltrigger_4`,
                    `spellcharges_4`,
                    `spellppmRate_4`,
                    `spellcooldown_4`,
                    `spellcategory_4`,
                    `spellcategorycooldown_4`,
                    `spellid_5`,
                    `spelltrigger_5`,
                    `spellcharges_5`,
                    `spellppmRate_5`,
                    `spellcooldown_5`,
                    `spellcategory_5`,
                    `spellcategorycooldown_5`,
                    `bonding`,
                    `description`,
                    `PageText`,
                    `LanguageID`,
                    `PageMaterial`,
                    `startquest`,
                    `lockid`,
                    `Material`,
                    `sheath`,
                    `RandomProperty`,
                    `RandomSuffix`,
                    `block`,
                    `itemset`,
                    `MaxDurability`,
                    `area`,
                    `Map`,
                    `BagFamily`,
                    `TotemCategory`,
                    `socketColor_1`,
                    `socketContent_1`,
                    `socketColor_2`,
                    `socketContent_2`,
                    `socketColor_3`,
                    `socketContent_3`,
                    `socketBonus`,
                    `GemProperties`,
                    `RequiredDisenchantSkill`,
                    `ArmorDamageModifier`,
                    `duration`,
                    `ItemLimitCategory`,
                    `HolidayId`,
                    `ScriptName`,
                    `DisenchantID`,
                    `FoodType`,
                    `minMoneyLoot`,
                    `maxMoneyLoot`,
                    `flagsCustom`,
                    `VerifiedBuild`
                FROM `item_template`
                WHERE `entry` = @EntryId
                ";

            #endregion

            var sqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@EntryId", MySqlDbType.Int32) { Value = entryId }
            };

            CompleteItemTemplate result = _worldContext.ExecuteSqlStatementAsObject(sql, dataReader => ConvertReadResponseToItemTemplate(dataReader), sqlParameters);
            var mapper = new SqlDataMapper<CompleteItemTemplate>();

            return result;
        }

        public List<CompleteItemTemplate> ReadItemTemplates()
		{
            #region Long Boi
            string sql = @"
                SELECT
                    `entry`,
                    `class`,
                    `subclass`,
                    `SoundOverrideSubclass`,
                    `name`,
                    `displayid`,
                    `Quality`,
                    `Flags`,
                    `FlagsExtra`,
                    `BuyCount`,
                    `BuyPrice`,
                    `SellPrice`,
                    `InventoryType`,
                    `AllowableClass`,
                    `AllowableRace`,
                    `ItemLevel`,
                    `RequiredLevel`,
                    `RequiredSkill`,
                    `RequiredSkillRank`,
                    `requiredspell`,
                    `requiredhonorrank`,
                    `RequiredCityRank`,
                    `RequiredReputationFaction`,
                    `RequiredReputationRank`,
                    `maxcount`,
                    `stackable`,
                    `ContainerSlots`,
                    `StatsCount`,
                    `stat_type1`,
                    `stat_value1`,
                    `stat_type2`,
                    `stat_value2`,
                    `stat_type3`,
                    `stat_value3`,
                    `stat_type4`,
                    `stat_value4`,
                    `stat_type5`,
                    `stat_value5`,
                    `stat_type6`,
                    `stat_value6`,
                    `stat_type7`,
                    `stat_value7`,
                    `stat_type8`,
                    `stat_value8`,
                    `stat_type9`,
                    `stat_value9`,
                    `stat_type10`,
                    `stat_value10`,
                    `ScalingStatDistribution`,
                    `ScalingStatValue`,
                    `dmg_min1`,
                    `dmg_max1`,
                    `dmg_type1`,
                    `dmg_min2`,
                    `dmg_max2`,
                    `dmg_type2`,
                    `armor`,
                    `holy_res`,
                    `fire_res`,
                    `nature_res`,
                    `frost_res`,
                    `shadow_res`,
                    `arcane_res`,
                    `delay`,
                    `ammo_type`,
                    `RangedModRange`,
                    `spellid_1`,
                    `spelltrigger_1`,
                    `spellcharges_1`,
                    `spellppmRate_1`,
                    `spellcooldown_1`,
                    `spellcategory_1`,
                    `spellcategorycooldown_1`,
                    `spellid_2`,
                    `spelltrigger_2`,
                    `spellcharges_2`,
                    `spellppmRate_2`,
                    `spellcooldown_2`,
                    `spellcategory_2`,
                    `spellcategorycooldown_2`,
                    `spellid_3`,
                    `spelltrigger_3`,
                    `spellcharges_3`,
                    `spellppmRate_3`,
                    `spellcooldown_3`,
                    `spellcategory_3`,
                    `spellcategorycooldown_3`,
                    `spellid_4`,
                    `spelltrigger_4`,
                    `spellcharges_4`,
                    `spellppmRate_4`,
                    `spellcooldown_4`,
                    `spellcategory_4`,
                    `spellcategorycooldown_4`,
                    `spellid_5`,
                    `spelltrigger_5`,
                    `spellcharges_5`,
                    `spellppmRate_5`,
                    `spellcooldown_5`,
                    `spellcategory_5`,
                    `spellcategorycooldown_5`,
                    `bonding`,
                    `description`,
                    `PageText`,
                    `LanguageID`,
                    `PageMaterial`,
                    `startquest`,
                    `lockid`,
                    `Material`,
                    `sheath`,
                    `RandomProperty`,
                    `RandomSuffix`,
                    `block`,
                    `itemset`,
                    `MaxDurability`,
                    `area`,
                    `Map`,
                    `BagFamily`,
                    `TotemCategory`,
                    `socketColor_1`,
                    `socketContent_1`,
                    `socketColor_2`,
                    `socketContent_2`,
                    `socketColor_3`,
                    `socketContent_3`,
                    `socketBonus`,
                    `GemProperties`,
                    `RequiredDisenchantSkill`,
                    `ArmorDamageModifier`,
                    `duration`,
                    `ItemLimitCategory`,
                    `HolidayId`,
                    `ScriptName`,
                    `DisenchantID`,
                    `FoodType`,
                    `minMoneyLoot`,
                    `maxMoneyLoot`,
                    `flagsCustom`,
                    `VerifiedBuild`
                FROM `item_template`
                ";

            #endregion

            List<CompleteItemTemplate> itemTemplates = _worldContext.ExecuteSqlStatementAsList<CompleteItemTemplate>(sql, dataReader => ConvertReadResponseToItemTemplate(dataReader));
            return itemTemplates;
        }

        internal void RunFullItemTemplateReadBenchmark()
		{
            #region Long Boi
            string sql = @"
                SELECT
                    `entry`,
                    `class`,
                    `subclass`,
                    `SoundOverrideSubclass`,
                    `name`,
                    `displayid`,
                    `Quality`,
                    `Flags`,
                    `FlagsExtra`,
                    `BuyCount`,
                    `BuyPrice`,
                    `SellPrice`,
                    `InventoryType`,
                    `AllowableClass`,
                    `AllowableRace`,
                    `ItemLevel`,
                    `RequiredLevel`,
                    `RequiredSkill`,
                    `RequiredSkillRank`,
                    `requiredspell`,
                    `requiredhonorrank`,
                    `RequiredCityRank`,
                    `RequiredReputationFaction`,
                    `RequiredReputationRank`,
                    `maxcount`,
                    `stackable`,
                    `ContainerSlots`,
                    `StatsCount`,
                    `stat_type1`,
                    `stat_value1`,
                    `stat_type2`,
                    `stat_value2`,
                    `stat_type3`,
                    `stat_value3`,
                    `stat_type4`,
                    `stat_value4`,
                    `stat_type5`,
                    `stat_value5`,
                    `stat_type6`,
                    `stat_value6`,
                    `stat_type7`,
                    `stat_value7`,
                    `stat_type8`,
                    `stat_value8`,
                    `stat_type9`,
                    `stat_value9`,
                    `stat_type10`,
                    `stat_value10`,
                    `ScalingStatDistribution`,
                    `ScalingStatValue`,
                    `dmg_min1`,
                    `dmg_max1`,
                    `dmg_type1`,
                    `dmg_min2`,
                    `dmg_max2`,
                    `dmg_type2`,
                    `armor`,
                    `holy_res`,
                    `fire_res`,
                    `nature_res`,
                    `frost_res`,
                    `shadow_res`,
                    `arcane_res`,
                    `delay`,
                    `ammo_type`,
                    `RangedModRange`,
                    `spellid_1`,
                    `spelltrigger_1`,
                    `spellcharges_1`,
                    `spellppmRate_1`,
                    `spellcooldown_1`,
                    `spellcategory_1`,
                    `spellcategorycooldown_1`,
                    `spellid_2`,
                    `spelltrigger_2`,
                    `spellcharges_2`,
                    `spellppmRate_2`,
                    `spellcooldown_2`,
                    `spellcategory_2`,
                    `spellcategorycooldown_2`,
                    `spellid_3`,
                    `spelltrigger_3`,
                    `spellcharges_3`,
                    `spellppmRate_3`,
                    `spellcooldown_3`,
                    `spellcategory_3`,
                    `spellcategorycooldown_3`,
                    `spellid_4`,
                    `spelltrigger_4`,
                    `spellcharges_4`,
                    `spellppmRate_4`,
                    `spellcooldown_4`,
                    `spellcategory_4`,
                    `spellcategorycooldown_4`,
                    `spellid_5`,
                    `spelltrigger_5`,
                    `spellcharges_5`,
                    `spellppmRate_5`,
                    `spellcooldown_5`,
                    `spellcategory_5`,
                    `spellcategorycooldown_5`,
                    `bonding`,
                    `description`,
                    `PageText`,
                    `LanguageID`,
                    `PageMaterial`,
                    `startquest`,
                    `lockid`,
                    `Material`,
                    `sheath`,
                    `RandomProperty`,
                    `RandomSuffix`,
                    `block`,
                    `itemset`,
                    `MaxDurability`,
                    `area`,
                    `Map`,
                    `BagFamily`,
                    `TotemCategory`,
                    `socketColor_1`,
                    `socketContent_1`,
                    `socketColor_2`,
                    `socketContent_2`,
                    `socketColor_3`,
                    `socketContent_3`,
                    `socketBonus`,
                    `GemProperties`,
                    `RequiredDisenchantSkill`,
                    `ArmorDamageModifier`,
                    `duration`,
                    `ItemLimitCategory`,
                    `HolidayId`,
                    `ScriptName`,
                    `DisenchantID`,
                    `FoodType`,
                    `minMoneyLoot`,
                    `maxMoneyLoot`,
                    `flagsCustom`,
                    `VerifiedBuild`
                FROM `item_template`
                ";

            #endregion
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var mapper = new SqlDataMapper<CompleteItemTemplate>();
            List<CompleteItemTemplate> itemTemplates = _worldContext.ExecuteSqlStatementAsList<CompleteItemTemplate>(sql, dataReader => mapper.MapSqlDataRow(dataReader));
            stopwatch.Stop();
            Console.WriteLine($"Read table using dynamic SQL mapping in {stopwatch.Elapsed}");
            itemTemplates.Clear();
            itemTemplates.TrimExcess();
            stopwatch.Reset();
            stopwatch.Start();
            itemTemplates = _worldContext.ExecuteSqlStatementAsList<CompleteItemTemplate>(sql, dataReader => ConvertReadResponseToItemTemplate(dataReader));
            stopwatch.Stop();
            Console.WriteLine($"Read table using static SQL mapping in {stopwatch.Elapsed}");
        }

        private static CompleteItemTemplate ConvertReadResponseToItemTemplate(MySqlDataReader dataReader)
		{
            var itemTemplate = new CompleteItemTemplate()
            {
                EntryId = (UInt24)(uint)dataReader["entry"],
                Class = (byte)dataReader["class"],
                SubClass = (byte)dataReader["subclass"],
                SoundOverrideSubclass = (sbyte)dataReader["SoundOverrideSubclass"],
                Name = (string)dataReader["name"],
                DisplayId = (UInt24)(uint)dataReader["displayid"],
                Quality = (byte)dataReader["Quality"],
                FlagsMask = (uint)dataReader["Flags"],
                FlagsExtraMask = (uint)dataReader["FlagsExtra"],
                BuyCount = (byte)dataReader["BuyCount"],
                BuyPrice = (long)dataReader["BuyPrice"],
                SellPrice = (uint)dataReader["SellPrice"],
                InventoryType = (byte)dataReader["InventoryType"],
                AllowableClassesMask = (int)dataReader["AllowableClass"],
                AllowableRacesMask = (int)dataReader["AllowableRace"],
                ItemLevel = (ushort)dataReader["ItemLevel"],
                Requirements = new ItemRequirements()
                {
                    RequiredLevel = (byte)dataReader["RequiredLevel"],
                    RequiredSkill = new ItemRequiredSkill()
                    {
                        SkillLineId = (ushort)dataReader["RequiredSkill"],
                        SkillRank = (ushort)dataReader["RequiredSkillRank"]
                    },
                    RequiredSpellId = (UInt24)(uint)dataReader["requiredspell"],
                    RequiredFaction = new ItemRequiredFaction()
                    {
                        ReputationFactionId = (ushort)dataReader["RequiredReputationFaction"],
                        ReputationFactionRank = (ushort)dataReader["RequiredReputationRank"],
                    }
                },
                MaxCount = (int)dataReader["maxcount"],
                MaxStackSize = (int)dataReader["stackable"],
                ContainerSlots = (byte)dataReader["ContainerSlots"],
                StatsCount = (byte)dataReader["StatsCount"],
                Stats = new ItemStat[10]
                {
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type1"],
                        StatValue = (short)dataReader["stat_value1"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type2"],
                        StatValue = (short)dataReader["stat_value2"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type3"],
                        StatValue = (short)dataReader["stat_value3"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type4"],
                        StatValue = (short)dataReader["stat_value4"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type5"],
                        StatValue = (short)dataReader["stat_value5"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type6"],
                        StatValue = (short)dataReader["stat_value6"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type7"],
                        StatValue = (short)dataReader["stat_value7"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type8"],
                        StatValue = (short)dataReader["stat_value8"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type9"],
                        StatValue = (short)dataReader["stat_value9"]
                    },
                    new ItemStat()
                    {
                        StatModified = (byte)dataReader["stat_type10"],
                        StatValue = (short)dataReader["stat_value10"]
                    }
                },
                ScalingStatDistribution = (short)dataReader["ScalingStatDistribution"],
                ScalingStatValue = (uint)dataReader["ScalingStatValue"],
                DamageDefinitions = new ItemDamageDefinition[2]
				{
                    new ItemDamageDefinition()
					{
                        MinValue = (float)dataReader["dmg_min1"],
                        MaxValue = (float)dataReader["dmg_max1"],
                        DamageType = (byte)dataReader["dmg_type1"]
					},
                    new ItemDamageDefinition()
                    {
                        MinValue = (float)dataReader["dmg_min2"],
                        MaxValue = (float)dataReader["dmg_max2"],
                        DamageType = (byte)dataReader["dmg_type2"]
                    },
                },
                Armor = (ushort)dataReader["armor"],
                Resistances = new ItemResistances
				{
                    Holy = (byte)dataReader["holy_res"],
                    Fire = (byte)dataReader["fire_res"],
                    Nature = (byte)dataReader["nature_res"],
                    Frost = (byte)dataReader["frost_res"],
                    Shadow = (byte)dataReader["shadow_res"],
                    Arcane = (byte)dataReader["arcane_res"],
                },
                DelayBetweenAttacksInMilliseconds = (ushort)dataReader["delay"],
                AmmoType = (byte)dataReader["ammo_type"],
                RangedModifier = (float)dataReader["RangedModRange"],
                Spells = new ItemSpell[5]
				{
                    new ItemSpell()
					{
                        Id = (Int24)(int)dataReader["spellid_1"],
                        Trigger = (byte)dataReader["spelltrigger_1"],
                        Charges = (short)dataReader["spellcharges_1"],
                        PpmRate = (float)dataReader["spellppmRate_1"],
                        Cooldown = (int)dataReader["spellcooldown_1"],
                        Category = (ushort)dataReader["spellcategory_1"],
                        CategoryCooldown = (int)dataReader["spellcategorycooldown_1"]
					},
                    new ItemSpell()
                    {
                        Id = (Int24)(int)dataReader["spellid_2"],
                        Trigger = (byte)dataReader["spelltrigger_2"],
                        Charges = (short)dataReader["spellcharges_2"],
                        PpmRate = (float)dataReader["spellppmRate_2"],
                        Cooldown = (int)dataReader["spellcooldown_2"],
                        Category = (ushort)dataReader["spellcategory_2"],
                        CategoryCooldown = (int)dataReader["spellcategorycooldown_2"]
                    },
                    new ItemSpell()
                    {
                        Id = (Int24)(int)dataReader["spellid_3"],
                        Trigger = (byte)dataReader["spelltrigger_3"],
                        Charges = (short)dataReader["spellcharges_3"],
                        PpmRate = (float)dataReader["spellppmRate_3"],
                        Cooldown = (int)dataReader["spellcooldown_3"],
                        Category = (ushort)dataReader["spellcategory_3"],
                        CategoryCooldown = (int)dataReader["spellcategorycooldown_3"]
                    },
                    new ItemSpell()
                    {
                        Id = (Int24)(int)dataReader["spellid_4"],
                        Trigger = (byte)dataReader["spelltrigger_4"],
                        Charges = (short)dataReader["spellcharges_4"],
                        PpmRate = (float)dataReader["spellppmRate_4"],
                        Cooldown = (int)dataReader["spellcooldown_4"],
                        Category = (ushort)dataReader["spellcategory_4"],
                        CategoryCooldown = (int)dataReader["spellcategorycooldown_4"]
                    },
                    new ItemSpell()
                    {
                        Id = (Int24)(int)dataReader["spellid_5"],
                        Trigger = (byte)dataReader["spelltrigger_5"],
                        Charges = (short)dataReader["spellcharges_5"],
                        PpmRate = (float)dataReader["spellppmRate_5"],
                        Cooldown = (int)dataReader["spellcooldown_5"],
                        Category = (ushort)dataReader["spellcategory_5"],
                        CategoryCooldown = (int)dataReader["spellcategorycooldown_5"]
                    },
                },
                Binding = (byte)dataReader["bonding"],
                Description = (string)dataReader["description"],
                PageTextId = (UInt24)(uint)dataReader["PageText"],
                LanguageId = (byte)dataReader["LanguageID"],
                PageMaterialId = (byte)dataReader["PageMaterial"],
                QuestId = (UInt24)(uint)dataReader["startquest"],
                LockId = (UInt24)(uint)dataReader["lockid"],
                MaterialId = (sbyte)dataReader["Material"],
                SheathAnimationId = (byte)dataReader["sheath"],
                RandomProperty = (Int24)(int)dataReader["RandomProperty"],
                RandomSuffix = (UInt24)(uint)dataReader["RandomSuffix"],
                BlockChance = (UInt24)(uint)dataReader["block"],
                ItemSetId = (UInt24)(uint)dataReader["itemset"],
                MaxDurability = (ushort)dataReader["MaxDurability"],
                GeographicRestrictions = new ItemGeographicRestrictions()
				{
                    ZoneId = (UInt24)(uint)dataReader["area"],
                    MapId = (short)dataReader["Map"]
				},
                BagFamilyId = (Int24)(int)dataReader["BagFamily"],
                TotemCategory = (Int24)(int)dataReader["TotemCategory"],
                SocketDefinitions = new ItemSocketDefinition[3]
				{
                    new ItemSocketDefinition()
					{
                        Color = (sbyte)dataReader["socketColor_1"],
                        Count = (Int24)(int)dataReader["socketContent_1"]
					},
                    new ItemSocketDefinition()
                    {
                        Color = (sbyte)dataReader["socketColor_2"],
                        Count = (Int24)(int)dataReader["socketContent_2"]
                    },
                    new ItemSocketDefinition()
                    {
                        Color = (sbyte)dataReader["socketColor_3"],
                        Count = (Int24)(int)dataReader["socketContent_3"]
                    },
                },
                SocketBonus = (Int24)(int)dataReader["socketBonus"],
                GemProperties = (Int24)(int)dataReader["GemProperties"],
                RequiredDisenchantSkill = (short)dataReader["RequiredDisenchantSkill"],
                ArmorDamageModifier = (float)dataReader["ArmorDamageModifier"],
                DurationInSeconds = (uint)dataReader["duration"],
                ItemLimitCategory = (short)dataReader["ItemLimitCategory"],
                HolidayId = (uint)dataReader["HolidayId"],
                ScriptName = (string)dataReader["ScriptName"],
                DisenchantId = (UInt24)(uint)dataReader["DisenchantID"],
                FoodTypeId = (byte)dataReader["FoodType"],
                MinMoneyLoot = (uint)dataReader["minMoneyLoot"],
                MaxMoneyLoot = (uint)dataReader["maxMoneyLoot"],
                FlagsCustomMask = (uint)dataReader["flagsCustom"],
                VerifiedBuild = (short)dataReader["VerifiedBuild"]
            };

            return itemTemplate;
		}
	}
}
