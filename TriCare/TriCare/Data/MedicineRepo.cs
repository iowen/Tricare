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
    public class MedicineRepo
    {
        protected SQLiteConnection database;
		private int isCreated;
        public MedicineRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<Medicine>();
        }
		public bool InsertRecords()
		{
			try
			{
			var ic = (from i in database.Table<Medicine>() select i).ToList();
				return ic.Count <= 0;
			}
			catch(Exception ex) {
				return true;
			}

		}
		public async Task<string> GetMedicines()
        {
            using (var client = new HttpClient())
            {
				client.BaseAddress = new Uri(App.ApiUrL);

				var resultTask = await client.GetAsync(App.ApiUrL+"/api/Medicine");
                var resultText = resultTask.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultFix = JsonConvert.DeserializeObject(resultText);
                    var resultItem = JsonConvert.DeserializeObject< List<Medicine>>(resultFix);
					foreach (var item in resultItem)
					{
						database.Insert(item);
					}
                }
                catch (Exception ex)
                {
                    resultText = ex.ToString();
                }
				var returnTask = new TaskCompletionSource<string>();
				returnTask.SetResult("success");
				return await returnTask.Task;
            }
        }

        public List<Medicine> GetAllMedicines()
        {
            return (from i in database.Table<Medicine>() select i).ToList();
        }

        public Medicine GetMedicine(int id)
        {
            return database.Table<Medicine>().FirstOrDefault(x => x.MedicineId == id);
        }

        public int AddMedicine(Medicine item)
        {
            return database.Insert(item);
        }

        public int DeleteMedicine(int id)
        {
            return database.Delete<Medicine>(id);
        }
    }
}
