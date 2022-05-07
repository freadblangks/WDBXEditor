using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Reflection;

namespace WDBXEditor.Data.NUnit.TestHelpers
{
	/// <summary>
	/// Helper class for creating instances of <see cref="MySqlException"/>.
	/// </summary>
	public static class MySqlExceptionBuilder
	{
		public static MySqlException CreateException(string exceptionMessage, int errorNumber, Exception innerException)
		{
			ConstructorInfo[] constructorInfos = typeof(MySqlException).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
			ConstructorInfo constructor = constructorInfos.First(info => info.GetParameters().Count() == 3 && info.GetParameters()[0].Name == "msg" && info.GetParameters()[1].Name == "errno" && info.GetParameters()[2].Name == "inner");

			return (MySqlException)constructor.Invoke(new object[] { exceptionMessage, errorNumber, innerException });
		}
	}
}
