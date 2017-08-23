using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace GamepadListener
{
	class SteamCollection
	{
		public List<string> games = new List<string>();

		public void FetchGameList()
		{
			string steamInstallDir = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam\\", "SteamPath", null) as string;

			if (steamInstallDir == null) // Steam is not installed
				return;

			string libFile = Path.Combine(steamInstallDir, "steamapps\\libraryfolders.vdf");

			if (!File.Exists(libFile)) // There is no "libraryfolders" file
				return;

			string libraryFolders = File.ReadAllText(libFile);

			var parser = new ValveKeyValueParser();
			parser.Read(libraryFolders);

			string libraryPath = parser.Data.GetValue("1"); // TODO: Need to make it work with multiple folders
			
			if (libraryPath == null) // No library path was found (Is that even possible?)
				return;

			string[] manifests = Directory.GetFiles(Path.Combine(libraryPath, "steamapps"));

			if (manifests.Length == 0) // No games is installed
				return;

			foreach (var man in manifests) // EXTRA DIRTY
			{
				parser.Read(File.ReadAllText(man));

				games.Add(parser.Data.GetValue("name"));

				// We can also check for registry record if the game is installed or running
				// But manifests exist only if game is installed so..
			}
		}
	}
}
