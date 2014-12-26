using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Data
{
    public interface ISqLite
    {
        SQLiteConnection GetConnection();
    }
}
