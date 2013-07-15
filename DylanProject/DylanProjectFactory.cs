using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Project;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace DylanVSShell.DylanProject
{
	[Guid(GuidList.GuidDylanProjectFactoryString)]
	class DylanProjectFactory : ProjectFactory
	{
		private DylanProjectPackage _package;

		public DylanProjectFactory(DylanProjectPackage package)
			: base(package)
		{
			_package = package;
		}

		protected override ProjectNode CreateProject()
		{
			var project = new DylanProjectNode(_package);
			project.SetSite(
				(IOleServiceProvider)((IServiceProvider)_package)
				.GetService(typeof(IOleServiceProvider))
			);
			return project;
		}
	}
}
