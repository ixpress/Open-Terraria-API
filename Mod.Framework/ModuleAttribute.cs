﻿using System;

namespace Mod.Framework
{
	public class ModuleAttribute : Attribute
	{
		public string Name { get; set; }

		public string Author { get; set; }

		public int Order { get; set; }

		public ModuleAttribute(string name = null, string author = null, int order = 10)
		{
			this.Name = name;
			this.Author = author;
			this.Order = order;
		}
	}
}
