using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Models
{
	public class State
	{
		[PrimaryKey,AutoIncrement]
		public int StateId { get; set; }
		public string Name { get; set; }
		public string NameFriendly {get {return ToString();}}

		public override string ToString ()
		{
			return Name.Trim ();
		}
	}
}

