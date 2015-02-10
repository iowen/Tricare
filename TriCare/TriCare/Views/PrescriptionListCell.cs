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
				TextColor = Color.Navy,
				Text = "Patient : "
			};

			var labelPatientName = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.Navy,
			};
			labelPatientName.SetBinding(Label.TextProperty, "PatientNameFriendly");

			var labelMedicine = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.Navy,
				Text = "Medicine : "
			};

			var labelMedicineName = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.Navy,
			};
			labelMedicineName.SetBinding(Label.TextProperty, "MedicineNameFriendly");

			var labelCreated = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.Navy,
				Text = "Date : "
			};

			var labelCreatedText = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.Navy,
			};
			labelCreatedText.SetBinding(Label.TextProperty, "CreatedFriendly");

			var pstack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { labelPatient, labelPatientName }

			};
			var mstack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { labelMedicine, labelMedicineName }
			};
			var cstack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { labelCreated, labelCreatedText }
			};
			var stack = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
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

