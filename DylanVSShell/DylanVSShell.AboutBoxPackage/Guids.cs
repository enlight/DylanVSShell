// Guids.cs
// MUST match guids.h
using System;

namespace DylanVSShell.AboutBoxPackage
{
	static class GuidList
	{
		public const string guidAboutBoxPackagePkgString = "d4a8d047-057f-408f-a711-db84d70b2cb5";
		public const string guidAboutBoxPackageCmdSetString = "f314a7b1-23a2-4858-aa81-a01c3e92cad5";

		public static readonly Guid guidAboutBoxPackageCmdSet = new Guid(guidAboutBoxPackageCmdSetString);
	};
}