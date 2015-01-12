using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class Ingredient
    {
        [PrimaryKey]
        public int IngredientId { get; set; }
        public string Name { get; set; }

		public override string ToString ()
		{
			var a = Name.Trim ();
			return a;
		}
    }
}
