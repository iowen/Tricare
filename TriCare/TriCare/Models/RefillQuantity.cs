using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
	public class RefillQuantity
	{
		[PrimaryKey]
		public int RefillQuantityId { get; set; }
		public int Quantity { get; set; }
	}
}
