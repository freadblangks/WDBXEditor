using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WDBXEditor.Data.Contexts.QueryRetry;
using WDBXEditor.Data.Exceptions;
using WDBXEditor.Data.Helpers;
using WDBXEditor.Data.Helpers.Connections;
using WDBXEditor.Data.Helpers.Connections.Interfaces;
using WDBXEditor.Data.Helpers.Interfaces;

namespace WDBXEditor.Data.Contexts
{
	/// <summary>
	/// Database context for interacting with a MySQL Server instance.
	/// </summary>
	[Serializable]
	public class MySqlContext : IDbContext
	{
		private IMySqlConnectionProvider _mySqlConnectionProvider;

		[NonSerialized]
		private MySqlConnection _connection;

		[NonSerialized]
		private MySqlTransaction _transaction;

		[NonSerialized]
		private MySqlCommand _currentCommand;

		[NonSerialized]
		bool _invalidOperationOccurred = false;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlContext"/>.
		/// </summary>
		/// <param name="connectionString">A connection string for connecting to the MySQL server instance.</param>
		public MySqlContext(string connectionString)
		{
			SetProvider(MySqlConnectionProviderFactory.Instance, connectionString);
		}

		//public MySqlContext(MySqlConnection connection, IDbConfigHelper configHelper)
		//{
		//	_connection = connection;
		//	_configHelper = configHelper;
		//}

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlConnection"/>.
		/// </summary>
		/// <param name="serverHostname">The hostname for the instance of MySQL server to connect to.</param>
		/// <param name="userId">The user ID to connect with.</param>
		/// <param name="password">The password for the user.</param>
		/// <param name="database">(Optional) The name of the database to connect to</param>
		public MySqlContext(string serverHostname, string userId, string password, string database = "")
		{
			string connectionString = $"Server={serverHostname};Uid={userId};Pwd={password}";

			if (!string.IsNullOrWhiteSpace(database))
			{
				connectionString += $";Database={database}";
			}
			SetProvider(MySqlConnectionProviderFactory.Instance, connectionString);
		}

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlcontext"/> using the specified <see cref="IMySqlConnectionProvider"/>.
		/// </summary>
		/// <param name="connectionProvider">An implementation of <see cref="IMySqlConnectionProvider"/> to get a connection from.</param>
		public MySqlContext(IMySqlConnectionProvider connectionProvider)
		{
			_mySqlConnectionProvider = connectionProvider;
		}

		internal void SetProvider(IMySqlConnectionProviderFactory connectionFactory, string connectionString)
		{
			_mySqlConnectionProvider = connectionFactory.GetMySqlConnectionProvider(connectionString);
		}

		#endregion

		#region Connection Properties

		/// <summary>
		/// Gets the database name to which the context is connected.
		/// </summary>
		public string Database => _mySqlConnectionProvider.DatabaseName;

		/// <summary>
		/// Gets the hostname of the server to which the context is connected.
		/// </summary>
		public string ServerName => _mySqlConnectionProvider.ServerName;

		#endregion

		private bool CanRetryQuery => GetTransaction() == null;

		#region Execute NonQuery

		public int ExecuteNonQuerySqlStatement(string sqlStatement)
		{
			return ExecuteNonQuerySqlStatement(sqlStatement, null, -1);
		}

		public int ExecuteNonQuerySqlStatement(string sqlStatement, int timeoutValue)
		{
			return ExecuteNonQuerySqlStatement(sqlStatement, null, timeoutValue);
		}

		public int ExecuteNonQuerySqlStatement(string sqlStatement, IEnumerable<MySqlParameter> parameters)
		{
			return ExecuteNonQuerySqlStatement(sqlStatement, parameters, -1);
		}

		public int ExecuteNonQuerySqlStatement(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue)
		{
			if (string.IsNullOrWhiteSpace(sqlStatement))
			{
				throw new ArgumentNullException(nameof(sqlStatement));
			}

			int retVal = 0;
			MySqlCommand command = null;
			timeoutValue = SanitizeCommandTimeout(timeoutValue);

			// TODO: Add logging for SQL execution.
			try
			{
				command = GetConnectionWithRetries().CreateCommand();
				command.Transaction = GetTransaction();
				if (parameters != null)
				{
					command.Parameters.AddRange(parameters.ToArray());
				}
				command.CommandText = sqlStatement;
				command.CommandTimeout = timeoutValue;

				var retryPolicy = new QueryRetryPolicy<int>(() =>
				{
					EnsureOpenConnection();

					// TODO: Make sure to pass logger here when we add it.
					return command.ExecuteNonQuery();
				});
				retryPolicy.RetryOnConnectionTimeout();
				retryPolicy.RetryWithoutQueryHints(
					sqlStatement,
					updatedText =>
					{
						command.CommandText = updatedText;
						EnsureOpenConnection();
						return command.ExecuteNonQuery();
					}
				);

				if (CanRetryQuery)
				{
					retryPolicy.RetryOnDeadlock();
					retryPolicy.RetryOnCommandTimeout();
				}

				retryPolicy.ReturnValueOnCancellation(-1);
				retVal = retryPolicy.RunQuery();
			}
			catch (Exception ex)
			{
				// TODO: Add log statement here.
				ExceptionHelper.ThrowWrappedException(ex, sqlStatement, parameters);
			}
			finally
			{
				ReleaseConnection();
			}

			return retVal;
		}

