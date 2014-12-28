using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using TriCare.Models;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace TriCare.Data
{
    public class PrescriberRepo
    {
        protected SQLiteConnection database;

        public PrescriberRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<Prescriber>();
            InsuranceCarrierRepo ir = new InsuranceCarrierRepo();
        }

        public List<Prescriber> GetAllPrescribers()
        {
            return (from i in database.Table<Prescriber>() select i).ToList();
        }

        public Prescriber GetPrescriber(int id)
        {
            return database.Table<Prescriber>().FirstOrDefault(x => x.PrescriberId == id);
        }

		public async Task<string> LoginPrescriber(LoginModel login)
        {
			if (database.Table<Prescriber>().Any(x => x.Email == login.Email && x.Password == login.Password))
				{
            var prescriber = database.Table<Prescriber>().FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);

                App.SaveToken(prescriber.PrescriberId.ToString());
                var returnTask = new TaskCompletionSource<string>();
                returnTask.SetResult("success");
                return await returnTask.Task;
            }

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://teamsavagemma.com");
					var json = JsonConvert.SerializeObject(login);

				var content = new FormUrlEncodedContent(new[] 
					{
						new KeyValuePair<string, string>("", json)
					});
				var resultTask = await client.PostAsync("http://teamsavagemma.com/api/PrescriberLogin", content);
					var resultText = resultTask.Content.ReadAsStringAsync().Result;
				try
				{
					dynamic resultFix = JsonConvert.DeserializeObject(resultText);
					var resultItem = JsonConvert.DeserializeObject<Prescriber>(resultFix);
					if (resultItem.PrescriberId > 0) 
					{
                        var pRepo = new PrescriberRepo();
                        var patRepo = new PatientRepo();
                        
						//var p =  new Prescriber() { AccountId = resultItem.AccountId, Address = resultItem.Address, City = resultItem.City, DeaNumber = resultItem.DeaNumber, Email = resultItem.Email, Fax = resultItem.Fax, FirstName = resultItem.FirstName, LastName = resultItem.LastName, LicenseNumber = resultItem.LicenseNumber, NpiNumber = resultItem.NpiNumber, Password = resultItem.Password, Phone = resultItem.Phone, PrescriberId = resultItem.PrescriberId, State = resultItem.State, Zip = resultItem.Zip };

						pRepo.AddPrescriberLocal(resultItem);
                        patRepo.PullAllPatientsForPrescriber(resultItem.PrescriberId);
                        App.SaveToken(resultItem.PrescriberId.ToString());
                        var returnTask = new TaskCompletionSource<string>();
                        returnTask.SetResult("success");
                        return await returnTask.Task;
					}
				}
				catch (Exception ex) {
					resultText = ex.ToString ();
				}


				var returnTask1 = new TaskCompletionSource<string>();
				returnTask1.SetResult("failure");
				return await returnTask1.Task;
				}
        }

		public async Task<string> GetResponseMessage(Prescriber item)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://teamsavagemma.com");
				var json = JsonConvert.SerializeObject(item);

				var content = new FormUrlEncodedContent(new[] 
					{
						new KeyValuePair<string, string>("", json)
					});

				var resultTask = await client.PostAsync ("http://teamsavagemma.com/api/Prescriber", content);
				var resultText = resultTask.Content.ReadAsStringAsync().Result;
				return resultText;
			}

		}
        public void AddPrescriberLocal (Prescriber item)
        {
            database.Insert(item);
        }

		public async Task<string> AddPrescriber(Prescriber item)
		{
			try
			{
			var client = new HttpClient ();
			//	client.BaseAddress = new Uri("");
				var json = JsonConvert.SerializeObject(item);

				var content = new FormUrlEncodedContent(new[] 
					{
						new KeyValuePair<string, string>("", json)
					});
			
				var resultTask = await client.PostAsync("http://teamsavagemma.com/api/prescriber", content);
				var resultText = resultTask.Content.ReadAsStringAsync().Result;
				var pReturn = JsonConvert.DeserializeObject<int>(resultText);
				if (pReturn > 0)
				{
					item.PrescriberId = pReturn;
					 database.Insert(item);
					App.SaveToken(item.PrescriberId.ToString());

					return resultText;
				}
				return null;
			}
			catch(Exception ex)
			{
				return null;
			}
        }

        public int DeletePrescriber(int id)
        {
            return database.Delete<Prescriber>(id);
        }
    }
}
