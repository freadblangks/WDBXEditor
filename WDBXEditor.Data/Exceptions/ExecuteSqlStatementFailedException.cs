using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WDBXEditor.Data.Exceptions
{
	/// <summary>
	/// Represents an error that caused the execution of a SQL statement to fail.
	/// </summary>
	[Serializable]
	public class ExecuteSqlStatementFailedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExecuteSqlStatementFailedException"/> class.
		/// </summary>
		public ExecuteSqlStatementFailedException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecuteSqlStatementFailedException"/> class with the specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ExecuteSqlStatementFailedException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecuteSqlStatementFailedException"/> class with the specified error message and a reference to the inner exception that is the cause of the exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public ExecuteSqlStatementFailedException(string message, Exception inner) : base(message, inner)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecuteSqlStatementFailedException"/> class with the specified inner exception and the SQL statement whose failed execution produced this exception.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		/// <param name="executedSql">The SQL statement whose failed execution produced this exception.</param>
		public ExecuteSqlStatementFailedException(Exception innerException, string executedSql) : base(ReplaceWithDefaultMessageIfEmpty(""), innerException)
		{
			ExecutedStatement = executedSql;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecuteSqlStatementFailedException"/> class with the specified inner exception as well as the SQL statement whose failed execution produced this exception and its parameters.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		/// <param name="executedSql">The SQL statement whose failed execution produced this exception.</param>
		/// <param name="parameters">The parameters the failed SQL statement was executed with.</param>
		public ExecuteSqlStatementFailedException(Exception innerException, string executedSql, IEnumerable<MySqlParameter> parameters) : this(innerException, executedSql)
		{
			SqlParameters = parameters;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecuteSqlStatementFailedException"/> class with the specified error message, the SQL statement whose failed execution produced this exception, and an inner exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="executedSql">The SQL statement whose failed execution produced this exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public ExecuteSqlStatementFailedException(string message, string executedSql, Exception innerException) : base(ReplaceWithDefaultMessageIfEmpty(message), innerException)
		{
			ExecutedStatement = executedSql;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecuteSqlStatementFailedException"/> with the specified error message and inner exception as well as the SQL statement whose failed execution produced this exception and its parameters.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="executedSql">The SQL statement whose failed execution produced this exception.</param>
		/// <param name="parameters">The parameters the failed SQL statement was executed with.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public ExecuteSqlStatementFailedException(string message, string executedSql, IEnumerable<MySqlParameter> parameters, Exception innerException) : this(message, executedSql, innerException)
		{
			SqlParameters = parameters;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecuteSqlStatementFailedException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected ExecuteSqlStatementFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}

		/// <summary>
		/// The SQL statement whose execution threw the exception.
		/// </summary>
		public string ExecutedStatement { get; set; } = "";

		/// <summary>
		/// All <see cref="MySqlParameter"/>s that the failed SQL statement was executed with.
		/// </summary>
		public IEnumerable<MySqlParameter> SqlParameters { get; set; } = null;

		/// <inheritdoc/>
		public override string ToString()
		{
			string retVal = base.ToString();
			if (!string.IsNullOrWhiteSpace(ExecutedStatement))
			{
				retVal += $"{Environment.NewLine}SQL STatement Failed:{Environment.NewLine}{ExecutedStatement}{FormatParams()}";
			}

			return retVal;
		}

		private static string ReplaceWithDefaultMessageIfEmpty(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				message = "SQL Statement Failed";
			}

			return message;
		}

		private string FormatParams()
		{
			string message = "";
			try
			{
				if (SqlParameters != null)
				{
					var stringBuilder = new StringBuilder();
					foreach (MySqlParameter param in SqlParameters)
					{
						if (param.Value != null)
						{
							stringBuilder.Append(Environment.NewLine);
							stringBuilder.Append($"{param.ParameterName} = {param.Value}");
						}
					}

					message = stringBuilder.ToString();
				}
			}
			catch
			{
				// Gobble up that exception, yessir.
			}

			return message;
		}
	}
}