		#endregion

		#region Execute As Scalar

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, int timeoutValue = -1)
		{
			return ExecuteSqlStatementAsScalar<T>(sqlStatement, null, timeoutValue);
		}

		public T ExecuteSqlStatementAsScalar<T>(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue = -1)
		{
			return (T)ExecuteSqlStatementAsScalar(sqlStatement, parameters, timeoutValue);
		}

		public object ExecuteSqlStatementAsScalar(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue = -1)
		{
			if (string.IsNullOrWhiteSpace(sqlStatement))
			{
				throw new ArgumentNullException(nameof(sqlStatement));
			}
			parameters ??= new MySqlParameter[] { };
			timeoutValue = SanitizeCommandTimeout(timeoutValue);

			
			object retVal = null;
			try
			{
				MySqlCommand command = GetConnectionWithRetries().CreateCommand();
				command.Transaction = GetTransaction();
				command.CommandText = sqlStatement;
				_currentCommand = command;
				command.CommandTimeout = timeoutValue;
				command.Parameters.AddRange(parameters.ToArray());

				// TODO: Add logging here.
				var retryPolicy = new QueryRetryPolicy<object>(() => { EnsureOpenConnection(); return command.ExecuteScalar(); });
				retryPolicy.RetryOnConnectionTimeout();
				retryPolicy.RetryWithoutQueryHints(
					sqlStatement,
					updatedText =>
					{
						command.CommandText = updatedText;
						EnsureOpenConnection();
						return command.ExecuteScalar();
					}
				);
				retryPolicy.ReturnValueOnCancellation(null);

				if (CanRetryQuery)
				{
					retryPolicy.RetryOnDeadlock();
					retryPolicy.RetryOnCommandTimeout();
				}

				retVal = retryPolicy.RunQuery();
			}
			catch (Exception ex)
			{
				// TODO: Add logging here.
				ExceptionHelper.ThrowWrappedException(ex, sqlStatement, parameters);
			}
			finally
			{
				ReleaseConnection();
			}

			return retVal;
		}

		#endregion

		#region Bulk Load

		public int ExecuteSqlBulkLoad(MySqlBulkLoadSettings settings)
		{
			return ExecuteSqlBulkLoad(settings, -1);
		}

		public int ExecuteSqlBulkLoad(MySqlBulkLoadSettings settings, int timeoutValue)
		{
			ValidateMySqlBulkLoadSettings(settings);
			timeoutValue = SanitizeCommandTimeout(timeoutValue);

			int rowsInserted = 0;
			try
			{
				// TODO: Wrap this in a retry.
				// That will likely require adding a new type of policy to somehow clean up
				// after a partially finished bulk load.
				EnsureOpenConnection();
				var bulkLoader = new MySqlBulkLoader(_connection)
				{
					TableName = settings.TableName,
					FieldTerminator = settings.FieldTerminator,
					LineTerminator = settings.LineTerminator,
					NumberOfLinesToSkip = settings.NumberOfLinesToSkip,
					FileName = settings.FilePath,
					FieldQuotationCharacter = settings.FieldQuotationCharacter,
					CharacterSet = settings.CharacterSet,
					Timeout = timeoutValue
				};

				rowsInserted = bulkLoader.Load();
			}
			catch (Exception ex)
			{
				throw new SqlBulkLoadException("Error during SQL Bulk Load operation", ex);
			}
			finally
			{
				ReleaseConnection();
			}

			return rowsInserted;
		}

		#endregion

		#region Execute As List

		public List<T> ExecuteSqlStatementAsList<T>(string sqlStatement, Func<MySqlDataReader, T> converter)
		{
			return ExecuteSqlStatementAsList(sqlStatement, converter, null, -1);
		}

