// Guids.cs
// MUST match guids.h
using System;

namespace DylanTools.Core
{
    /// <summary>
    /// This class is used only to expose the list of Guids used by this package.
    /// This list of guids must match the set of Guids used inside the VSCT file.
    /// </summary>
    static class GuidList
    {
        public const string GuidDylanToolsCorePkgString = "2444c21d-c0fc-4c79-b300-c544658fefc6";
        public const string GuidDylanToolsCoreCmdSetString = "131d104d-51e2-4b78-a60d-1985973eda31";
	    public const string GuidDylanProjectFactoryString = "A433481A-4553-4DAA-BE69-2F2AB94C12BE";

        public static readonly Guid GuidDylanToolsCoreCmdSet = 
			new Guid(GuidDylanToolsCoreCmdSetString);
		public static readonly Guid GuidDylanProjectFactory = 
			new Guid(GuidDylanProjectFactoryString);
    };
}