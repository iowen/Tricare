using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriCare.Views
{
	public class PrescriptionListCell :ViewCell
	{
		public PrescriptionListCell ()
		{
			var labelPatient = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.White,
				Text = "Patient : "
			};

			var labelPatientName = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.White,
			};
			labelPatientName.SetBinding(Label.TextProperty, "PatientNameFriendly");


		}
	}
}

