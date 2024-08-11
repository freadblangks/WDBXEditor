using System.Runtime.Serialization;

namespace Acmil.Core.Exceptions
{
	/// <summary>
	/// Represents an error encountered writing a DBC to disk.
	/// </summary>
	[Serializable]
	public class DbcWriteException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DbcWriteException"/> class.
		/// </summary>
		public DbcWriteException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DbcWriteException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public DbcWriteException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DbcWriteException"/> class with a specified error message and a reference to the inner exception that is the cause of the exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public DbcWriteException(string message, Exception inner) : base(message, inner)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DbcWriteException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected DbcWriteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}