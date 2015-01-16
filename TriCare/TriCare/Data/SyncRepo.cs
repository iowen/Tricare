﻿using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TriCare.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace TriCare.Data
{
    public class SyncRepo
    {
         protected SQLiteConnection database;

        public SyncRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
			database.CreateTable<AppDataUpdate>();
			database.CreateTable<Patient>();
            database.CreateTable<InsuranceCarrier>();
            database.CreateTable<Prescriber>();
            database.CreateTable<Medicine>();
            database.CreateTable<Ingredient>();
            database.CreateTable<MedicineIngredient>();
            database.CreateTable<Prescription>();
            database.CreateTable<PrescriptionMedicine>();
            database.CreateTable<PrescriptionMedicineIngredient>();
            database.CreateTable<PresciptionRefill>();
            database.CreateTable<RefillAmount>();
            database.CreateTable<RefillQuantity>();
        }

        public async Task GetSyncData(SyncModel model)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);

                var content = new FormUrlEncodedContent(new[] 
					{
						new KeyValuePair<string, string>("", json)
					});
                var resultTask = await client.PostAsync("http://teamsavagemma.com/api/Sync", content);
                var resultText = resultTask.Content.ReadAsStringAsync().Result;
                try
                {
                    dynamic resultFix = JsonConvert.DeserializeObject(resultText);
                    var resultItem = JsonConvert.DeserializeObject<SyncResponseModel>(resultFix);
                    if (resultItem != null)
                    {
						if(model.SyncType == 'b' || model.SyncType == 'a')
						{
						database.InsertOrReplaceAll(resultItem.AppDataUpdates.InsuranceCarriers);
						database.InsertOrReplaceAll(resultItem.AppDataUpdates.Ingredients);
						database.InsertOrReplaceAll(resultItem.AppDataUpdates.Medicines);
						database.InsertOrReplaceAll(resultItem.AppDataUpdates.MedicineIngredients);
						database.InsertOrReplaceAll(resultItem.AppDataUpdates.RefillAmounts);
						database.InsertOrReplaceAll(resultItem.AppDataUpdates.RefillQuantities);
						if(model.LastAppDataSync < new DateTime(1987,12,31))
						{
							var au = new AppDataUpdate(){AppDataUpdateId =1, LastUpdate = resultItem.AppDataUpdates.Updated};
							database.InsertOrReplace(au);
						}
							else{
								var aa = database.Table<AppDataUpdate>().First(x =>x.AppDataUpdateId == 1);
								aa.LastUpdate = resultItem.AppDataUpdates.Updated;
								database.Update(aa);
							}
						}
						if(model.SyncType == 'b' || model.SyncType == 'p')
						{
							var p = resultItem.PrescriberUpdates.Prescriber;
							p.LastUpdate = resultItem.PrescriberUpdates.Updated; 
							database.InsertOrReplace(p);
							database.InsertOrReplaceAll(resultItem.PrescriberUpdates.Patients);
						}
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
        public DateTime GetLastAppUpdate()
        {
            try
            {
                return database.Table<AppDataUpdate>().First(x => x.AppDataUpdateId == 1).LastUpdate;
            }
            catch(Exception ex)
            {
                return new DateTime(1987, 11, 21);
            }
        }
    }
}