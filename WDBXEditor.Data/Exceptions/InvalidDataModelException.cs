using System;
using System.Runtime.Serialization;

namespace WDBXEditor.Data.Exceptions
{
	/// <summary>
	/// Represents an error caused by a model or submodel whose structure does not follow the expected rules for a model or submodel in the Extended WDBX Editor project.
	/// </summary>
	/// TODO: Define the rules somewhere.
	[Serializable]
	public class InvalidDataModelException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidDataModelException"/> class.
		/// </summary>
		public InvalidDataModelException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidDataModelException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public InvalidDataModelException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidDataModelException"/> class with a specified error message and a reference to the inner exception that is the cause of the exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public InvalidDataModelException(string message, Exception inner) : base(message, inner)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidDataModelException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		protected InvalidDataModelException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}