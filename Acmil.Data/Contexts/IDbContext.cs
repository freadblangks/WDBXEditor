using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Acmil.Data.Contexts
{
	/// <summary>
	/// Interface describing a database context.
	/// </summary>
	public interface IDbContext
	{
		// TODO: Full document the exceptions and everything else for these guys.

		/// <summary>
		/// Opens a transaction against the connected MySQL server instance.
		/// </summary>
		void BeginTransaction();

		/// <summary>
		/// Commits the currently active transaction to hte connected MySQL server instance.
		/// </summary>
		void CommitTransation();

		/// <summary>
		/// Executes a MySQL statement against the connection.
		/// </summary>
		/// <param name="sqlStatement">The SQL statement to execute.</param>
		/// <returns>The number of rows affected.</returns>
		int ExecuteNonQuerySqlStatement(string sqlStatement);

		/// <summary>
		/// Executes a SQL statement against the connection with a specified timeout.
		/// </summary>
		/// <param name="sqlStatement">The SQL statement to execute.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>The number of rows affected.</returns>
		int ExecuteNonQuerySqlStatement(string sqlStatement, int timeoutValue);

		/// <summary>
		/// Executes a parameterized SQL statement against the connection.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <returns>The number of rows affected.</returns>
		int ExecuteNonQuerySqlStatement(string sqlStatement, IEnumerable<MySqlParameter> parameters);

		/// <summary>
		/// Executes a parameterized SQL statement against the connection with a specified timeout.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>The number of rows affected</returns>
		int ExecuteNonQuerySqlStatement(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue);

		/// <summary>
		/// Executes a SQL statement that expects a single result, casted to the type of <typeparamref name="T"/>, using the provided timeout.
		/// </summary>
		/// <typeparam name="T">The expected type of the result of the <see param="sqlStatement"/>.</typeparam>
		/// <param name="sqlStatement">A string containing the SQL statement to execute.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>The result of the executed SQL statement, cast to the type specified in <typeparamref name="T"/>.</returns>
		T ExecuteSqlStatementAsScalar<T>(string sqlStatement, int timeoutValue = -1);

		/// <summary>
		/// Executes a parameterized SQL statement that expects a single result, casted to the type of <typeparamref name="T"/>, using the provided set of parameters and timeout.
		/// </summary>
		/// <typeparam name="T">The expected type of the result of the <see param="sqlStatement"/>.</typeparam>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>The result of the executed SQL statement, cast to the type specified in <typeparamref name="T"/>.</returns>
		T ExecuteSqlStatementAsScalar<T>(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue = -1);

		/// <summary>
		/// Executes a parameterized SQL statement that expects a single generic <see cref="object"/> result, using the provided parameters and timeout.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>A generic object representation of the result of the <see param="sqlStatement"/>.</returns>
		object ExecuteSqlStatementAsScalar(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue = -1);

		/// <summary>
		/// Executes a SQL statement and returns a list of strongly-typed results, using the provided converter.
		/// </summary>
		/// <typeparam name="T">The expected type of the results of <paramref name="sqlStatement"/>.</typeparam>
		/// <param name="sqlStatement">A string containing the SQL statement to execute.</param>
		/// <param name="converter">A function that takes a <see cref="MySqlDataReader"/> and produces a <typeparamref name="T"/>-type result.</param>
		/// <returns>The results from executing the SQL statement.</returns>
		List<T> ExecuteSqlStatementAsList<T>(string sqlStatement, Func<MySqlDataReader, T> converter);

		/// <summary>
		/// Executes a SQL statement and returns a list of strongly-typed results, using the provided converter and tiemout value.
		/// </summary>
		/// <typeparam name="T">The expected type of the results of <paramref name="sqlStatement"/>.</typeparam>
		/// <param name="sqlStatement">A string containing the SQL statement to execute.</param>
		/// <param name="converter">A function that takes a <see cref="MySqlDataReader"/> and produces a <typeparamref name="T"/>-type result.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>The results from executing the SQL statement.</returns>
		List<T> ExecuteSqlStatementAsList<T>(string sqlStatement, Func<MySqlDataReader, T> converter, int timeoutValue);

		/// <summary>
		/// Executes a parameterized SQL statement and returns a list of strongly-typed results, using the provided converter and set of parameters.
		/// </summary>
		/// <typeparam name="T">The expected type of the results of <paramref name="sqlStatement"/>.</typeparam>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="converter">A function that takes a <see cref="MySqlDataReader"/> and produces a <typeparamref name="T"/>-type result.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <returns>The results from executing the SQL statement.</returns>
		List<T> ExecuteSqlStatementAsList<T>(string sqlStatement, Func<MySqlDataReader, T> converter, IEnumerable<MySqlParameter> parameters);

		/// <summary>
		/// Executes a parameterized SQL statement and returns a list of strongly-typed results, using the provided converter, set of parameters, and timeout value.
		/// </summary>
		/// <typeparam name="T">The expected type of the results of <paramref name="sqlStatement"/>.</typeparam>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="converter">A function that takes a <see cref="MySqlDataReader"/> and produces a <typeparamref name="T"/>-type result.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>The results from executing the SQL statement.</returns>
		List<T> ExecuteSqlStatementAsList<T>(string sqlStatement, Func<MySqlDataReader, T> converter, IEnumerable<MySqlParameter> parameters, int timeoutValue);

		/// <summary>
		/// Executes a SQL statement and returns the result as a <see cref="DataTable"/>.
		/// </summary>
		/// <param name="sqlStatement">A string containing the SQL statement to execute.</param>
		/// <returns>A <see cref="DataTable"/> containing the result of the executed query.</returns>
		DataTable ExecuteSqlStatementAsDataTable(string sqlStatement);

		/// <summary>
		/// Executes a SQL statement and returns the result as a <see cref="DataTable"/>, using the provided timeout value.
		/// </summary>
		/// <param name="sqlStatement">A string containing the SQL statement to execute.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>A <see cref="DataTable"/> containing the result of the executed query.</returns>
		DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, int timeoutValue);

		/// <summary>
		/// Executes a parameterized SQL statement and returns the result as a <see cref="DataTable"/>, using the provided set of parameters.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <returns>A <see cref="DataTable"/> containing the result of the executed query.</returns>
		DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, IEnumerable<MySqlParameter> parameters);

		/// <summary>
		/// Executes a parameterized SQL statement and returns the result as a <see cref="DataTable"/>, using the provided set of parameters, and a timeout value.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>A <see cref="DataTable"/> containing the result of the executed query.</returns>
		DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue);

		/// <summary>
		/// Executes a parameterized SQL statement and returns the result as a <see cref="DataTable"/>, using the provided set of parameters,
		/// timeout value, and a Boolean indicating whether or not to retry the <paramref name="sqlStatement"/> without MySQL query hints.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <param name="retryWithoutQueryHints">Whether the query should be retried without MySQL query hints.</param>
		/// <returns>A <see cref="DataTable"/> containing the result of the executed query.</returns>
		DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue, bool retryWithoutQueryHints);

		/// <summary>
		/// Executes the specified SQL statement as a Data Reader. If executed in a transaction, the connection will not be closed when
		/// the Data Reader is closed. Otherwise, when the Data Reader is closed, the connection is closed.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <returns>An instance of <see cref="MySqlDataReader"/>, ready to return the data from the query.</returns>
		MySqlDataReader ExecuteSqlStatementAsDataReader(string sqlStatement);

		/// <summary>
		/// Executes the specified SQL statement as a Data Reader. If executed in a transaction, the connection will not be closed when
		/// the Data Reader is closed. Otherwise, when the Data Reader is closed, the connection is closed.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>An instance of <see cref="MySqlDataReader"/>, ready to return the data from the query.</returns>
		MySqlDataReader ExecuteSqlStatementAsDataReader(string sqlStatement, int timeoutValue);

		/// <summary>
		/// Executes the specified parameterized SQL statement as a Data Reader. If executed in a transaction, the connection will not be closed when
		/// the Data Reader is closed. Otherwise, when the Data Reader is closed, the connection is closed.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <returns>An instance of <see cref="MySqlDataReader"/>, ready to return the data from the query.</returns>
		MySqlDataReader ExecuteSqlStatementAsDataReader(string sqlStatement, IEnumerable<MySqlParameter> parameters);

		/// <summary>
		/// Executes the specified parameterized SQL statement as a Data Reader. If executed in a transaction, the connection will not be closed when
		/// the Data Reader is closed. Otherwise, when the Data Reader is closed, the connection is closed.
		/// </summary>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>An instance of <see cref="MySqlDataReader"/>, ready to return the data from the query.</returns>
		MySqlDataReader ExecuteSqlStatementAsDataReader(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue);

		/// <summary>
		/// Executes a SQL statement that expects a single result of type <typeparamref name="T"/>, using the provided converter.
		/// </summary>
		/// <typeparam name="T">The expected type of the query execution result.</typeparam>
		/// <param name="sqlStatement">A string containing the SQL statement to execute.</param>
		/// <param name="converter">A function that takes a <see cref="MySqlDataReader"/> and produces a <typeparamref name="T"/>-type result.</param>
		/// <returns>A <see typeparam="T"/> representation of the result of the <paramref name="sqlStatement"/>.</returns>
		T ExecuteSqlStatementAsObject<T>(string sqlStatement, Func<MySqlDataReader, T> converter);

		/// <summary>
		/// Executes a SQL statement that expects a single result of type <typeparamref name="T"/>, using the provided converter and timeout value.
		/// </summary>
		/// <typeparam name="T">The expected type of the query execution result.</typeparam>
		/// <param name="sqlStatement">A string containing the SQL statement to execute.</param>
		/// <param name="converter">A function that takes a <see cref="MySqlDataReader"/> and produces a <typeparamref name="T"/>-type result.</param>
		/// <param name="timeoutValue">A timeout value (in seconds) for the query.</param>
		/// <returns>A <see typeparam="T"/> representation of the result of the <paramref name="sqlStatement"/>.</returns>
		T ExecuteSqlStatementAsObject<T>(string sqlStatement, Func<MySqlDataReader, T> converter, int timeoutValue);

		/// <summary>
		/// Executes a parameterized SQL statement that expects a single result of type <typeparamref name="T"/>, using the provided converter and set of parameters.
		/// </summary>
		/// <typeparam name="T">The expected type of the query execution result.</typeparam>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="converter">A function that takes a <see cref="MySqlDataReader"/> and produces a <typeparamref name="T"/>-type result.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <returns>A <see typeparam="T"/> representation of the result of the <paramref name="sqlStatement"/>.</returns>
		T ExecuteSqlStatementAsObject<T>(string sqlStatement, Func<MySqlDataReader, T> converter, IEnumerable<MySqlParameter> parameters);

		/// <summary>
		/// Executes a parameterized SQL statement that expects a single result of type <typeparamref name="T"/>, using the provided converter, set of parameters, and timeout value.
		/// </summary>
		/// <typeparam name="T">The expected type of the query execution result.</typeparam>
		/// <param name="sqlStatement">A string containing the parameterized SQL statement to execute.</param>
		/// <param name="converter">A function that takes a <see cref="MySqlDataReader"/> and produces a <typeparamref name="T"/>-type result.</param>
		/// <param name="parameters">A list of MySQL parameters to be passed into the SQL statement.</param>
		/// <param name="timeoutValue">>A timeout value (in seconds) for the query.</param>
		/// <returns>A <see typeparam="T"/> representation of the result of the <paramref name="sqlStatement"/>.</returns>
		T ExecuteSqlStatementAsObject<T>(string sqlStatement, Func<MySqlDataReader, T> converter, IEnumerable<MySqlParameter> parameters, int timeoutValue);

		/// <summary>
		/// Executes a bulk load operation with the provided settings.
		/// </summary>
		/// <param name="settings">The settings for the bulk load operation.</param>
		/// <returns>The number of rows inserted.</returns>
		int ExecuteSqlBulkLoad(MySqlBulkLoadSettings settings);

		/// <summary>
		/// Executes a bulk load operation with the provided settings and timeout value.
		/// </summary>
		/// <param name="settings">The settings for the bulk load operation.</param>
		/// <param name="timeoutValue">The timeout (in seconds) for the bulk load operation.</param>
		/// <returns>The number of rows inserted.</returns>
		int ExecuteSqlBulkLoad(MySqlBulkLoadSettings settings, int timeoutValue);

		/// <summary>
		/// Closes the underlying connection and drops the handle to any in-progress SQL command.
		/// </summary>
		void ReleaseConnection();
	}
}
