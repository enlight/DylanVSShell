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

using DylanTools.Core.VSServices;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DylanTools.Core.Presentation
{
    internal static class Commands
    {
        public static readonly RoutedCommand BrowseFolder = new RoutedCommand();

        public static void CanExecute(Window window, object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == BrowseFolder)
            {
                e.CanExecute = e.OriginalSource is TextBox;
            }
        }

        public static void Executed(Window window, object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == BrowseFolder)
            {
                BrowseFolderExecute(window, e);
            }
        }

        private static void BrowseFolderExecute(Window window, ExecutedRoutedEventArgs e)
        {
            var textBox = (TextBox)e.OriginalSource;
            var path = e.Parameter as string ?? textBox.GetValue(TextBox.TextProperty) as string;
            // obtain a path to an existing directory
            while (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            var uiShell = new UiShell(DylanToolsCorePackage.Instance.VsUiShell);
            path = uiShell.GetDirectoryViaBrowseDlg(window, null, path);
            if (path != null)
            {
                textBox.SetCurrentValue(TextBox.TextProperty, path);
                var binding =
                    BindingOperations.GetBindingExpressionBase(textBox, TextBox.TextProperty);
                if (binding != null)
                {
                    binding.UpdateSource();
                }
            }
        }
    }
}
