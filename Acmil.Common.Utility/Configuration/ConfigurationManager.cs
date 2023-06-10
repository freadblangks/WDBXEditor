using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Configuration.SettingsModels;
using System.Text.Json;

namespace Acmil.Common.Utility.Configuration
{
	/// <summary>
	/// Manager for interacting with configuration values.
	/// </summary>
	public class ConfigurationManager : IConfigurationManager
	{
		private static readonly Lazy<ConfigurationModel> _CONFIGURATION = new Lazy<ConfigurationModel>(() => InitializeConfiguration());

		private static readonly JsonSerializerOptions _SERIALIZER_CONFIG = new JsonSerializerOptions()
		{
			PropertyNameCaseInsensitive = true,
		};

		public ConfigurationModel GetConfiguration() => _CONFIGURATION.Value;

		private static ConfigurationModel InitializeConfiguration()
		{
			string configFilePath = GetConfigFilePath();

			ConfigurationModel configuration;
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

		private static ConfigurationModel CreateConfigFile()
		{
			var config = new ConfigurationModel();
			string configText = JsonSerializer.Serialize(config);
			File.WriteAllText(GetConfigFilePath(), configText);

			return config;
		}

		private static ConfigurationModel ReadConfigFile(string configFilePath)
		{
			string configText = File.ReadAllText(configFilePath);
			return JsonSerializer.Deserialize<ConfigurationModel>(configText, _SERIALIZER_CONFIG);
		}

		private static string GetConfigFilePath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".acmil", "config.json");
	}
}
