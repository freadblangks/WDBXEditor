using System;
using System.Runtime.Serialization;

namespace Acmil.Data.Exceptions
{
	/// <summary>
	/// Represents an error caused by an attempt to invalidly cast a value from MySQL or store it in a field that would cause data loss.
	/// </summary>
	[Serializable]
	public class ImproperMySqlDataTypeConversionException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImproperMySqlDataTypeConversionException"/> class.
		/// </summary>
		public ImproperMySqlDataTypeConversionException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImproperMySqlDataTypeConversionException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ImproperMySqlDataTypeConversionException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImproperMySqlDataTypeConversionException"/> class with a specified error message and a reference to the inner exception that is the cause of the exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public ImproperMySqlDataTypeConversionException(string message, Exception inner) : base(message, inner)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImproperMySqlDataTypeConversionException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected ImproperMySqlDataTypeConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}