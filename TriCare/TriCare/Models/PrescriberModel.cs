﻿using System;

namespace TriCare.Models
{
	public class PrescriberModel
	{
		public int PrescriberId{ get; set; }

		public int AccountId{ get; set; }

		public string FirstName{ get; set; }

		public string LastName{ get; set; }

		public string NpiNumber{ get; set; }

		public string LicenseNumber{ get; set; }

		public string DeaNumber{ get; set; }

		public string Address{ get; set; }

		public string City{ get; set; }

		public string State{ get; set; }

		public int Zip{ get; set; }

		public string Phone{ get; set; }

		public string Fax{ get; set; }

		public string Email{ get; set; }

		public string Password{ get; set; }
}
}

