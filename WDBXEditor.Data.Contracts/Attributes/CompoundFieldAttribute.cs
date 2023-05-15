using System;

namespace Acmil.Data.Contracts.Attributes
{
	/// <summary>
	/// Specifies that a Field or Property maps to multiple columns in MySQL.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class CompoundFieldAttribute : Attribute
	{
		/// <summary>
		/// Initializes an instance of <see cref="CompoundFieldAttribute"/>.
		/// </summary>
		public CompoundFieldAttribute()
		{

		}

		/// <summary>
		/// Initializes an instance of <see cref="CompoundFieldAttribute"/> for a Field or Property whose type is a Collection.
		/// </summary>
		/// <param name="numberOfInstances">The number of instances of the MySQL column grouping that the Field or Property represents.</param>
		/// <remarks><paramref name="numberOfInstances"/> should match the size of the collection you're expecting to store in the Field or Property.</remarks>
		public CompoundFieldAttribute(int numberOfInstances)
		{
			NumberOfInstances = numberOfInstances;
		}

		/// <summary>
		/// The number of instances of the associated column grouping that the Field or Property represents.
		/// </summary>
		public int NumberOfInstances { get; private set; } = 1;
	}
}
