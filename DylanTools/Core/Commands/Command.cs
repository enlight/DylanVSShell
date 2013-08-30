using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DylanTools.Core.Commands
{
    /// <summary>
    /// All commands provided by the package should be subclassed from this class.
    /// 
    /// To implement a new command subclass:
    /// - Add a corresponding IDSymbol element to Core.vsct with the id of the new command.
    /// - Add the command id to the PkgCmdIDList class.
    /// - Subclass Command.
    /// - Update DylanToolsCorePackage::Initialize() to register the new command.
    /// </summary>
    abstract class Command
    {
        /// <summary>
        /// Performs an action when the command is executed.
        /// </summary>
        /// <param name="sender">The MenuComand or OleMenuCommand instance that invoked the 
        /// command.</param>
        /// <param name="args"></param>
        public abstract void Execute(object sender, EventArgs args);

        /// <summary>
        /// Returns the id of this command.
        /// 
        /// The id should match the one in the Core.vsct and PkgCmdID.cs.
        /// </summary>
        public abstract uint CommandId
        {
            get;
        }
    }
}
