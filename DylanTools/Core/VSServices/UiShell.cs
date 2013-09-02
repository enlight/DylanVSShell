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
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DylanTools.Core.VSServices
{
    /// <summary>
    /// Provides access to basic windowing functionality within Visual Studio.
    /// 
    /// This class wraps the IVsUIShell interface in order to hide most of the interop mess.
    /// </summary>
    internal class UiShell
    {
        private IVsUIShell _uiShell;

        public UiShell(IVsUIShell uiShell)
        {
            _uiShell = uiShell;
        }

        /// <summary>
        /// Returns a handle to a window that can be used to parent modal dialogs.
        /// 
        /// The handle returned will either be to the Visual Studio main application window, 
        /// or to the top-most modal dialog (if one is displayed).
        /// </summary>
        /// <returns>A window handle (HWND).</returns>
        public IntPtr GetDialogOwnerHandle()
        {
            IntPtr handle;
            ErrorHandler.ThrowOnFailure(_uiShell.GetDialogOwnerHwnd(out handle));
            return handle;
        }

        /// <summary>
        /// Displays the Browse dialog that allows the user to select a directory.
        /// </summary>
        /// <param name="parent">The parent window of the Browse dialog, can be null.</param>
        /// <param name="title">The title for the Browse dialog.</param>
        /// <param name="initialDirectory">The directory that should be selected when the Browse
        /// dialog is initially displayed.</param>
        /// <returns>The directory selected by the user, or null if none was selected.</returns>
        public string GetDirectoryViaBrowseDlg(Window parent, string title = null,
                                               string initialDirectory = null)
        {
            var owner = IntPtr.Zero;
            if (parent != null)
            {
                owner = new WindowInteropHelper(parent).Handle;
            }
            if (owner == IntPtr.Zero)
            {
                owner = GetDialogOwnerHandle();
            }

            var browseInfo = new VSBROWSEINFOW[1];
            browseInfo[0].lStructSize = (uint)Marshal.SizeOf(typeof(VSBROWSEINFOW));
            browseInfo[0].pwzInitialDir = initialDirectory;
            browseInfo[0].pwzDlgTitle = title;
            browseInfo[0].hwndOwner = owner;
            browseInfo[0].nMaxDirName = 260;
            var dirName = Marshal.AllocCoTaskMem(520);
            browseInfo[0].pwzDirName = dirName;

            try
            {
                var retval = _uiShell.GetDirectoryViaBrowseDlg(browseInfo);
                if (retval == VSConstants.OLE_E_PROMPTSAVECANCELLED)
                {
                    return null;
                }
                ErrorHandler.ThrowOnFailure(retval);
                return Marshal.PtrToStringAuto(browseInfo[0].pwzDirName);
            }
            finally
            {
                if (dirName != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(dirName);
                }
            }
        }
    }
}
