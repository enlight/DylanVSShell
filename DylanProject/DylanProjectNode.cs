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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DylanVSShell.DylanProject
{
    /// <summary>
    /// This class represents Dylan projects in the Solution Explorer.
    /// </summary>
    [Guid(DylanConstants.ProjectNodeGuidString)]
    public class DylanProjectNode : ProjectNode
    {
        private DylanProjectPackage _package;
        private VSLangProj.VSProject _vsProject;

        public DylanProjectNode(DylanProjectPackage package)
        {
            Contract.Assert(package != null);

            _package = package;

            OleServiceProvider.AddService(
                typeof(VSLangProj.VSProject),
                new OleServiceProvider.ServiceCreatorCallback(this.CreateServices), false);

            this.AddCATIDMapping(typeof(OAProject), typeof(OAProject).GUID);
            this.AddCATIDMapping(
                typeof(DylanGeneralPropertyPage),
                typeof(DylanGeneralPropertyPage).GUID);
            this.AddCATIDMapping(
                typeof(DylanProjectNodeProperties),
                typeof(DylanProjectNodeProperties).GUID);
        }

        public override Guid ProjectGuid
        {
            get { return GuidList.GuidDylanProjectFactory; }
        }

        public override string ProjectType
        {
            get { return "DylanProjectType"; }
        }

        protected internal VSLangProj.VSProject VSProject
        {
            get { return _vsProject ?? (_vsProject = new OAVSProject(this)); }
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

        protected override NodeProperties CreatePropertiesObject()
        {
            return new DylanProjectNodeProperties(this);
        }

        public override FileNode CreateFileNode(ProjectElement item)
        {
            var node = new DylanFileNode(this, item);
            node.OleServiceProvider.AddService(
                typeof(EnvDTE.Project),
                new OleServiceProvider.ServiceCreatorCallback(this.CreateServices), false);
            node.OleServiceProvider.AddService(
                typeof(EnvDTE.ProjectItem), node.ServiceCreator, false);
            node.OleServiceProvider.AddService(
                typeof(VSLangProj.VSProject),
                new OleServiceProvider.ServiceCreatorCallback(this.CreateServices), false);
            return node;
        }

        private object CreateServices(Type serviceType)
        {
            object service = null;
            if (typeof(VSLangProj.VSProject) == serviceType)
            {
                service = this.VSProject;
            }
            else if (typeof(EnvDTE.Project) == serviceType)
            {
                service = this.GetAutomationObject();
            }
            return service;
        }
    }
}
