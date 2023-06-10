using Acmil.Common.Utility.Configuration.Interfaces;
using System.Text.Json;

namespace Acmil.Common.Utility.Configuration
{
	/// <summary>
	/// Manager for interacting with configuration values.
	/// </summary>
	public class ConfigurationManager : IConfigurationManager
	{
		private static readonly Lazy<SettingsModels.Configuration> _CONFIGURATION = new Lazy<SettingsModels.Configuration>(() => InitializeConfiguration());

		private static readonly JsonSerializerOptions _SERIALIZER_CONFIG = new JsonSerializerOptions()
		{
			PropertyNameCaseInsensitive = true,
		};

		public SettingsModels.Configuration GetConfiguration() => _CONFIGURATION.Value;

		private static SettingsModels.Configuration InitializeConfiguration()
		{
			string configFilePath = GetConfigFilePath();

			SettingsModels.Configuration configuration;
			if (!File.Exists(configFilePath))
			{
				configuration = CreateConfigFile();
			}
			else
			{
				configuration = ReadConfigFile(configFilePath);
			}

			return configuration;
		}

		private static SettingsModels.Configuration CreateConfigFile()
		{
			var config = new SettingsModels.Configuration();
			string configText = JsonSerializer.Serialize(config);
			File.WriteAllText(GetConfigFilePath(), configText);

			return config;
		}

		private static SettingsModels.Configuration ReadConfigFile(string configFilePath)
		{
			string configText = File.ReadAllText(configFilePath);
			return JsonSerializer.Deserialize<SettingsModels.Configuration>(configText, _SERIALIZER_CONFIG);
		}

		private static string GetConfigFilePath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".acmil", "config.json");
	}
}
