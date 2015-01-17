using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class AppDataUpdate
    {
        [PrimaryKey]
        public int AppDataUpdateId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
    public class SyncModel
    {
        public char SyncType;

        public int PrescriberId;

        public DateTime LastSync;

        public DateTime LastAppDataSync;
    }

    public class AppSyncDataModel
    {
        public List<InsuranceCarrier> InsuranceCarriers;
        public List<Medicine> Medicines;
        public List<Ingredient> Ingredients;
        public List<MedicineIngredient> MedicineIngredients;
        public List<RefillAmount> RefillAmounts;
        public List<RefillQuantity> RefillQuantities;
		public DateTime Updated;
        public AppSyncDataModel()
        {
            InsuranceCarriers = new List<InsuranceCarrier>();
            Medicines = new List<Medicine>();
            Ingredients = new List<Ingredient>();
            MedicineIngredients = new List<MedicineIngredient>();
            RefillAmounts = new List<RefillAmount>();
            RefillQuantities = new List<RefillQuantity>();
        }

    }

    public class PrescriberSyncDataModel
    {
        public Prescriber Prescriber;
        public List<Patient> Patients;
        public List<PrescriptionMedicineModel> Prescriptions;
		public DateTime Updated;
        public PrescriberSyncDataModel()
        {
            Prescriber = new Prescriber();
            Patients = new List<Patient>();
            Prescriptions = new List<PrescriptionMedicineModel>();
        }
    }

    public class SyncResponseModel
    {
        public AppSyncDataModel AppDataUpdates;
        public PrescriberSyncDataModel PrescriberUpdates;
    }
}