		public List<T> ExecuteSqlStatementAsList<T>(string sqlStatement, Func<MySqlDataReader, T> converter, int timeoutValue)
		{
			return ExecuteSqlStatementAsList(sqlStatement, converter, null, timeoutValue);
		}

		public List<T> ExecuteSqlStatementAsList<T>(string sqlStatement, Func<MySqlDataReader, T> converter, IEnumerable<MySqlParameter> parameters)
		{
			return ExecuteSqlStatementAsList(sqlStatement, converter, parameters, -1);
		}

		public List<T> ExecuteSqlStatementAsList<T>(string sqlStatement, Func<MySqlDataReader, T> converter, IEnumerable<MySqlParameter> parameters, int timeoutValue)
		{
			List<T> retVal = null;
			parameters ??= new MySqlParameter[] { };

			try
			{
				Func<List<T>> readAndConvertSqlDataReader = () =>
				{
					var result = new List<T>();
					using (MySqlDataReader reader = ExecuteSqlStatementAsDataReader(sqlStatement, parameters, timeoutValue))
					{
						while ((bool)(reader?.Read()))
						{
							result.Add(converter(reader));
						}
						return result;
					}
				};

				// TODO: Add logging here.
				var retryPolicy = new QueryRetryPolicy<List<T>>(readAndConvertSqlDataReader);
				if (CanRetryQuery)
				{
					retryPolicy.RetryOnDeadlock();
					retryPolicy.RetryOnCommandTimeout();
				}
				retVal = retryPolicy.RunQuery();
			}
			catch (Exception ex)
			{
				ExceptionHelper.ThrowWrappedException(ex, sqlStatement, parameters);
			}
			finally
			{
				ReleaseConnection();
			}

			return retVal;
		}

		#endregion

		#region Connection

		/// <summary>
		/// Gets the current underlying transaction being managed by this Context.
		/// </summary>
		/// <returns>The current underlying transaction being managed by this Context.</returns>
		public MySqlTransaction GetTransaction()
		{
			return _transaction;
		}

		public void BeginTransaction()
		{
			GetConnection();
			_transaction = _connection.BeginTransaction();
			// TODO: Add logging here.
		}

		public void CommitTransation()
		{
			int attempts = 0;
			if (_transaction == null)
			{
				throw new InvalidOperationException("There is no transaction to commit.");
			}

			try
			{
				// TODO: Add logging here.
				var retryPolicy = new QueryRetryPolicy<bool>(() =>
				{
					try
					{
						++attempts;
						_transaction.Commit();
						_transaction = null;
						// TODO: Clear the transaction in the logger here.
					}

					// TODO: If we encounter an exception about the transaction being completed and
					// no longer usable, add a check for it here.
					catch (InvalidOperationException ioe) when (attempts > 1)
					{
						// TODO: Log warning here.
					}

					return true;
				});

				retryPolicy.RetryOnConnectionTimeout();
				retryPolicy.RetryOnCommandTimeout();
				retryPolicy.RunQuery();
			}
			catch (Exception ex)
			{
				// TODO: Add logging here.
				throw;
			}

			try
			{
				_connection?.Close();
			}
			catch (Exception ex)
			{
				// TODO: Add logging here.
				throw;
			}
		}

		public void ReleaseConnection()
		{
			if (_transaction == null)
			{
				_connection?.Close();
			}
			ClearCurrentCommand();
		}

		/// <summary>
		/// Drops the reference to the current SQL command.
		/// </summary>
		private void ClearCurrentCommand()
		{
			if (_currentCommand != null && _currentCommand.Parameters != null)
			{
				// This is in case the same parameters are used in another command.
				_currentCommand.Parameters.Clear();
			}

			_currentCommand = null;
		}

		/// <summary>
		/// Drops the reference to the parameters of the current SQL command for DataReaders, 
		/// but keeps the current command so that the underlying query can be cancelled.
		/// </summary>
		private void ClearCurrentCommandParametersForReader()
		{
			if (_currentCommand != null && _currentCommand.Parameters != null)
			{
				_currentCommand.Parameters.Clear();
			}
		}

		public MySqlConnection GetConnection() => GetConnection(true);

		public MySqlConnection GetConnection(bool openConnectionIfClosed)
		{
			EnsureConnection(!openConnectionIfClosed);
			if (openConnectionIfClosed)
			{
				EnsureOpenConnection();
			}

			return _connection;
		}

		private MySqlConnection GetConnectionWithRetries() => GetConnectionWithRetries(true);

		private MySqlConnection GetConnectionWithRetries(bool openConnectionIfClosed)
		{
			var policy = new QueryRetryPolicy<MySqlConnection>(() => GetConnection(openConnectionIfClosed));
			policy.RetryOnConnectionTimeout();
			return policy.RunQuery();
		}

