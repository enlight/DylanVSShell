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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DylanTools.Core.Project.ImportWizard;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace DylanTools.Core.Commands
{
    /// <summary>
    /// This command displays a wizard that creates one or more Dylan projects from an existing 
    /// Dylan source base.
    /// </summary>
    class ImportWizardCommand : Command
    {
        public override void Execute(object sender, EventArgs args)
        {
            var statusBar = (IVsStatusbar)Package.GetGlobalService(typeof(SVsStatusbar));
            statusBar.SetText("Importing from existing source...");

            var dlg = new ImportWizardDialog();
            if (dlg.ShowModal() ?? false)
            {
                
            }
            else
            {
                statusBar.SetText(String.Empty);
            }
        }

        public override uint CommandId
        {
            get { return PkgCmdIDList.CmdIdImportWizard; }
        }
    }
}
