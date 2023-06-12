using System.Reflection;
using System.Runtime.Loader;

namespace Acmil.PowerShell.Common.Alcs
{
	internal class AcmilAssemblyLoadContext : AssemblyLoadContext
	{
		private readonly string _dependencyDirectoryPath;

		public AcmilAssemblyLoadContext(string dependencyDirectoryPath)
		{
			_dependencyDirectoryPath = dependencyDirectoryPath;
		}

		protected override Assembly Load(AssemblyName assemblyName)
		{
			string assemblyPath = Path.Combine(_dependencyDirectoryPath, $"{assemblyName.Name}.dll");

			Assembly assembly = null;
			if (File.Exists(assemblyPath))
			{
				assembly = LoadFromAssemblyPath(assemblyPath);
			}

			return assembly;
		}
	}
}
