namespace Acmil.Common.Utility.Localization.Interfaces
{
	/// <summary>
	/// Interface describing a helper class for localization.
	/// </summary>
	public interface ILocalizationHelper
	{
		/// <summary>
		/// Gets the configured locale code.
		/// </summary>
		/// <returns>The configured locale code.</returns>
		/// <remarks>
		/// e.g. "enUS"
		/// </remarks>
		public string GetConfiguredLocaleCode();

		/// <summary>
		/// A SQL query template string containing column names to localize.
		/// All instances of "xxYY" will be replaced with the configured locale code.
		/// </summary>
		/// <param name="templateQueryString">The SQL query template string.</param>
		/// <returns>A localized version of the SQL query template string.</returns>
		public string GetLocalizedSqlQueryFromTemplate(string templateQueryString);
	}
}
