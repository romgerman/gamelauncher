using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace GamepadListener
{
	class UplayCollection
	{
		public List<string> games = new List<string>();

		public void FetchGameList()
		{
			using (var originKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Ubisoft"))
			{
				if (originKey == null) // Origin is not installed
					return;

				string[] gamesKeys = originKey.GetSubKeyNames();

				foreach (var subKey in gamesKeys)
				{
					games.Add(subKey);

					// "Launcher" is not a game (it's a uplay launcher)
					
					/*using (var gameKey = originKey.OpenSubKey(subKey))
					{
						string[] vn = gameKey.GetValueNames();

						
							//we can get install path here
						 

						games.Add((string)gameKey.GetValue("Install path"));
					}*/
				}
			}
		}
	}

}
