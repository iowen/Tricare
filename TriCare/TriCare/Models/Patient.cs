using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class Patient
    {
        [PrimaryKey]
        public int PatientId { get; set; }
        public int PrescriberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int SSN { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Allergies { get; set; }
        public string Diagnosis { get; set; }
        public int InsuranceCarrierId { get; set; }
        public string InsuranceCarrierIdNumber { get; set; }
        public string InsuranceGroupNumber { get; set; }
        public string RxBin { get; set; }
        public string RxPcn { get; set; }
        public string InsurancePhone { get; set; }
        public string PaymentType { get; set; }
        public DateTime LastUpdate { get; set; }
		[Ignore]
		public string NameFriendly{ get { return FirstName.ToString ().Trim() +" "+ LastName.ToString ().Trim(); } }
        public string BirthDateFriendly { get { return BirthDate.ToString("d"); } }
    }
}
