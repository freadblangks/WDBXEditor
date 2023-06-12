using Acmil.Common.Utility.Extensions;
using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Types.Primitives;
using Acmil.Data.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Acmil.Data.Helpers.Mapping
{
	public class SqlDataMapper<T> where T : new()
	{
		private const string _TEMPLATED_NUMBER_PATTERN = "\\[\\d:\\d+\\]";

		private readonly Dictionary<string, Action<MySqlDataReader, T>> MapFunctions = new Dictionary<string, Action<MySqlDataReader, T>>();

		/// <summary>
		/// Initializes a new instance of <see cref="SqlDataMapper{T}"/>.
		/// </summary>
		public SqlDataMapper()
		{
			PopulateMapFunctionsDict(-1);
		}

		private SqlDataMapper(int instanceNumber)
		{
			PopulateMapFunctionsDict(instanceNumber);
		}

		public T MapSqlDataRow(MySqlDataReader reader)
		{
			T result = new T();
			foreach (var propertyInfo in typeof(T).GetProperties())
			{
				// TODO: Add the case here for array-type properties.
				if (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameAttribute)) || propertyInfo.HasCustomAttribute(typeof(CompoundFieldAttribute)) || propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameTemplateAttribute)))
				{
					MapFunctions[propertyInfo.Name](reader, result);
				}
			}

			return result;
		}
		private void PopulateMapFunctionsDict(int instanceNumber)
		{
			foreach (var propertyInfo in typeof(T).GetProperties())
			{
				if (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameAttribute)) || propertyInfo.HasCustomAttribute(typeof(CompoundFieldAttribute)) || propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameTemplateAttribute)))
				{
					Action<MySqlDataReader, T> mapFunction = BuildMapFunction(propertyInfo, instanceNumber);
					MapFunctions.Add(propertyInfo.Name, mapFunction);
				}

			}
		}

		/// <summary>
		/// Builds a function to read a value from a MySqlDataReader and return the result as type <typeparamref name="U"/> so that it can be saved to the property specified by <paramref name="propertyInfo"/>.
		/// </summary>
		/// <typeparam name="U">The type that the built function should return. This should match the type for <paramref name="propertyInfo"/>.</typeparam>
		/// <param name="propertyInfo">An instance of <see cref="PropertyInfo"/> representing the property the returned function should return a value for.</param>
		/// <returns>A function to read a value from a MySqlDataReader and return the result as type <typeparamref name="U"/>.</returns>
		private Action<MySqlDataReader, T> BuildMapFunction(PropertyInfo propertyInfo, int instanceNumber = -1)
		{
			Action<MySqlDataReader, T> mapFunction = null;

			try
			{
				// If the property is marked as a [CompoundField], it's a reference type whose properties need to be populated.
				// So, we'll create an inner SqlDataMapper<T> that we'll use to define the functions to set its properties.
				if (propertyInfo.HasCustomAttribute(typeof(CompoundFieldAttribute)))
				{
					mapFunction = GetMapFunctionForCompoundField(propertyInfo);
				}

				else if (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameTemplateAttribute)))
				{
					mapFunction = GetMapFunctionForPropertyWithTemplatedMySqlColumnName(propertyInfo, instanceNumber);
				}

				// If the property has the [MySqlColumnName()] attribute, we know it's a property containing a value type
				// that maps to only one column in table.
				else if (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameAttribute)))
				{
					string mySqlColumnName = propertyInfo.GetCustomAttributePropertyValue<MySqlColumnNameAttribute, string>("Name");
					mapFunction = GetMapFunctionForValueType(propertyInfo, mySqlColumnName);
				}
			}
			catch (InvalidDataModelException ex)
			{
				// TODO: Add link to documentation on the standards to this error message.
				string errorMessage = $"A map function could not be generated for {propertyInfo.DeclaringType.FullName}.{propertyInfo.Name} because it or the containing class is in violation of this project's data model standards. See inner exception for details.";
				throw new InvalidDataModelException(errorMessage, ex);
			}
			catch (Exception ex)
			{
				string errorMessage = $"A map function could not be generated for {propertyInfo.DeclaringType.FullName}.{propertyInfo.Name}.";
				throw new SqlDataMapperException(errorMessage, ex);
			}

			return mapFunction;
		}

		private Action<MySqlDataReader, T> GetMapFunctionForCompoundField(PropertyInfo propertyInfo)
		{
			Action<MySqlDataReader, T> mapFunction = null;

			if (propertyInfo.PropertyType.ImplementsInterface<ICollection>())
			{
				Type elementType = propertyInfo.PropertyType.GetElementType();
				int collectionSize = propertyInfo.GetCustomAttributePropertyValue<CompoundFieldAttribute, int>(nameof(CompoundFieldAttribute.NumberOfInstances));

				mapFunction = (reader, modelInstance) =>
				{
					// Create a new instance of SqlDataMapper<T>, where T is the type of the collection-type property's contents.
					// For example, if the property was of type int[], T would be int.
					dynamic collectionInstance = Activator.CreateInstance(propertyInfo.PropertyType, new object[] { collectionSize });
					for (int i = 0; i < collectionSize; ++i)
					{
						dynamic innerDataMapper = Activator.CreateInstance(GetType().GetGenericTypeDefinition().MakeGenericType(new Type[] { elementType }), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { i + 1 }, null, null);
						collectionInstance[i] = innerDataMapper.MapSqlDataRow(reader);
					}
					propertyInfo.SetValue(modelInstance, collectionInstance);
				};
			}
			else
			{
				// Create a new instance of SqlDataMapper<T>, where T is the type of the property marked as [CompoundField].
				dynamic innerDataMapper = Activator.CreateInstance(GetType().GetGenericTypeDefinition().MakeGenericType(new Type[] { propertyInfo.PropertyType }));
				mapFunction = (reader, modelInstance) =>
				{
					propertyInfo.SetValue(modelInstance, innerDataMapper.MapSqlDataRow(reader));
				};
			}

			return mapFunction;
		}

		private Action<MySqlDataReader, T> GetMapFunctionForPropertyWithTemplatedMySqlColumnName(PropertyInfo propertyInfo, int instanceNumber)
		{
			Action<MySqlDataReader, T> mapFunction = null;

			// It's a collection of value types.
			if (propertyInfo.PropertyType.ImplementsInterface<ICollection>())
			{
				throw new NotImplementedException("Collections of value types are not currently supported for mapping.");
			}

			// It's a property of a [CompoundField] in a collection.
			else
			{
				if (instanceNumber == -1)
				{
					string errorMessage = $"{nameof(MySqlColumnNameTemplateAttribute)} is only allowed on Field and Properties that are either on a submodel used as a Field/Property that's marked with {nameof(CompoundFieldAttribute)}, or on a Field/Property whose type is a collection.";
					throw new InvalidDataModelException(errorMessage);
				}
				string mySqlColumnName = BuildMySqlColumnNameFromTemplate(propertyInfo.GetCustomAttributePropertyValue<MySqlColumnNameTemplateAttribute, string>("TemplateString"), instanceNumber);
				mapFunction = GetMapFunctionForValueType(propertyInfo, mySqlColumnName);
			}

			return mapFunction;
		}

		private Action<MySqlDataReader, T> GetMapFunctionForValueType(PropertyInfo propertyInfo, string mySqlColumnName)
		{
			Action<MySqlDataReader, T> mapFunction = null;
			Type targetPropertyType = propertyInfo.PropertyType;

			// This gets us a reference to GetDataReaderFieldConversionFunction where the TTarget typeparam has been set as targetPropertyType.
			// Because typing in C# is an actual nightmare sometimes.
			MethodInfo getDataReaderConversionFunction = GetType().GetMethod(nameof(GetDataReaderFieldConversionFunction), BindingFlags.NonPublic | BindingFlags.Static)
				.MakeGenericMethod(new Type[] { targetPropertyType });

			mapFunction = (reader, modelInstance) =>
			{
				// TODO: Figure out if we can move this line out of the mapFunction.
				dynamic conversionFunction = getDataReaderConversionFunction.Invoke(this, new object[] { targetPropertyType });

				// Use reflection to set the value of the property.
				propertyInfo.SetValue(modelInstance, conversionFunction(reader[mySqlColumnName]));
			};

			return mapFunction;
		}

		/// <summary>
		/// Gets a function to convert an object taken from a MySqlDataReader into the desired type.
		/// </summary>
		/// <typeparam name="TTarget">The type we want to convert to. NOTE: This MUST be the same type as <paramref name="targetType"/></typeparam>
		/// <param name="targetType">The type we want to convert to. NOTE: This MUST be the same type as <typeparamref name="T"/>.</param>
		/// <returns>A function to convert an object taken from a MySqlDataReader into the desired type.</returns>
		/// <remarks>
		/// The reason this takes two different type arguments is that type conversion in C# is really dumb sometimes.
		/// </remarks>
		private static Func<object, TTarget> GetDataReaderFieldConversionFunction<TTarget>(Type targetType)
		{
			if (targetType != typeof(TTarget))
			{
				throw new ArgumentException($"The type specified by parameter {nameof(targetType)} must be the same as the type specified by type parameter {nameof(TTarget)}.");
			}

			Func<object, TTarget> result;

			if (targetType == typeof(Int24))
			{
				result = valueToConvert => (TTarget)(object)(Int24)(int)Convert.ChangeType(valueToConvert, typeof(int));
			}
			else if (targetType == typeof(UInt24))
			{
				result = valueToConvert => (TTarget)(object)(UInt24)(uint)Convert.ChangeType(valueToConvert, typeof(uint));
			}
			else
			{
				result = valueToConvert => (TTarget)Convert.ChangeType(valueToConvert, typeof(TTarget));
			}

			return result;
		}

		private static string BuildMySqlColumnNameFromTemplate(string templateString, int instanceNumber)
		{
			return Regex.Replace(templateString, _TEMPLATED_NUMBER_PATTERN, instanceNumber.ToString());
		}
	}
}
