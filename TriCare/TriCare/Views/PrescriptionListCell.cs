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

			var labelMedicine = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.White,
				Text = "Medicine : "
			};

			var labelMedicineName = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.White,
			};
			labelMedicineName.SetBinding(Label.TextProperty, "MedicineNameFriendly");

			var labelCreated = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.White,
				Text = "Date : "
			};

			var labelCreatedText = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.White,
			};
			labelCreatedText.SetBinding(Label.TextProperty, "CreatedFriendly");

			var pstack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { labelPatient, labelPatientName }
			};
			var mstack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { labelMedicine, labelMedicineName }
			};
			var cstack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { labelCreated, labelCreatedText }
			};
			var stack = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {pstack,mstack,cstack}
			};
			View = stack;
		}
		protected override void OnBindingContextChanged()
		{
			// Fixme : this is happening because the View.Parent is getting 
			// set after the Cell gets the binding context set on it. Then it is inheriting
			// the parents binding context.
			View.BindingContext = BindingContext;
			this.Height = 90;
			base.OnBindingContextChanged();
		}
	}
}

