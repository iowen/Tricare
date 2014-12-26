using Newtonsoft.Json;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TriCare.Models;
using Xamarin.Forms;

namespace TriCare.Data
{
    public class PatientRepo
    {
        protected SQLiteConnection database;

        public PatientRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<Patient>();
        }

        public List<Patient> GetAllPatients()
        {
            return (from i in database.Table<Patient>() select i).ToList();
        }

        public List<Patient> GetAllPatientsForPrescriber(int prescriber)
        {
            return database.Table<Patient>().Where(x => x.PrescriberId == prescriber).ToList();
        }

        public Patient GetPatient(int id)
        {
            return database.Table<Patient>().FirstOrDefault(x => x.PatientId == id);
        }

        public async Task<string> AddPatient(Patient item)
        {
            try
            {
                var client = new HttpClient();
                //	client.BaseAddress = new Uri("");
                var json = JsonConvert.SerializeObject(item);

				var content = new FormUrlEncodedContent(new[] 
					{
						new KeyValuePair<string, string>("", json)
					});



                var resultTask = await client.PostAsync("http://teamsavagemma.com/api/patient", content);
				var resultText = resultTask.Content.ReadAsStringAsync().Result;

				var pReturn = JsonConvert.DeserializeObject<int>(resultText);
				if (pReturn > 0)
				{
					item.PatientId = pReturn;
					database.Insert(item);
					return resultText;
				}
				return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int DeletePatient(int id)
        {
            return database.Delete<Patient>(id);
        }
    }
}
