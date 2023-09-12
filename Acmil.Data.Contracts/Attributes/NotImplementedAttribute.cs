using System;

namespace Acmil.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies that the field or property has not been implemented.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class NotImplementedAttribute : Attribute
	{
	}
}
