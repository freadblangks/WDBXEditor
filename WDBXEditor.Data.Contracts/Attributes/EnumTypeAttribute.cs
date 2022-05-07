using System;

namespace WDBXEditor.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies the Enum Type that values in the Property or Field should be checked against.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class EnumTypeAttribute : Attribute
	{
		public readonly Type AssociatedType;

		public EnumTypeAttribute(Type associatedType)
		{
			AssociatedType = associatedType;
		}
	}
}
