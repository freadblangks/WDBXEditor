using System.Reflection;

namespace Acmil.Data.Helpers.Mapping.Interfaces
{
	public interface IModelCustomAttributeHelper
	{
		public T GetPropertyCustomAttribute<T>(PropertyInfo propertyInfo) where T : Attribute;
	}
}