		private void EnsureConnection(bool includeDatabase)
		{
			if (_connection == null)
			{
				_connection = _mySqlConnectionProvider.GetConnection(includeDatabase);
			}
		}

		private void EnsureOpenConnection()
		{
			if (_connection.State == System.Data.ConnectionState.Broken)
			{
				_connection.Close();
			}

			if (_connection.State == System.Data.ConnectionState.Closed)
			{
				_connection.Open();
				if (_mySqlConnectionProvider.ChangeDatabaseOnConnectionReOpen)
				{
					_connection.ChangeDatabase(Database);
				}
			}
		}

		#endregion

		#region Execute As DataTable

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement)
		{
			return ExecuteSqlStatementAsDataTable(sqlStatement, null, -1);
		}

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, int timeoutValue)
		{
			return ExecuteSqlStatementAsDataTable(sqlStatement, null, timeoutValue);
		}

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, IEnumerable<MySqlParameter> parameters)
		{
			return ExecuteSqlStatementAsDataTable(sqlStatement, parameters, -1);
		}

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue)
		{
			return ExecuteSqlStatementAsDataTable(sqlStatement, parameters, timeoutValue);
		}

		public DataTable ExecuteSqlStatementAsDataTable(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue, bool retryWithoutQueryHints)
		{
			if (string.IsNullOrEmpty(sqlStatement))
			{
				throw new ArgumentNullException(nameof(sqlStatement));
			}
			timeoutValue = SanitizeCommandTimeout(timeoutValue);
			var dataTable = new DataTable();

			try
			{
				MySqlCommand command = GetConnectionWithRetries().CreateCommand();
				MySqlTransaction transaction = GetTransaction();
				command.Transaction = transaction;
				_currentCommand = command;
				command.CommandText = sqlStatement;
				command.CommandTimeout = timeoutValue;
				if (parameters != null)
				{
					command.Parameters.AddRange(parameters.ToArray());
				}

				MySqlDataAdapter dataAdapter;
				var retryPolicy = new QueryRetryPolicy<DataTable>(() =>
				{
					EnsureOpenConnection();
					dataAdapter = new MySqlDataAdapter(command);
					return FillDataTable(dataAdapter);
				});

				retryPolicy.RetryOnConnectionTimeout();
				if (retryWithoutQueryHints)
				{
					retryPolicy.RetryWithoutQueryHints(
						sqlStatement,
						updatedText =>
						{
							command.CommandText = updatedText;
							EnsureOpenConnection();
							dataAdapter = new MySqlDataAdapter(command);
							return FillDataTable(dataAdapter);
						}
					);
				}

				if (CanRetryQuery)
				{
					retryPolicy.RetryOnDeadlock();
					retryPolicy.RetryOnCommandTimeout();
				}

				retryPolicy.ReturnValueOnCancellation(null);
				dataTable = retryPolicy.RunQuery();
			}
			catch (Exception ex)
			{
				// TODO: Add logging here.
				ExceptionHelper.ThrowWrappedException(ex, sqlStatement, parameters);
			}
			finally
			{
				ReleaseConnection();
			}

			return dataTable;
		}

		private DataTable FillDataTable(MySqlDataAdapter dataAdapter)
		{
			var table = new DataTable();
			dataAdapter.Fill(table);
			return table;
		}

		#endregion

		#region Execute As DB Reader

		public MySqlDataReader ExecuteSqlStatementAsDataReader(string sqlStatement)
		{
			return ExecuteSqlStatementAsDataReader(sqlStatement, null, -1);
		}

		public MySqlDataReader ExecuteSqlStatementAsDataReader(string sqlStatement, int timeoutValue)
		{
			return ExecuteSqlStatementAsDataReader(sqlStatement, null, timeoutValue);
		}

		public MySqlDataReader ExecuteSqlStatementAsDataReader(string sqlStatement, IEnumerable<MySqlParameter> parameters)
		{
			return ExecuteSqlStatementAsDataReader(sqlStatement, parameters, -1);
		}

		public MySqlDataReader ExecuteSqlStatementAsDataReader(string sqlStatement, IEnumerable<MySqlParameter> parameters, int timeoutValue)
		{
			if (string.IsNullOrEmpty(sqlStatement))
			{
				throw new ArgumentNullException(nameof(sqlStatement));
			}
			timeoutValue = SanitizeCommandTimeout(timeoutValue);

			MySqlDataReader retValue = null;
			try
			{
				MySqlCommand command = GetConnectionWithRetries().CreateCommand();
				command.Transaction = GetTransaction();
				command.CommandText = sqlStatement;
				command.CommandTimeout = timeoutValue;
				_currentCommand = command;

				if (parameters != null)
				{
					command.Parameters.AddRange(parameters.ToArray());
				}
				CommandBehavior commandBehavior = CalculateCommandBehaviorForReader(command, sequentialAccess: false);

				var retryPolicy = new QueryRetryPolicy<MySqlDataReader>(() => { EnsureOpenConnection(); return command.ExecuteReader(commandBehavior); });
				retryPolicy.RetryOnConnectionTimeout();
				retryPolicy.OnAnyException(() => ReleaseConnection());
				retryPolicy.ReturnValueOnCancellation(null);

				retValue = retryPolicy.RunQuery();
			}
			catch (Exception ex)
			{
				// TODO: Add logging here.
				ExceptionHelper.ThrowWrappedException(ex, sqlStatement, parameters);
			}
			finally
			{
				ClearCurrentCommandParametersForReader();
			}

			return retValue;
		}

		internal static CommandBehavior CalculateCommandBehaviorForReader(MySqlCommand command, bool sequentialAccess)
		{
			return CalculateCommandBehaviorForReader(command, sequentialAccess, false);
		}

		private static CommandBehavior CalculateCommandBehaviorForReader(MySqlCommand command, bool sequentialAccess, bool includeSchemaInfo)
		{
			var commandBehavior = CommandBehavior.CloseConnection;
			if (command.Transaction != null)
			{
				commandBehavior = CommandBehavior.Default;
			}

			if (sequentialAccess)
			{
				commandBehavior |= CommandBehavior.SequentialAccess;
			}

			if (includeSchemaInfo)
			{
				commandBehavior |= CommandBehavior.KeyInfo;
			}

			return commandBehavior;
		}

		#endregion

		#region Execute As Object

		public T ExecuteSqlStatementAsObject<T>(string sqlStatement, Func<MySqlDataReader, T> converter)
		{
			return ExecuteSqlStatementAsObject(sqlStatement, converter, null, -1);
		}

		public T ExecuteSqlStatementAsObject<T>(string sqlStatement, Func<MySqlDataReader, T> converter, int timeoutValue)
		{
			return ExecuteSqlStatementAsObject(sqlStatement, converter, null, timeoutValue);
		}

		public T ExecuteSqlStatementAsObject<T>(string sqlStatement, Func<MySqlDataReader, T> converter, IEnumerable<MySqlParameter> parameters)
		{
			return ExecuteSqlStatementAsObject(sqlStatement, converter, parameters, -1);
		}

		public T ExecuteSqlStatementAsObject<T>(string sqlStatement, Func<MySqlDataReader, T> converter, IEnumerable<MySqlParameter> parameters, int timeoutValue)
		{
			T retVal = default;
			parameters ??= new MySqlParameter[] { };

			try
			{
				Func<T> readAndConvertSqlDataReader = () =>
				{
					T result = default;
					using (MySqlDataReader reader = ExecuteSqlStatementAsDataReader(sqlStatement, parameters, timeoutValue))
					{
						if ((bool)(reader?.Read()))
						{
							result = converter(reader);
						}

						return result;
					}
				};

				var retryPolicy = new QueryRetryPolicy<T>(readAndConvertSqlDataReader);
				if (CanRetryQuery)
				{
					retryPolicy.RetryOnDeadlock();
					retryPolicy.RetryOnCommandTimeout();
				}

				retVal = retryPolicy.RunQuery();
			}
			catch (Exception ex)
			{
				ExceptionHelper.ThrowWrappedException(ex, sqlStatement, parameters);
			}
			finally
			{
				ReleaseConnection();
			}

			return retVal;
		}

		#endregion

		private void ValidateMySqlBulkLoadSettings(MySqlBulkLoadSettings settings)
		{
			if (string.IsNullOrWhiteSpace(settings.TableName))
			{
				throw new ArgumentNullException(nameof(MySqlBulkLoadSettings.TableName));
			}
			else if (string.IsNullOrWhiteSpace(settings.FieldTerminator))
			{
				throw new ArgumentNullException(nameof(MySqlBulkLoadSettings.FieldTerminator));
			}
			else if (string.IsNullOrWhiteSpace(settings.FilePath))
			{
				throw new ArgumentNullException(nameof(MySqlBulkLoadSettings.FilePath));
			}
		}

		private int SanitizeCommandTimeout(int potentialTimeout)
		{
			return (potentialTimeout > 0) ? potentialTimeout : DbConfigHelper.DefaultCommandTimeout;
		}
	}
}
