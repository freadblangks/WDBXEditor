using System;

namespace Acmil.Data.Exceptions
{
	/// <summary>
	/// Represents an error thrown during a retry.
	/// </summary>
	[Serializable]
	public class RetryPolicyException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RetryPolicyException"/> to wrap the provided Exception.
		/// </summary>
		/// <param name="ex">The Exception to wrap.</param>
		public RetryPolicyException(Exception ex) : base("", ex)
		{

		}
	}
}
