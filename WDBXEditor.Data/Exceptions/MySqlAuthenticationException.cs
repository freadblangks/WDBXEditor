using System;
using System.Runtime.Serialization;

namespace Acmil.Data.Exceptions
{
	/// <summary>
	/// Represents an error thrown when MySQL server encounters a failure due to failed authentication.
	/// </summary>
	[Serializable]
	public class MySqlAuthenticationException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlAuthenticationException"/> class.
		/// </summary>
		public MySqlAuthenticationException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlAuthenticationException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public MySqlAuthenticationException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlAuthenticationException"/> class with a specified error message and a reference to the inner exception that is the cause of the exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public MySqlAuthenticationException(string message, Exception inner) : base(message, inner)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlAuthenticationException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected MySqlAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}
