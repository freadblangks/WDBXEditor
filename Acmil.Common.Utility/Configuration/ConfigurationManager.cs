using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Configuration.SettingsModels;
using Acmil.Common.Utility.Connections;
using Acmil.Common.Utility.Exceptions;
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
			WriteIndented = true,
		};

		private static MySqlConnectionInfo _connectionInfo;

		public ConfigurationModel GetConfiguration() => _CONFIGURATION.Value;

		public MySqlConnectionInfo GetConnectionInfo()
		{
			if (_connectionInfo is null)
			{
				string msg = $"Attempted to read connection info from {nameof(ConfigurationManager)}, but none was found.";
				msg += " This is usually caused by not calling BaseCmdlet.BeginProcessing() from a child cmdlet before requesting SQL data.";
				throw new NoConnectionInfoSetException(msg);
			}
			return _connectionInfo;
		}

		public void SetConnectionInfo(MySqlConnectionInfo connectionInfo)
		{
			_connectionInfo = connectionInfo;
		}

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
			string configText = JsonSerializer.Serialize(config, _SERIALIZER_CONFIG);
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
