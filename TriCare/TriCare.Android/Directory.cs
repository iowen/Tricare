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

[assembly: Dependency(typeof(TriCare.Droid.Directory))]
namespace TriCare.Droid
{
    public class Directory : IDirectory
    {
        private readonly DirectoryInfo info;


        public Directory(string path) : this(new DirectoryInfo(path)) { }
        internal Directory(DirectoryInfo info)
        {
            this.info = info;
        }

        #region IDirectory Members

        public string Name
        {
            get { return this.info.Name; }
        }


        public string FullName
        {
            get { return this.info.FullName; }
        }


        public bool Exists
        {
            get { return this.info.Exists; }
        }


        private IDirectory root;
        public IDirectory Root
        {
            get
            {
                this.root = this.root ?? new Directory(this.info.Root);
                return this.root;
            }
        }


        private IDirectory parent;
        public IDirectory Parent
        {
            get
            {
                this.parent = this.parent ?? new Directory(this.info.Parent);
                return this.parent;
            }
        }


        public DateTime CreationTime
        {
            get { return this.info.CreationTime; }
        }


        public DateTime LastAccessTime
        {
            get { return this.info.LastAccessTime; }
        }


        public DateTime LastWriteTime
        {
            get { return this.info.LastWriteTime; }
        }


        public void Create()
        {
            this.info.Create();
        }


        public void MoveTo(string path)
        {
            this.info.MoveTo(path);
        }


        public bool FileExists(string fileName)
        {
            var path = Path.Combine(this.FullName, fileName);
            return System.IO.File.Exists(path);
        }


        public IFile CreateFile(string fileName)
        {
            var path = Path.Combine(this.FullName, fileName);
            return new File(new FileInfo(path));
        }


        public IDirectory CreateSubdirectory(string path)
        {
            var dir = this.info.CreateSubdirectory(path);
            return new Directory(dir);
        }


        public void Delete(bool recursive = false)
        {
            this.info.Delete(recursive);
        }


        private IEnumerable<IDirectory> directories;
        public IEnumerable<IDirectory> Directories
        {
            get
            {
                this.directories = this.directories ?? this.info.GetDirectories().Select(x => new Directory(x)).ToList();
                return this.directories;
            }
        }


        private IEnumerable<IFile> files;
        public IEnumerable<IFile> Files
        {
            get
            {
                this.files = this.files ?? this.info.GetFiles().Select(x => new File(x)).ToList();
                return this.files;
            }
        }

        #endregion
    }
}