using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TriCare.Models
{
    public class Prescriber
    {
		[PrimaryKey, JsonProperty("PrescriberId")] 
        public int PrescriberId { get; set; }
		[JsonProperty("AccountId")] 
        public int AccountId { get; set; }
		[Required, JsonProperty("FirstName")]
        public string FirstName { get; set; }
		[Required, JsonProperty("LastName")]
        public string LastName { get; set; }
		[Required, JsonProperty("NpiNumber")]
        public string NpiNumber { get; set; }
		[Required, JsonProperty("LicenseNumber")]
        public string LicenseNumber { get; set; }
		[Required, JsonProperty("DeaNumber")]
        public string DeaNumber { get; set; }
		[Required, JsonProperty("Address")]
        public string Address { get; set; }
		[Required, JsonProperty("City")]
        public string City { get; set; }
		[Required, JsonProperty("State")]
        public string State { get; set; }
		[Required, JsonProperty("Zip")]
        public int Zip { get; set; }
		[Required, JsonProperty("Phone")]
        public string Phone { get; set; }
		[Required, JsonProperty("Fax")]
        public string Fax { get; set; }
		[Required, JsonProperty("Email")]
        public string Email { get; set; }
		[Required, JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("LastUpdate")]
        public DateTime LastUpdate { get; set; }

		[Ignore]
		public string NameFriendly{ get { return FirstName.ToString ().Trim() +" "+ LastName.ToString ().Trim(); } }
    }
}
