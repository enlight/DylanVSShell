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

using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using System.ComponentModel;

namespace DylanVSShell.DylanProject
{
    /// <summary>
    /// This class exposes Dylan project properties to the Properties window in VS.
    /// 
    /// Any public properties in this class without a [Browsable(false)] attribute will be visible
    /// in VS Properties window.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Guid(DylanConstants.ProjectNodePropertiesGuidString)]
    public class DylanProjectNodeProperties : ProjectNodeProperties
    {
        public DylanProjectNodeProperties(DylanProjectNode node) : base(node) {}

        [DisplayName(DylanConstants.ProjectSynopsis)]
        [Description("A concise description of the project.")]
        public string Synopsis
        {
            get
            {
                return this.Node.ProjectMgr.GetProjectProperty(
                    DylanConstants.ProjectSynopsis, true
                );
            }
            set
            {
                this.Node.ProjectMgr.SetProjectProperty(DylanConstants.ProjectSynopsis, value);
            }
        }
    }
}
