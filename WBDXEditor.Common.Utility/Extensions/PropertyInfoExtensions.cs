using System;
using System.Reflection;

namespace WDBXEditor.Common.Utility.Extensions
{
	/// <summary>
	/// Class for <see cref="PropertyInfo"/> extensions.
	/// </summary>
	public static class PropertyInfoExtensions
	{
		/// <summary>
		/// Gets the value of a Property exposed by a custom attribute on a Property.
		/// </summary>
		/// <typeparam name="TCustomAttribute">The custom attribute type.</typeparam>
		/// <typeparam name="TResult">The type of the custom attribute's Property.</typeparam>
		/// <param name="propertyInfo">An instance of <see cref="PropertyInfo"/> with the custom attribute whose Property is to be read.</param>
		/// <param name="propertyName">The name of the custom attribute Property to read.</param>
		/// <returns>The value fo the custom attribute Property.</returns>
		public static TResult GetCustomAttributePropertyValue<TCustomAttribute, TResult>(this PropertyInfo propertyInfo, string propertyName) where TCustomAttribute : Attribute
		{
			var customAttribute = (TCustomAttribute)propertyInfo.GetCustomAttribute(typeof(TCustomAttribute));
			if (customAttribute is null)
			{
				throw new ArgumentException($"Property '{propertyInfo.Name}' does not have an instance of the custom attribute '{typeof(TCustomAttribute).FullName}'.");
			}

			PropertyInfo customAttributePropertyInfo = customAttribute.GetType().GetProperty(propertyName);
			if (customAttributePropertyInfo is null)
			{
				throw new ArgumentException($"Custom Attribute '{typeof(TCustomAttribute).FullName}' has no visible property with the name '{propertyName}'.");
			}

			return (TResult)customAttributePropertyInfo.GetValue(customAttribute);
		}

		/// <summary>
		/// Returns true if the Property has the specified <paramref name="customAttributeType"/>. Otherwise, false.
		/// </summary>
		/// <param name="propertyInfo">An instance of <see cref="PropertyInfo"/> representing the Property to check.</param>
		/// <param name="customAttributeType">The custom attribute type to check for.</param>
		/// <returns>True if the Property has the specified <paramref name="customAttributeType"/>. Otherwise, false.</returns>
		public static bool HasCustomAttribute(this PropertyInfo propertyInfo, Type customAttributeType)
		{
			return !(propertyInfo.GetCustomAttribute(customAttributeType) is null);
		}
	}
}
