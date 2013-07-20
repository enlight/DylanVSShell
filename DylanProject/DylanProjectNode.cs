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
using Microsoft.VisualStudio.Project;

namespace DylanVSShell.DylanProject
{
    /// <summary>
    /// This class represents Dylan projects in the Solution Explorer.
    /// </summary>
    public class DylanProjectNode : ProjectNode
    {
        private DylanProjectPackage _package;

        public DylanProjectNode(DylanProjectPackage package)
        {
            _package = package;
        }

        public override Guid ProjectGuid
        {
            get { return GuidList.GuidDylanProjectFactory; }
        }

        public override string ProjectType
        {
            get { return "DylanProjectType"; }
        }

        public override void AddFileFromTemplate(string source, string target)
        {
            this.FileTemplateProcessor.UntokenFile(source, target);
            this.FileTemplateProcessor.Reset();
        }

        protected override Guid[] GetConfigurationIndependentPropertyPages()
        {
            return new[] { typeof(DylanGeneralPropertyPage).GUID };
        }
    }
}
