using System;
using System.Runtime.Serialization;

namespace Acmil.Data.Exceptions
{
	/// <summary>
	/// Represents an error resulting from a command timeout or connection timeout in MySQL.
	/// </summary>
	[Serializable]
	public class MySqlTimeoutException : ExecuteSqlStatementFailedException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlTimeoutException"/> class.
		/// </summary>
		public MySqlTimeoutException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlTimeoutException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public MySqlTimeoutException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlTimeoutException"/> class with a specified error message and a reference to the inner exception that is the cause of the exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public MySqlTimeoutException(string message, Exception inner) : base(message, inner)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlTimeoutException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected MySqlTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}