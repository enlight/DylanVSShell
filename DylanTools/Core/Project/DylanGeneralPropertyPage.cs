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

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DylanTools.Core.Project
{
    /// <summary>
    /// This class implements the General project property page for a Dylan project.
    /// 
    /// This is one of the pages that appear when a user right-clicks on a Dylan project in
    /// the Solution Explorer and selects Properties from the context-menu.
    /// </summary>
    [ComVisible(true)]
    [Guid(DylanConstants.GeneralPropertyPageGuidString)]
    internal class DylanGeneralPropertyPage : SettingsPage
    {
        private string _synopsis;
        private string _keywords;
        private string _author;
        private string _description;
        private string _comment;

        public DylanGeneralPropertyPage()
        {
            this.Name = "General";
        }

        [DisplayName(DylanConstants.ProjectSynopsis)]
        [Description("A concise description of the project.")]
        public string Synopsis
        {
            get { return _synopsis; }
            set
            {
                _synopsis = value;
                this.IsDirty = true;
            }
        }

        [DisplayName(DylanConstants.ProjectKeywords)]
        [Description(
            "Any number of phrases, separated by commas, that are relevant to the description or use of the project."
            )]
        public string Keywords
        {
            get { return _keywords; }
            set
            {
                _keywords = value;
                this.IsDirty = true;
            }
        }

        [DisplayName(DylanConstants.ProjectAuthor)]
        [Description("The name of the project's author.")]
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                this.IsDirty = true;
            }
        }

        [DisplayName(DylanConstants.ProjectDescription)]
        [Description("A fuller, less concise description than that given by the Synopsis.")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                this.IsDirty = true;
            }
        }

        [DisplayName(DylanConstants.ProjectComment)]
        [Description("Any additional comments about the project.")]
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                this.IsDirty = true;
            }
        }

        protected override void BindProperties()
        {
            this.Synopsis = this.ProjectMgr.GetProjectProperty(DylanConstants.ProjectSynopsis);
            this.Keywords = this.ProjectMgr.GetProjectProperty(DylanConstants.ProjectKeywords);
            this.Author = this.ProjectMgr.GetProjectProperty(DylanConstants.ProjectAuthor);
            this.Description = this.ProjectMgr.GetProjectProperty(DylanConstants.ProjectDescription);
            this.Comment = this.ProjectMgr.GetProjectProperty(DylanConstants.ProjectComment);
        }

        protected override int ApplyChanges()
        {
            this.ProjectMgr.SetProjectProperty(DylanConstants.ProjectSynopsis, _synopsis);
            this.ProjectMgr.SetProjectProperty(DylanConstants.ProjectKeywords, _keywords);
            this.ProjectMgr.SetProjectProperty(DylanConstants.ProjectAuthor, _author);
            this.ProjectMgr.SetProjectProperty(DylanConstants.ProjectDescription, _description);
            this.ProjectMgr.SetProjectProperty(DylanConstants.ProjectComment, _comment);

            this.IsDirty = false;
            return VSConstants.S_OK;
        }
    }
}
