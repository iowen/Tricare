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
        public int Amount;
        public int Quantity;
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
    }
}
