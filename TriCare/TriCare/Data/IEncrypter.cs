using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Data
{
    public interface IEncrypter
    {
		string GetEncryption (string input);
		string GetDecryption(string input);
    }
}
