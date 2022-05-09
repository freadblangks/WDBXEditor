using System;
using System.Runtime.Serialization;
using WDBXEditor.Data.Helpers.Mapping;

namespace WDBXEditor.Data.Exceptions
{
	/// <summary>
	/// Represents a generic error thrown by an instance of <see cref="SqlDataMapper{T}"/>.
	/// </summary>
	[Serializable]
	public class SqlDataMapperException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDataMapperException"/> class.
		/// </summary>
		public SqlDataMapperException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDataMapperException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public SqlDataMapperException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDataMapperException"/> class with a specified error message and a reference to the inner exception that is the cause of the exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public SqlDataMapperException(string message, Exception inner) : base(message, inner)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDataMapperException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected SqlDataMapperException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}