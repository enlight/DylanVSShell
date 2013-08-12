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

using System.Windows.Forms;
using DylanVSShell.DylanProject;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;

namespace DylanVSShell.DylanImportWizard
{
    /// <summary>
    /// This wizard is used in conjuction with the "From Existing Code" project template to create
    /// a new project from an existing Dylan code base.
    /// 
    /// This class is just a stub that invokes a command to do all the actual work.
    /// </summary>
    public sealed class Wizard : IWizard
    {
        // called before opening any item that has the OpenInEditor attribute
        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem) {}

        public void ProjectFinishedGenerating(EnvDTE.Project project) {}

        // only called for item templates, not project templates
        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem) {}

        // called after a project is created
        public void RunFinished() {}

        public void RunStarted(object automationObject,
                               Dictionary<string, string> replacementsDictionary,
                               WizardRunKind runKind, object[] customParams)
        {
            // invoke the corresponding command (implemented in DylanProject) to do the actual work
            var dte = automationObject as DTE;
            if (dte == null)
            {
                var provider =
                    automationObject as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
                if (provider != null)
                {
                    dte = new ServiceProvider(provider).GetService(typeof(DTE)) as DTE;
                }
            }
            if (dte == null)
            {
                MessageBox.Show(
                    "Unable to start wizard: no automation object available.",
                    "Dylan Tools for Visual Studio"
                );
            }
            else
            {
                System.Threading.Tasks.Task.Run(
                    () => {
                        object inObj = null;
                        object outObj = null;
                        dte.Commands.Raise(
                            GuidList.guidDylanProjectCmdSet.ToString("B"),
                            (int)PkgCmdIDList.CmdIdImportWizard, ref inObj, ref outObj
                        );
                    }
                );
            }
            throw new WizardCancelledException();
        }

        // only called for item templates, not project templates
        public bool ShouldAddProjectItem(string filePath)
        {
            return false;
        }
    }
}
