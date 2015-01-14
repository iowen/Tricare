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
    public class RefillRepo
    {
        protected SQLiteConnection database;

        public RefillRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<RefillAmount>();
            database.CreateTable<RefillQuantity>();
        }
        public bool InsertRecords()
        {
            try
            {
                var ic = (from i in database.Table<RefillAmount>() select i).ToList();
                return ic.Count <= 0;
            }
            catch (Exception ex)
            {
                return true;
            }

        }
        public List<RefillAmount> GetAllRefillAmounts()
        {
            return (from i in database.Table<RefillAmount>() select i).ToList();
        }

        public List<RefillQuantity> GetAllRefillQuantities()
        {
            return (from i in database.Table<RefillQuantity>() select i).ToList();
        }

        public int GetRefillIdForAmount(int amount)
        {
            var a = database.Table<RefillAmount>().First(x => x.Amount == amount);
            return a.RefillAmountId;
        }

        public int GetRefillIdForQuantity(int quantity)
        {
            var a = database.Table<RefillQuantity>().First(x => x.Quantity == quantity);
            return a.RefillQuantityId;
        }

		public int GetRefillAmountForId(int amount)
		{
			var a = database.Table<RefillAmount>().First(x => x.RefillAmountId == amount);
			return a.Amount;
		}

		public int GetRefillQuantityForId(int quantity)
		{
			var a = database.Table<RefillQuantity>().First(x => x.RefillQuantityId == quantity);
			return a.Quantity;
		}
        public async Task<string> GetRefillQuantities()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://teamsavagemma.com");

                var resultTask = await client.GetAsync("http://teamsavagemma.com/api/RefillQuantity");
                var resultText = resultTask.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultFix = JsonConvert.DeserializeObject(resultText);
                    var resultItem = JsonConvert.DeserializeObject<List<RefillQuantity>>(resultFix);
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
        public async Task<string> GetRefillAmounts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://teamsavagemma.com");

                var resultTask = await client.GetAsync("http://teamsavagemma.com/api/RefillAmount");
                var resultText = resultTask.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultFix = JsonConvert.DeserializeObject(resultText);
                    var resultItem = JsonConvert.DeserializeObject<List<RefillAmount>>(resultFix);
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

		public RefillModel GetPrescriptionRefillAsModel(int prescriptionId)
		{
			var p = database.Table<Prescription>().FirstOrDefault(x => x.PrescriptionId == prescriptionId);
			var pr = database.Table<PresciptionRefill>().FirstOrDefault(x => x.PrescriptionId == prescriptionId);
//			var raa =GetRefillAmountForId(pr.RefillAmountId);
//			var qa =GetRefillQuantityForId(pr.RefillQuantityId);
//			var ra = new RefillAmount (){RefillAmountId = pr.RefillAmountId, Amount = raa };
//			var rq = new RefillQuantity (){RefillQuantityId = pr.RefillQuantityId, Quantity = qa };
			return new RefillModel (){Amount = pr.RefillAmountId, Quantity = pr.RefillQuantityId,PrescriptionId = prescriptionId };
		}
    }
}
