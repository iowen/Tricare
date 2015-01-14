using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriCare.Data
{
    public class SyncRepo
    {
         protected SQLiteConnection database;

        public SyncRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<Patient>();
        }

        public List<Patient> GetAllPatients()
        {
            return (from i in database.Table<Patient>() select i).ToList();
        }
    }
}
