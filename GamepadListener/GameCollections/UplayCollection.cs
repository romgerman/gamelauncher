using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace GamepadListener
{
	class UplayCollection
	{
		public List<LibraryItem> Games { get; private set; } = new List<LibraryItem>();

		public void FetchGameList()
		{
			using (var originKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Ubisoft"))
			{
				if (originKey == null) // Origin is not installed
					return;

				string[] gamesKeys = originKey.GetSubKeyNames();

				foreach (var subKey in gamesKeys)
				{
					if (!subKey.Equals("Launcher"))
					{
						string installPath = null;

						using (var gameKey = originKey.OpenSubKey(subKey))
						{
							installPath = gameKey.GetValue("Install path") as string; // It could not exist
						}

						Games.Add(new LibraryItem()
						{
							Name = subKey,
							Path = installPath // TODO: And here we also need to get exe path
						});
					}
				}
			}
		}
	}

}
