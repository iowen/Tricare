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

        public List<Prescription> GetAllPrescriptions()
        {
            return (from i in database.Table<Prescription>() select i).ToList();
        }

        public Prescription GetPrescription(int id)
        {
            return database.Table<Prescription>().FirstOrDefault(x => x.PrescriptionId == id);
        }
		public List<PrescriptionModel>GetPrescriptionsForPrescriber(int prescriberId)
		{
			var res = new List<PrescriptionModel> ();
			var pres = database.Table<Prescription> ().Where (x => x.PrescrberId == prescriberId).ToList ();
			foreach (var i in pres) {
				res.Add (GetPrescriptionAsModel (i.PrescriptionId));
			}
			return res;
		}
		public PrescriptionModel GetPrescriptionAsModel(int id)
		{
			var pRepo = new PrescriberRepo ();
			var presc = pRepo.GetPrescriber (int.Parse (App.Token));

			var prescr = database.Table<Prescription>().FirstOrDefault(x => x.PrescriptionId == id);

			var paRepo = new PatientRepo ();
			var pat = paRepo.GetPatient (prescr.PatientId);


			var rr = new RefillRepo ();
			var rm = rr.GetPrescriptionRefillAsModel (id);
			var pMeds = database.Table<PrescriptionMedicine>().First(x => x.PrescriptionId == id);
			var pMedIs = database.Table<PrescriptionMedicineIngredient>().Where(x => x.PrescriptionMedicineId == pMeds.PrescriptionMedicineId).ToList();
			var mIng = new List<PrescriptionMedicineIngredientModel> ();

			foreach (var i in pMedIs) {
				mIng.Add(new PrescriptionMedicineIngredientModel(){Percentage = i.Percentage, PrescriptionMedicineId = i.PrescriptionMedicineId,PrescriptionMedicineIngredientId = i.PrescriptionMedicineIngredientId,IngredientId = i.IngredientId, Name = i.Name});
			}
			var mm = new MedicineModelForPrescription ();
			var mRepo = new MedicineRepo ();
			var med = mRepo.GetMedicine (pMeds.MedicineId);
			mm.MedicineId = med.MedicineId;
			mm.Ingredients = mIng;
			mm.MedicineName = med.Name.Trim();
			mm.PrescriptionId = id;
			return new PrescriptionModel (){Prescriber = presc, PrescriptionId = id,Patient = pat,Created = prescr.Created,Refill = rm ,Medicine = mm};
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
					FileName = "Sig777.png"
				};
				content.Add(fileContent);

				var resultTask = await client.PostAsync("http://teamsavagemma.com/api/PrescriptionMedicine", content);
				var resultText = resultTask.Content.ReadAsStringAsync().Result;

				dynamic resultFix = JsonConvert.DeserializeObject(resultText);

				var pReturn = JsonConvert.DeserializeObject<CreatePrescriptionModel>(resultFix);

				if (pReturn.PrescriptionId > 0)
				{
					var p = new Prescription(){PrescrberId = pReturn.PrescriberId, PatientId = pReturn.PatientId, PrescriptionId = pReturn.PrescriptionId, Location = pReturn.Location, Created = pReturn.Created};
					database.Insert(p);
					var pm = new PrescriptionMedicine(){MedicineId = pReturn.MedicineId, PrescriptionMedicineId = pReturn.PrescriptionMedicineId, PrescriptionId = pReturn.PrescriptionId};
					database.Insert(pm);
					foreach(var ing in pReturn.Ingredients)
					{
						var ii = database.Table<Ingredient>().FirstOrDefault(x => x.IngredientId == ing.IngredientId);
						var pmi = new PrescriptionMedicineIngredient(){Name = ii.Name,Percentage = ing.Percentage, PrescriptionMedicineId = pReturn.PrescriptionMedicineId,PrescriptionMedicineIngredientId = ing.PrescriptionMedicineIngredientId,IngredientId = ing.IngredientId};
						database.Insert(pmi);
					}
					var pr = new PresciptionRefill(){RefillAmountId = pReturn.RefillAmount, RefillQuantityId = pReturn.RefillQuantity, PrescriptionRefillId = pReturn.PrescriptionRefillId, PrescriptionId = pReturn.PrescriptionId};
					database.Insert(pr);
					return "success";
				}
				return "failure";
			}
			catch (Exception ex)
			{
				return "error";
			}

        }

        public int DeleteIngredient(int id)
        {
            return database.Delete<Ingredient>(id);
        }
    }
}
