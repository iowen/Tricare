using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
    public class InsuranceCarrier
    {
        [PrimaryKey]
        public int InsuranceCarrierId { get; set; }
        public string Name { get; set; }
		[Ignore]
		public string NameFriendly {get {return ToString();}}
		public override string ToString ()
		{
			return Name.Trim ();
		}

    }
}
