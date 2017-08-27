using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace GamepadListener.GameCollections
{
	class OriginCollection : IGameCollection
	{
		public List<LibraryItem> Games { get; set; } = new List<LibraryItem>();

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

						Games.Add(new LibraryItem()
						{
							Name = (string)gameKey.GetValue("DisplayName"),
							Path = (string)gameKey.GetValue("Install Dir") // TODO: And here we need to get exe path somehow too
						});
					}
				}
			}
		}
	}

}
