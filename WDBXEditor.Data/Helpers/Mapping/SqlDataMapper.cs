using MySql.Data.MySqlClient;
using System;
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

		//private static readonly Dictionary<Type, ConversionFunc<>> SqlTypeConversionFunctionMap = new Dictionary<Type, System.Func>();
		private readonly Dictionary<string, Action<MySqlDataReader, T>> MapFunctions = new Dictionary<string, Action<MySqlDataReader, T>>();

		public SqlDataMapper()
		{
			PopulateMapFunctionsDict();
		}

		private delegate TOut ConversionFunc<TOut>(object readerOut);

		public T MapSqlDataRow(MySqlDataReader reader)
		{
			T result = new T();
			foreach (var propertyInfo in typeof(T).GetProperties())
			{
				// TODO: Add the case here for CompoundFields.
				if (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameAttribute)))
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
				if (propertyInfo.HasCustomAttribute(typeof(CompoundFieldAttribute)))
				{

				}
				else if (propertyInfo.HasCustomAttribute(typeof(MySqlColumnNameAttribute)))
				{
					string mySqlColumnName = propertyInfo.GetCustomAttributePropertyValue<MySqlColumnNameAttribute, string>("Name");
					Type targetPropertyType = propertyInfo.PropertyType;

					// This gets us a reference to GetDataReaderFieldConversionFunction where the TTarget typeparam has been set as targetPropertyType.
					// Because typing in C# is an actual nightmare sometimes.
					MethodInfo getDataReaderConversionFunction = GetType().GetMethod(nameof(GetDataReaderFieldConversionFunction), BindingFlags.NonPublic | BindingFlags.Static)
						.MakeGenericMethod(new Type[] { targetPropertyType });

					Action<MySqlDataReader, T> mapFunction = (reader, modelInstance) =>
					{
						dynamic conversionFunction = getDataReaderConversionFunction.Invoke(this, new object[] { reader.GetDataTypeName(mySqlColumnName), targetPropertyType });
						//GetDataReaderFieldConversionFunction<T>()
						propertyInfo.SetValue(modelInstance, conversionFunction(reader[mySqlColumnName]));
					};

					MapFunctions.Add(propertyInfo.Name, mapFunction);
				}
			}
		}

		private static Func<object, TTarget> GetDataReaderFieldConversionFunction<TTarget>(string mySqlDataTypeName, Type targetType)
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
			else if(targetType == typeof(UInt24))
			{
				result = valueToConvert => (TTarget)(object)(UInt24)(uint)Convert.ChangeType(valueToConvert, typeof(uint));
			}
			else
			{
				result = valueToConvert => (TTarget)Convert.ChangeType(valueToConvert, typeof(TTarget));
			}
			//switch (mySqlDataTypeName)
			//{
			//	case _MYSQL_DATA_TYPE_NAME_MEDIUM_INT:
			//		if (typeof(TTarget) != typeof(Int24))
			//		{
			//			throw new ImproperMySqlDataTypeConversionException();
			//		}
			//		result = valueToConvert => (Int24)Convert.ChangeType(valueToConvert, typeof(int));
			//		break;
			//	case _MYSQL_DATA_TYPE_NAME_MEDIUM_INT_UNSIGNED:
			//		if (typeof(TTarget) != typeof(UInt24))
			//		{
			//			throw new ImproperMySqlDataTypeConversionException();
			//		}
			//		result = valueToConvert => (UInt24)Convert.ChangeType(valueToConvert, typeof(uint));
			//		break;
			//	default:
			//		result = valueToConvert =>
			//		{
			//			return (TTarget)Convert.ChangeType(valueToConvert, typeof(TTarget));
			//		};
			//		break;
			//}

			return result;
		}

		//public static Func<TTarget, TDataReaderField, object> GetDataReaderFieldConversionFunction<TTarget, TDataReaderField>() where TTarget : IConvertible
		//{
		//	Func<TTarget, TDataReaderField, object> result;
		//	if (typeof(TTarget) == typeof(UInt24))
		//	{
		//		if (typeof(TDataReaderField) != typeof(uint))
		//		{
		//			throw new ImproperMySqlDataTypeConversionException();
		//		}

		//		result = (int24Type, valueToConvert) => (UInt24)Convert.ChangeType(valueToConvert, typeof(uint));
		//	}
		//	else if (typeof(TTarget) == typeof(Int24))
		//	{
		//		if (typeof(TDataReaderField) != typeof(uint))
		//		{
		//			throw new ImproperMySqlDataTypeConversionException();
		//		}

		//		result = (int24Type, valueToConvert) => (Int24)Convert.ChangeType(valueToConvert, typeof(int));
		//	}
		//	else
		//	{
		//		result = ()
		//	}
		//}
	}
}
