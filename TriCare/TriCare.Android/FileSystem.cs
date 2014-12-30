using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TriCare;
using TriCare.Droid;
using TriCare.Utilities;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(TriCare.Droid.FileSystem))]
namespace TriCare.Droid
{
    public class FileSystem: IFileSystem
    {
        public FileSystem() {
			this.AppData = new Directory(Android.App.Application.Context.FilesDir.AbsolutePath);
            this.Cache = new Directory(Android.App.Application.Context.CacheDir.AbsolutePath);
            this.Temp = new Directory(Android.App.Application.Context.CacheDir.AbsolutePath);
            this.Public = new Directory(Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath);
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