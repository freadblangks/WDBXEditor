using System;

namespace WDBXEditor.Data.Contracts.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class NotImplementedAttribute : Attribute
	{
	}
}
