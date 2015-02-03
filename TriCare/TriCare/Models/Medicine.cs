using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class Medicine
    {
        [PrimaryKey]
        public int MedicineId { get; set; }
        public string Name { get; set; }
		public string Directions{ get; set; }
		public int MedicineCategoryId { get; set; }
    }

    public class PrescriptionMedicine
    {
        [PrimaryKey]
        public int PrescriptionMedicineId { get; set; }
        public int MedicineId { get; set; }
        public int PrescriptionId { get; set; }
		[Ignore]
		public string Directions{ get; set; }
		public int MedicineCategoryId { get; set; }

    }


	public class MedicineWithIngredients
	{
		public int MedicineId { get; set; }
		public string Name { get; set; }
		public List<MedicineIngredient> Ingredients { get; set; }
		public string Directions{ get; set; }
		public int MedicineCategoryId { get; set; }

	}
}
