using System.Runtime.Serialization;

namespace Acmil.Core.Exceptions
{
	/// <summary>
	/// Represents an error encountered while reading a DBC.
	/// </summary>
	[Serializable]
	public class DbcReadException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DbcReadException"/> class.
		/// </summary>
		public DbcReadException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DbcReadException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public DbcReadException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DbcReadException"/> class with a specified error message and a reference to the inner exception that is the cause of the exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public DbcReadException(string message, Exception inner) : base(message, inner)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DbcReadException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected DbcReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}