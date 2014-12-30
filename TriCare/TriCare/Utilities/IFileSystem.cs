using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Utilities
{
    public interface IFileSystem
    {

        IDirectory AppData { get; }
        IDirectory Cache { get; }
        IDirectory Public { get; }
        IDirectory Temp { get; }

        IDirectory GetDirectory(string path);
        IFile GetFile(string path);
    }
}
