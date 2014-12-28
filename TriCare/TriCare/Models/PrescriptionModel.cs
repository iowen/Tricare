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
    }

    public class PrescriptionModel
    {
        public int PrescriptionId;
        public Prescriber Prescriber;
        public Patient Patient;
        public DateTime Created;
        public MedicineModelForPrescription Medicine;
        public RefillModel Refill;
    }
}
