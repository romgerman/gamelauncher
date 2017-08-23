using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GamepadListener
{
    [Serializable]
    public class LibraryItemApplication : LibraryItem
    {
        public uint PlayCount { get; set; }
        public string LastPlayed { get; set; }
    }

    [Serializable]
    public class LibraryItemFolder : LibraryItem
    {
        // TODO: Unused for now
    }

    [XmlInclude(typeof(LibraryItemApplication))]
    [XmlInclude(typeof(LibraryItemFolder))]
    [Serializable]
    public class LibraryItem
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Thumbnail { get; set; }
    }

    public struct LibraryData
    {
        public List<LibraryItem> Items { get; set; }

        public static LibraryData LoadFromFile(string filename)
        {
            if(File.Exists(filename))
            {
                using (var stream = new StreamReader(filename))
                {
                    var serializer = new XmlSerializer(typeof(LibraryData));
                    return (LibraryData)serializer.Deserialize(stream);
                }
            }
            else
            {
                var libraryData = new LibraryData();
                libraryData.Items = new List<LibraryItem>();
                libraryData.SaveToFile(filename);
                return libraryData;
            }
        }

        public void SaveToFile(string filename)
        {
            using (var stream = new StreamWriter(File.Exists(filename) ? File.Open(filename, FileMode.Open) : File.Create(filename)))
            {
                var serializer = new XmlSerializer(typeof(LibraryData));
                serializer.Serialize(stream, this);
                stream.Flush(); // Just to be safe
            }
        }
    }

    class Library
    {
        LibraryData Data { get; set; }
    }
}
