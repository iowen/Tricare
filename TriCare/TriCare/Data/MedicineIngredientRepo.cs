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
    public class MedicineIngredientRepo
    {
        protected SQLiteConnection database;
		private int isCreated;
        public MedicineIngredientRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<MedicineIngredient>();
        }
		public bool InsertRecords()
		{
			try
			{
			var ic = (from i in database.Table<MedicineIngredient>() select i).ToList();
				return ic.Count <= 0;
			}
			catch(Exception ex) {
				return true;
			}

		}
		public async Task<string> GetMedicineIngredients()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://teamsavagemma.com");

                var resultTask = await client.GetAsync("http://teamsavagemma.com/api/MedicineIngredient");
                var resultText = resultTask.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultFix = JsonConvert.DeserializeObject(resultText);
                    var resultItem = JsonConvert.DeserializeObject< List<MedicineWithIngredients>>(resultFix);
					foreach (var item in resultItem)
					{
						foreach(var mItem in item.Ingredients)
						{
						database.Insert(mItem);
						}
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

		public List<MedicineIngredient> GetIngredientsForMedicine(int id)
        {
			return database.Table<MedicineIngredient>().Where(x => x.MedicineId == id).ToList();
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
