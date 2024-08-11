namespace Acmil.Common.Utility.Configuration.SettingsModels.Locale
{
    /// <summary>
    /// An object representing the contents of the "locale" property in config.json.
    /// </summary>
    public class LocaleConfiguration
	{
		/// <summary>
		/// The IETF language tag to use for the target locale.
		/// Defaults to "enUS".
		/// </summary>
		/// <remarks>
		/// Represents the "locale.localeCode" property in config.json.
		/// For a full list of supported locales, see <see cref="LocaleCode"/>.
		/// </remarks>
		public string LocaleCode { get; set; } = Enums.LocaleCode.enUS.ToString();
	}
}
