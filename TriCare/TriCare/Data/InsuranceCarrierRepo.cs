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
    public class InsuranceCarrierRepo
    {
        protected SQLiteConnection database;
		private int isCreated;
        public InsuranceCarrierRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<InsuranceCarrier>();
        }
		public bool InsertRecords()
		{
			try
			{
			var ic = (from i in database.Table<InsuranceCarrier>() select i).ToList();
				return ic.Count <= 0;
			}
			catch(Exception ex) {
				return true;
			}

		}
		public async Task<string> GetInsuranceCarriers()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://teamsavagemma.com");

                var resultTask = await client.GetAsync("http://teamsavagemma.com/api/InsuranceCarrier");
                var resultText = resultTask.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultFix = JsonConvert.DeserializeObject(resultText);
                    var resultItem = JsonConvert.DeserializeObject< List<InsuranceCarrier>>(resultFix);
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

        public List<InsuranceCarrier> GetAllInsuranceCarriers()
        {
            return (from i in database.Table<InsuranceCarrier>() select i).ToList();
        }

        public InsuranceCarrier GetInsuranceCarrier(int id)
        {
            return database.Table<InsuranceCarrier>().FirstOrDefault(x => x.InsuranceCarrierId == id);
        }

        public int AddInsuranceCarrier(InsuranceCarrier item)
        {
            return database.Insert(item);
        }

        public int DeleteInsuranceCarrier(int id)
        {
            return database.Delete<InsuranceCarrier>(id);
        }
    }
}
