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

			if (!File.Exists(libFile)) // there is no "libraryfolders" file
				return;

			string[] libraryFolders = File.ReadAllLines(libFile);
			string libraryPath = null;

			foreach(string line in libraryFolders) // SUPER DIRTY PARSING
			{
				string str = line.TrimStart();

				if (str.Length > 0)
					str = str.Substring(1);
				else
					continue;

				if (str.StartsWith("0") || str.StartsWith("1") || str.StartsWith("3"))
				{
					libraryPath = str.Substring(2);
					libraryPath = libraryPath.TrimStart();
					libraryPath = libraryPath.TrimStart('\t');
					libraryPath = libraryPath.Substring(1, libraryPath.Length - 2);
				}
			}

			if (libraryPath == null) // No library path was found. (Is that even possible?)
				return;

			string[] manifests = Directory.GetFiles(Path.Combine(libraryPath, "steamapps"));

			if (manifests.Length == 0) // No games is installed
				return;

			foreach (var man in manifests) // EXTRA DIRTY
			{
				string name = File.ReadAllText(man);
				int index = name.IndexOf("name");
				name = name.Substring(index + 5).Trim('\t').Substring(1);
				name = name.Substring(0, name.IndexOf('"'));
				games.Add(name);

				// We can also check for registry record if the game is installed or running
				// But manifests exist only if game is installed so..
			}
		}
	}
}
