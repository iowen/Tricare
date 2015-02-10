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
using System.Net.Http.Headers;

namespace TriCare.Data
{
    public class MedicineCategoryRepo
    {
        protected SQLiteConnection database;
		private int isCreated;
        public MedicineCategoryRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<MedicineCategory>();
        }
		public bool InsertRecords()
		{
			try
			{
			var ic = (from i in database.Table<MedicineCategory>() select i).ToList();
				return ic.Count <= 0;
			}
			catch(Exception ex) {
				return true;
			}

		}
		public async Task<string> GetMedicineCategories()
        {
            using (var client = new HttpClient())
            {
				var appToken = App.GetAppToken();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",appToken.AccessToken);
				client.BaseAddress = new Uri(App.ApiUrL);

				var resultTask = await client.GetAsync(App.ApiUrL+"/api/MedicineCategory");
                var resultText = resultTask.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultFix = JsonConvert.DeserializeObject(resultText);
                    var resultItem = JsonConvert.DeserializeObject< List<MedicineCategory>>(resultFix);
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

        public List<MedicineCategory> GetAllMedicineCategories()
        {
            return (from i in database.Table<MedicineCategory>() select i).ToList();
        }

        public MedicineCategory GetMedicineCategory(int id)
        {
            return database.Table<MedicineCategory>().FirstOrDefault(x => x.MedicineCategoryId == id);
        }

        public int AddMedicineCategory(MedicineCategory item)
        {
            return database.Insert(item);
        }

        public int DeleteMedicineCategory(int id)
        {
            return database.Delete<MedicineCategory>(id);
        }
    }
}
