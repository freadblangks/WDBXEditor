using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Configuration.SettingsModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Acmil.Common.Utility.Configuration
{
	/// <summary>
	/// Manager for interacting with the session configuration values, defined by appsettings.json.
	/// </summary>
	public class ConfigurationManager : IConfigurationManager
	{
		private static readonly Lazy<ConfigurationManager> _LAZY_INSTANCE = new Lazy<ConfigurationManager>(() => new ConfigurationManager());

		private IConfigurationRoot _configRoot;

		private ConfigurationManager()
		{
			string currentExecutableDirectoryPath = GetCurrentExecutableDirectoryPath();
			var configBuilder = new ConfigurationBuilder()
				.SetBasePath(currentExecutableDirectoryPath)
				.AddJsonFile("appsettings.json");
			_configRoot = configBuilder.Build();
		}

		public static IConfigurationManager Instance
		{
			get
			{
				return _LAZY_INSTANCE.Value;
			}
		}

		public AppSettings GetAppSettings()
		{
			var appSettings = new AppSettings();
			_configRoot.Bind(appSettings);

			return appSettings;
		}

		public string GetCurrentExecutableDirectoryPath()
		{
			string executablePathWithFileName = Assembly.GetEntryAssembly()?.Location;

			if (executablePathWithFileName is null)
			{
				string message = "The directory of the current executable could not be found, possibly because the call came from unmanaged code.";
				message += $" If this error is occurring during test execution, make sure that a Mock of {nameof(IConfigurationManager)} is being used instead of an actual implementation.";
				throw new InvalidOperationException(message);
			}

			// Remove the file name from the path before returning it.
			return Path.GetDirectoryName(executablePathWithFileName);
		}
	}
}
