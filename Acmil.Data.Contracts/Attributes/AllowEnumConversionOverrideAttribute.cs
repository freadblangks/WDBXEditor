using System;

namespace Acmil.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies whether enum value checking can be toggled for the field or property.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class AllowEnumConversionOverrideAttribute : Attribute
	{
		public AllowEnumConversionOverrideAttribute(bool allowOverride)
		{
			AllowOverride = allowOverride;
		}

		/// <summary>
		/// True if the associated property or field allows for overriding of enum checking.
		/// Otherwise, false.
		/// </summary>
		public bool AllowOverride { get; private set; }
	}
}
