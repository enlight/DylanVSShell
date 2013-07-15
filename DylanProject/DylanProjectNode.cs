﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Project;

namespace DylanVSShell.DylanProject
{
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
	}
}