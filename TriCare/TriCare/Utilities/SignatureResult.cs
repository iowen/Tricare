using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Utilities
{
    public class SignatureResult
    {

        public bool Cancelled { get; private set; }
        public Stream Stream { get; private set; }
        public IEnumerable<DrawPoint> Points { get; private set; }


        public SignatureResult(bool cancelled, Stream stream, IEnumerable<DrawPoint> points)
        {
            this.Cancelled = cancelled;
            this.Stream = stream;
            this.Points = points;
        }
    }
}
