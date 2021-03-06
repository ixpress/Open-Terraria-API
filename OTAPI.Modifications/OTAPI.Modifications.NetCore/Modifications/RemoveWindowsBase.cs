﻿using System.Linq;
using OTAPI.Patcher.Engine.Modification;

namespace OTAPI.Patcher.Engine.Modifications.Patches
{
	/// <summary>
	/// Replaces all WindowsBase references to use OTAPI.Modifications.NetCore.dll
	/// </summary>
	public class RemoveWindowsBase : ModificationBase
	{
		public override System.Collections.Generic.IEnumerable<string> AssemblyTargets => new[]
		{
			"TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null"
		};
		public override string Description => "Removing WindowsBase references...";

		public override void Run()
		{
			foreach (var reference in SourceDefinition.MainModule.AssemblyReferences
				.Where(x => x.Name.StartsWith("WindowsBase")))
			{
				reference.Name = "OTAPI.Modifications.NetCore";
				reference.PublicKey = ModificationDefinition.Name.PublicKey;
				reference.PublicKeyToken = ModificationDefinition.Name.PublicKeyToken;
				reference.Version = ModificationDefinition.Name.Version;
			}
		}
	}
}