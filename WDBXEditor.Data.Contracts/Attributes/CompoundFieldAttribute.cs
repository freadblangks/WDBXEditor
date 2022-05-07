using System;

namespace WDBXEditor.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies that a Field or Property maps to multiple columns in MySQL.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class CompoundFieldAttribute : Attribute
	{
	}
}
