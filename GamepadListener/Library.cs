using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadListener
{
    class LibraryItemApplication : LibraryItem
    {
        string PlayCount { get; set; }
        string LastPlayed { get; set; }
    }

    class LibraryItemFolder : LibraryItem
    {

    }

    class LibraryItem
    {
        string Path { get; set; }
        string Name { get; set; }
        string Desc { get; set; }
        string Thumbnail { get; set; }
    }

    class LibraryData
    {

    }

    class Library
    {
    }
}
