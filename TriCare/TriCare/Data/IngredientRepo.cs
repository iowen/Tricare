using Newtonsoft.Json;
using Newtonsoft.Json;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TriCare.Models;
using Xamarin.Forms;

namespace TriCare.Data
{
    public class IngredientRepo
    {
        protected SQLiteConnection database;
		private int isCreated;
        public IngredientRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<Ingredient>();
        }
		public bool InsertRecords()
		{
			try
			{
			var ic = (from i in database.Table<Ingredient>() select i).ToList();
				return ic.Count <= 0;
			}
			catch(Exception ex) {
				return true;
			}

		}
		public async Task<string> GetIngredients()
        {

            using (var client = new HttpClient())
			{				
				var appToken = App.GetAppToken();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",appToken.AccessToken);
				client.BaseAddress = new Uri(App.ApiUrL);

				var resultTask = await client.GetAsync(App.ApiUrL+"/api/Ingredient");
                var resultText = resultTask.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultFix = JsonConvert.DeserializeObject(resultText);
                    var resultItem = JsonConvert.DeserializeObject< List<Ingredient>>(resultFix);
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

        public List<Ingredient> GetAllIngredients()
        {
            return (from i in database.Table<Ingredient>() select i).ToList();
        }

        public Ingredient GetIngredient(int id)
        {
            return database.Table<Ingredient>().FirstOrDefault(x => x.IngredientId == id);
        }

        public int AddIngredient(Ingredient item)
        {
            return database.Insert(item);
        }

        public int DeleteIngredient(int id)
        {
            return database.Delete<Ingredient>(id);
        }
    }
}
