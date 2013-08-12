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
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Project;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace DylanTools.Core.Project
{
    [Guid(GuidList.GuidDylanProjectFactoryString)]
    internal class DylanProjectFactory : ProjectFactory
    {
        private DylanToolsCorePackage _package;

        public DylanProjectFactory(DylanToolsCorePackage package)
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
