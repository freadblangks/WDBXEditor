using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Acmil.PowerShell.Common.Alcs
{
	public class AcmilModuleAssemblyResolutionEventHandler : IModuleAssemblyInitializer, IModuleAssemblyCleanup
	{
		private const string _ENGINE_ASSEMBLY_NAME = "Acmil.PowerShell.Engines";

		private static readonly string _DEPENDENCY_DIR_PATH = GetDependencyDirectoryPath();
		private static readonly AcmilAssemblyLoadContext _DEPENDENCY_ALC = new AcmilAssemblyLoadContext(_DEPENDENCY_DIR_PATH);

		public void OnImport()
		{
			AssemblyLoadContext.Default.Resolving += ResolveAlcEngine;
		}

		public void OnRemove(PSModuleInfo psModuleInfo)
		{
			AssemblyLoadContext.Default.Resolving -= ResolveAlcEngine;
		}

		private static Assembly ResolveAlcEngine(AssemblyLoadContext defaultAlc, AssemblyName assemblyToResolve)
		{
			Assembly assembly = null;
			if (assemblyToResolve.Name.Equals(_ENGINE_ASSEMBLY_NAME))
			{
				// Load our Engines assembly here.
				// This is to minimize dependency conflicts with PowerShell and any
				// random assemblies that might already be loaded in the session.
				assembly = _DEPENDENCY_ALC.LoadFromAssemblyName(assemblyToResolve);
			}

			return assembly;
		}

		private static string GetDependencyDirectoryPath()
		{
			// NOTE: We might need to change where this directory is or when it's written to
			// in the build process. If it doesn't seem to get written to correctly,
			// we might need to adjust the project build order or move the directory.

			//string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;
			//string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

			//return Path.GetFullPath(
			//	Path.Combine(

			//);

			//return Assembly.GetExecutingAssembly().GetName().CodeBase.Replace("file:\\", "");
			//AppContext.BaseDirectory
			return Path.GetFullPath(
				Path.Combine(
					Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
					"IsolatedDependencies"
				)
			);
		}
	}
}
