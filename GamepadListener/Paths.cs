﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadListener
{
	class Paths
	{
		public static string LibraryPath
		{
			get
			{
				return $"{AppDomain.CurrentDomain.BaseDirectory}library.xml";
			}
		}
	}
}
