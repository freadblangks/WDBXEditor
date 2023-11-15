using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Localization.Interfaces;

namespace Acmil.Common.Utility.Localization
{
	/// <summary>
	/// Helper class for localization.
	/// </summary>
	public class LocalizationHelper : ILocalizationHelper
	{
		private IConfigurationManager _configurationManager;

		public LocalizationHelper(IConfigurationManager configurationManager)
		{
			_configurationManager = configurationManager;
		}

		public string GetConfiguredLocaleCode()
		{
			return _configurationManager.GetConfiguration().Locale.LocaleCode;
		}

		public string GetLocalizedSqlQueryFromTemplate(string templateQueryString)
		{
			string localeCode = GetConfiguredLocaleCode();

			return templateQueryString.Replace("xxYY", localeCode);
		}
	}
}
