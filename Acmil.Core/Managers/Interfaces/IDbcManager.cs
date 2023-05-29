namespace Acmil.Core.Managers.Interfaces
{
	/// <summary>
	/// Interface describing a manager class for interacting with DBC files.
	/// </summary>
	public interface IDbcManager
	{
		/// <summary>
		/// Loads a DBC into a new SQL table.
		/// </summary>
		/// <param name="dbcPath">The path to the DBC file to load.</param>
		/// <param name="tableName">The name the table should be created with. Defaults to 'db_{dbc_name}_{build_number}.</param>
		public void LoadDbcIntoSql(string dbcPath, string tableName = null);
	}
}
