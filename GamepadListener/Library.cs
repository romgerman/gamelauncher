using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadListener
{
    class LibraryItemApplication : LibraryItem
    {
        public uint PlayCount { get; set; }
        public string LastPlayed { get; set; }
    }

    class LibraryItemFolder : LibraryItem
    {
        // TODO: Unused for now
    }

    class LibraryItem
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Thumbnail { get; set; }
    }

    class LibraryData
    {
        public List<LibraryItem> Items { get; set; }
    }

    class Library
    {
        LibraryData Data { get; set; }
    }
}
