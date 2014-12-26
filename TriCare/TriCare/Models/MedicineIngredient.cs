using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class MedicineIngredient
    {
        [PrimaryKey]
		public int MedicineIngredientId { get; set; }
		public int MedicineId { get; set; }
		public int IngredientId { get; set; }
        public string Name { get; set; }
		public double Percentage { get; set; }
		[Ignore]
		public string PercentageFriendly{ get { return Percentage.ToString (); } }	
		[Ignore]
		public string NameFriendly{ get { return Name.ToString (); } }
    }
}
