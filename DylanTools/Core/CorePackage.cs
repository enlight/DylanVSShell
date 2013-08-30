#region license

//  Copyright (C) 2013 Vadim Macagon
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows.Forms;
using DylanTools.Core.Commands;
using DylanTools.Core.Project;
using EnvDTE;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Project;
using Command = DylanTools.Core.Commands.Command;

namespace DylanTools.Core
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the 
	/// IVsPackage interface and uses the registration attributes defined in the framework to 
	/// register itself and its components with the shell.
	/// </summary>
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
	// a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute lets the shell know this package provides some menus.
    [ProvideMenuResource(1000, 1)]
    // These two attributes ensure the ImportWizardCommand is always registered.
    // This is necessary because Visual Studio lazy loads packages by default, and since the
    // ImportWizardCommand is associated with an invisible menu item VS may not bother loading this
    // package.
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.NoSolution)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionExists)]
	[ProvideProjectFactory(
		typeof(DylanProjectFactory), null,
		"Dylan Project Files (*.dylproj);*.dylproj",
		"dylproj", "dylproj",
		".\\NullPath",
		LanguageVsTemplate = DylanConstants.LanguageName)]
	[ProvideObject(typeof(DylanGeneralPropertyPage))]
	[Guid(GuidList.GuidDylanToolsCorePkgString)]
	public sealed class DylanToolsCorePackage : ProjectPackage
	{
		#region properties

		public override string ProductUserContext
		{
			get { return ""; }
		}

        /// <summary>
        /// Obtains the highest level object in the Visual Studio automation model hierarchy.
        /// </summary>
        public EnvDTE.DTE AutomationRoot
        {
            get { return (EnvDTE.DTE)GetService(typeof(EnvDTE.DTE)); }
        }

		#endregion

		/// <summary>
		/// Default constructor of the package.
		/// Inside this method you can place any initialization code that does not require 
		/// any Visual Studio service because at this point the package object is created but 
		/// not sited yet inside Visual Studio environment. The place to do all the other 
		/// initialization is the Initialize method.
		/// </summary>
		public DylanToolsCorePackage()
		{
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}",
			                              this.ToString()));
		}

		/////////////////////////////////////////////////////////////////////////////
		// Overridden Package Implementation

		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, 
		/// so this is the place where you can put all the initialization code that rely on 
		/// services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}",
			                              this.ToString()));
			base.Initialize();
			this.RegisterProjectFactory(new DylanProjectFactory(this));
            // these commands must exist in the Core.vsct file
		    this.RegisterCommands(new Command[] {
		        new ImportWizardCommand()
		    });

            var dte = AutomationRoot;
            if (dte == null)
            {
                MessageBox.Show(
                    "No automation object available.",
                    "Dylan Tools for Visual Studio"
                );
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("{0} commands found:", dte.Commands.Count);
                //foreach (EnvDTE.Command command in dte.Commands)
                //{
                //    System.Diagnostics.Debug.WriteLine("{0}, {1}, {2}, Enabled = {3}", command.Name, command.Guid, command.ID, command.IsAvailable);
                //}   
            }
		}

		#endregion

        /// <summary>
        /// Register the given commands with Visual Studio.
        /// </summary>
        /// <param name="commands">A collection of commands to be registered.</param>
        private void RegisterCommands(IEnumerable<Command> commands)
        {
            var omcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != omcs)
            {
                foreach (var command in commands)
                {
                    var cmdId = 
                        new CommandID(GuidList.GuidDylanToolsCoreCmdSet, (int)command.CommandId);
                    omcs.AddCommand(new MenuCommand(command.Execute, cmdId));
                    Debug.Assert(omcs.FindCommand(cmdId) != null);
                }
            }
        }
	}
}
