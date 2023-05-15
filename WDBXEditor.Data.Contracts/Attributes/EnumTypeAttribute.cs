using System;

namespace Acmil.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies the Enum Type that values in the Property or Field should be checked against.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public class EnumTypeAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of <see cref="EnumTypeAttribute"/>.
		/// </summary>
		/// <param name="associatedType">The Enum type for the values in the Field or Property to be checked against.</param>
		public EnumTypeAttribute(Type associatedType)
		{
			AssociatedType = associatedType;
		}

		/// <summary>
		/// The enum type associated with the Field or Property.
		/// </summary>
		public Type AssociatedType { get; private set; }
	}
}
