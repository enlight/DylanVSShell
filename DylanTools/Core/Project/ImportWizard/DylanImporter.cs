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

using System.IO;
using System.Windows;

namespace DylanTools.Core.Project.ImportWizard
{
    /// <summary>
    /// Imports one or more existing Dylan projects into .dylproj files.
    /// </summary>
    internal class DylanImporter : DependencyObject
    {
        public string SourcePath
        {
            get { return (string)GetValue(SourcePathProperty); }
            set { SetValue(SourcePathProperty, value); }
        }

        public static readonly DependencyProperty SourcePathProperty =
            DependencyProperty.Register(
                "SourcePath", typeof(string), typeof(DylanImporter),
                new PropertyMetadata(SourcePath_Updated)
            );

        private static void SourcePath_Updated(DependencyObject d,
                                               DependencyPropertyChangedEventArgs e)
        {
            // if the calling thread doesn't have access to the dependency object redirect the
            // call to the thread that does
            if (!d.Dispatcher.CheckAccess())
            {
                d.Dispatcher.InvokeAsync(() => SourcePath_Updated(d, e));
                return;
            }

            var importer = d as DylanImporter;
            if (importer == null)
            {
                return;
            }

            var sourcePath = importer.SourcePath;
            if (IsValidPath(sourcePath) && Directory.Exists(sourcePath))
            {
                // locate all source files of interest
                // ...   
            }
        }

        private static bool IsValidPath(string path)
        {
            return !string.IsNullOrEmpty(path) &&
                   (path.IndexOfAny(Path.GetInvalidPathChars()) == -1);
        }
    }
}
