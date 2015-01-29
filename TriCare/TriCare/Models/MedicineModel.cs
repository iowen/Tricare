using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class RefillModel
    {
        public int PrescriptionId;
        public int PrescriptionRefillId;
        public int Amount;
        public int Quantity;
    }
    public class RefillAmountModel
    {
        public int RefillAmountId;
        public int Amount;
    }
    public class RefillQuantityModel
    {
        public int RefillQuantityId;
        public int Quantity;
    }

    public class PrescriptionRefillModel
    {
        public int PrescriptionId;
        public RefillAmountModel Amount;
        public RefillQuantityModel Quantity;
        public int PrescriptionRefillId;

    }
   public class PrescriptionMedicineIngredientModel
    {
        public int PrescriptionMedicineIngredientId;
        public int PrescriptionMedicineId;
        public int IngredientId;
        public string Name;
        public double Percentage;
    }
    public class MedicineModelForPrescription
    {
        public int PrescriptionId;
        public string MedicineName;
        public int MedicineId;
        public List<PrescriptionMedicineIngredientModel> Ingredients;
		public string Directions;
    }
	public class MedicineIngredientForPrescriptionModel
	{
		public int IngredientId;
		public double Percentage;
		public int PrescriptionMedicineIngredientId;

	}
		

	public class CreatePrescriptionModel
	{
		public int PrescriptionId;
		public int PrescriberId;
		public int PatientId;
		public int PrescriptionMedicineId;
		public string Location;
		public DateTime Created;
		public int MedicineId;
		public List<MedicineIngredientForPrescriptionModel> Ingredients;
		public int PrescriptionRefillId;
		public int RefillAmount;
		public int RefillQuantity;
	}
}
