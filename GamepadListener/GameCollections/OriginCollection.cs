using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace GamepadListener
{
	class OriginCollection
	{
		public List<string> games = new List<string>();

		public void FetchGameList()
		{
			using (var originKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\EA Games"))
			{
				if (originKey == null) // Origin is not installed
					return;

				string[] gamesKeys = originKey.GetSubKeyNames();

				foreach(var subKey in gamesKeys)
				{
					using (var gameKey = originKey.OpenSubKey(subKey))
					{
						string[] vn = gameKey.GetValueNames();

						/*
							GDFBinary
							GameExplorer
							GDFBinary64
							"GameExplorer64"
							"DisplayName"
							"Locale"
							"Product GUID"
							"Install Dir"
						 */

						games.Add((string)gameKey.GetValue("DisplayName"));
					}
				}
			}
		}
	}

}
