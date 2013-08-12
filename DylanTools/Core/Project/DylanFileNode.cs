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
using EnvDTE;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using VSLangProj;

namespace DylanTools.Core.Project
{
    internal class DylanFileNode : FileNode
    {
        private DylanProjectNode _project;
        private OAVSProjectItem _vsProjectItem;

        public DylanFileNode(DylanProjectNode root, ProjectElement element) : base(root, element)
        {
            _project = root;
        }

        protected internal VSProjectItem VSProjectItem
        {
            get { return _vsProjectItem ?? (_vsProjectItem = new OAVSProjectItem(this)); }
        }

        internal override object Object
        {
            get { return _vsProjectItem; }
        }

        internal OleServiceProvider.ServiceCreatorCallback ServiceCreator
        {
            get { return new OleServiceProvider.ServiceCreatorCallback(this.CreateServices); }
        }

        protected override NodeProperties CreatePropertiesObject()
        {
            return new FileNodeProperties(this);
        }

        private object CreateServices(Type serviceType)
        {
            object service = null;
            if (typeof(ProjectItem) == serviceType)
            {
                service = this.GetAutomationObject();
            }
            return service;
        }
    }
}
