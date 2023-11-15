using Acmil.Data.Helpers.Mapping.Interfaces;
using System.Reflection;

namespace Acmil.Data.Helpers.Mapping
{
	public class ModelCustomAttributeHelper : IModelCustomAttributeHelper
	{
		public T GetPropertyCustomAttribute<T>(PropertyInfo propertyInfo) where T : Attribute
		{
			return propertyInfo.GetCustomAttribute<T>();
		}
	}
}
