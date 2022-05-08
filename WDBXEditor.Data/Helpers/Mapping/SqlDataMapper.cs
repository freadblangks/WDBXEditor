using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using WDBXEditor.Common.Utility.Extensions;
using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Attributes;
using WDBXEditor.Data.Exceptions;

namespace WDBXEditor.Data.Helpers.Mapping
{
	public class SqlDataMapper<T> where T : new()
	{
		private const string _MYSQL_DATA_TYPE_NAME_MEDIUM_INT = "mediumint";
		private const string _MYSQL_DATA_TYPE_NAME_MEDIUM_INT_UNSIGNED = "mediumint unsigned";

		private readonly Dictionary<string, Action<MySqlDataReader, T>> MapFunctions = new Dictionary<string, Action<MySqlDataReader, T>>();

		/// <summary>
		/// Initializes a new instance of <see cref="SqlDataMapper{T}"/>.
		/// </summary>
		public SqlDataMapper()
		{
			PopulateMapFunctionsDict();
		}

		public T MapSqlDataRow(MySqlDataReader reader)
		{
			T result = new T();
			foreach (var propertyInfo in typeof(T).GetProperties())
			{
				// TODO: Add the case here for array-type properties.
				if (!propertyInfo.PropertyType.IsArray && (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameAttribute)) || propertyInfo.HasCustomAttribute(typeof(CompoundFieldAttribute))))
				{
					MapFunctions[propertyInfo.Name](reader, result);
				}
			}

			return result;
		}

		private void PopulateMapFunctionsDict()
		{
			foreach (var propertyInfo in typeof(T).GetProperties())
			{
				if (!propertyInfo.PropertyType.IsArray && (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameAttribute)) || propertyInfo.HasCustomAttribute(typeof(CompoundFieldAttribute))))
				{
					Action<MySqlDataReader, T> mapFunction = BuildMapFunction(propertyInfo);
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
		private Action<MySqlDataReader, T> BuildMapFunction(PropertyInfo propertyInfo)
		{
			Action<MySqlDataReader, T> mapFunction = null;

			// If the property is marked as a [CompoundField], it's a reference type whose properties need to be populated.
			// So, we'll create an inner SqlDataMapper<T> that we'll use to define the functions to set its properties.
			if (propertyInfo.HasCustomAttribute(typeof(CompoundFieldAttribute)))
			{
				// Create a new instance of SqlDataMapper<T>, where T is the type of the property marked as [CompoundField].
				dynamic innerDataMapper = Activator.CreateInstance(GetType().GetGenericTypeDefinition().MakeGenericType(new Type[] { propertyInfo.PropertyType }));
				mapFunction = (reader, submodelInstance) =>
				{
					propertyInfo.SetValue(submodelInstance, innerDataMapper.MapSqlDataRow(reader));
				};

			}

			// If the property has the [MySqlColumnName()] attribute, we know it's a property containing a value type
			// that maps to only one column in table.
			else if (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameAttribute)))
			{
				string mySqlColumnName = propertyInfo.GetCustomAttributePropertyValue<MySqlColumnNameAttribute, string>("Name");
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
			}

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
	}
}
