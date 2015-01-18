using System;
using System.Collections.Generic;
using TriCare.Data;
using Xamarin.Forms;

namespace TriCare
{
	public class RefillPage :ContentPage
	{

		List<string> ca;
			List<string> cq;
		public RefillPage ()
		{
			this.BackgroundColor = Color.White;
			ca = new List<string> ();
			cq = new List<string> ();
            var rRepo = new RefillRepo();
            Title = "Refills";
			App.EnableLogout ();
            var ra =rRepo.GetAllRefillAmounts();
			var qa =rRepo.GetAllRefillQuantities();

			var medL = new Label {
				TextColor = Color.Navy,
				Text = "Medicine : "+App.CurrentPrescription.Medicine.MedicineName.Trim()
			};
			var amL = new Label {
				TextColor = Color.Navy,
				Text = "Refill Amount in MG",

			};
			var aqL = new Label {
				TextColor = Color.Navy,
				Text = "Refill Quantity"
			};
			var refillAmountPicker = new Picker {
				Title = "Amount",
					BackgroundColor =Color.Transparent,
					
			};
			var refillQuantPicker = new Picker {
				Title = "Quantity",
				BackgroundColor =Color.Transparent
			};
			var continueButton = new Button { Text = "Continue", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
			continueButton.Clicked += (sender, e) =>
			{
				App.CurrentPrescription.Refill = new TriCare.Models.RefillModel();
				App.CurrentPrescription.Refill.Amount = ra[refillAmountPicker.SelectedIndex].RefillAmountId;
				App.CurrentPrescription.Refill.Quantity = qa[refillQuantPicker.SelectedIndex].RefillQuantityId;
				Navigation.PushAsync(new VerifyPage());
			};

            foreach(var r in ra)
            {
				ca.Add (r.Amount.ToString ());
				refillAmountPicker.Items.Add(r.Amount.ToString());
            }


            foreach(var q in qa)
            {
				cq.Add (q.Quantity.ToString ());
				refillQuantPicker.Items.Add(q.Quantity.ToString());
            }


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(40),
                Children = {
					medL, amL, refillAmountPicker, aqL,refillQuantPicker,continueButton
                }
            };
		}
	}
}

