using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class Prescription
    {
        [PrimaryKey]
		public int PrescriptionId{ get; set; }
		public int PrescrberId{ get; set; }
		public int PatientId{ get; set; }
		public DateTime Created{ get; set; }
		public string Location { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public class PrescriptionModel
    {
        public int PrescriptionId;
        public Prescriber Prescriber;
        public Patient Patient;
        public DateTime Created;
        public MedicineModelForPrescription Medicine;
        public RefillModel Refill;
		[Ignore]
		public string PatientNameFriendly { get{return Patient.FirstName.Trim () + " " + Patient.LastName.Trim ();}}
		public string MedicineNameFriendly {get{return Medicine.MedicineName.Trim();}}
		public string CreatedFriendly { get { return Created.ToString ("d"); } }
    }
		

    public class PrescriptionMedicineModel
    {
        public int PrescriptionId;
        public Prescriber Prescriber;
        public Patient Patient;
        public DateTime Created;
        public string MedicineName;
        public string Location;
        public DateTime LastUpdate;
        public int MedicineId;
        public List<PrescriptionMedicineIngredientModel> Ingredients;
		public PrescriptionRefillModel Refill;
    }


}
