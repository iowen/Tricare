using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Utilities
{
    public interface ISignatureService
    {
        SignaturePadConfiguration Configuration { get; }

        void Request(Action<SignatureResult> onAction);
        //void Load(IEnumerable<DrawPoint> points);
    }
}
