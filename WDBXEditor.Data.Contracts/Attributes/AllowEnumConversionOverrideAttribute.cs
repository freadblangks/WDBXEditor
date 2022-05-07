using System;

namespace WDBXEditor.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies whether enum value checking can be toggled for the field or property.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class AllowEnumConversionOverrideAttribute : Attribute
	{
		public readonly bool AllowOverride;

		public AllowEnumConversionOverrideAttribute(bool allowOverride)
		{
			AllowOverride = allowOverride;
		}
	}
}
