using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TriCare;
using TriCare.iOS;
using TriCare.Utilities;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(TriCare.Droid.FileSystem))]
namespace TriCare.Droid
{
    public class FileSystem: IFileSystem
    {
        public FileSystem() {
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var library = Path.Combine(documents, "..", "Library");
			this.AppData = new Directory(library);
			this.Cache = new Directory(Path.Combine (library, "Caches"));
			this.Temp = new Directory(Path.Combine(documents, "..", "tmp"));
			this.Public = new Directory(documents);
        }

        public IDirectory AppData { get; private set; }
        public IDirectory Cache { get; private set; }
        public IDirectory Public { get; private set; }
        public IDirectory Temp { get; private set; }


        public IDirectory GetDirectory(string path) {
            return new Directory(new DirectoryInfo(path));
        }


        public IFile GetFile(string fileName) {
            return new File(new FileInfo(fileName));
        }
    }
}