using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadListener.GameCollections
{
	interface IGameCollection
	{
		List<LibraryItem> Games { get; set; }

		void FetchGameList();
	}
}
