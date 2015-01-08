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
using System.IO;
using System.Net.Http.Headers;
namespace TriCare.Data
{
    public class PrescriptionRepo
    {
        protected SQLiteConnection database;
		private int isCreated;
		public PrescriptionRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
			database.CreateTable<Prescription>();
			database.CreateTable<PrescriptionMedicine>();
			database.CreateTable<PrescriptionMedicineIngredient>();
			database.CreateTable<PresciptionRefill>();

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
                client.BaseAddress = new Uri("http://teamsavagemma.com");

                var resultTask = await client.GetAsync("http://teamsavagemma.com/api/Ingredient");
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

		public async Task<string>  AddPrescription(CreatePrescriptionModel item, byte[] Sig)
        {
			try
			{
				var client = new HttpClient();
				//	client.BaseAddress = new Uri("");
				var json = JsonConvert.SerializeObject(item);

				var content = new MultipartFormDataContent();
				content.Add(new StringContent(json), "value");
				var fileContent = new ByteArrayContent(Sig);
				fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
				{
					FileName = "Sig.png"
				};
				content.Add(fileContent);

				var resultTask = await client.PostAsync("http://teamsavagemma.com/api/PrescriptionMedicine", content);
				var resultText = resultTask.Content.ReadAsStringAsync().Result;
//
//				dynamic resultFix = JsonConvert.DeserializeObject(resultText);
//
//				var pReturn = JsonConvert.DeserializeObject<CreatePrescriptionModel>(resultFix);

//				if (pReturn > 0)
//				{
//					item.PatientId = pReturn;
//					database.Insert(item);
//					return resultText;
//				}
//				return pReturn.MedicineId.ToString();
				return resultText;
			}
			catch (Exception ex)
			{
				return null;
			}

         //   return database.Insert(item);
        }

        public int DeleteIngredient(int id)
        {
            return database.Delete<Ingredient>(id);
        }
    }
}
