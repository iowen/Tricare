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
			this.BackgroundImage = "tricareBG.png";
			ca = new List<string> ();
			cq = new List<string> ();
            var rRepo = new RefillRepo();
            Title = "Refills";

            var ra =rRepo.GetAllRefillAmounts();
			var medL = new Label {
				TextColor = Color.White,
				Text = "Medicine : "+App.CurrentPrescription.Medicine.MedicineName.Trim()
			};
			var amL = new Label {
				TextColor = Color.White,
				Text = "Refill Amount in MG",

			};
			var aqL = new Label {
				TextColor = Color.White,
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
				App.CurrentPrescription.Refill.Amount = int.Parse(ca[refillAmountPicker.SelectedIndex]);
				App.CurrentPrescription.Refill.Quantity = int.Parse(cq[refillQuantPicker.SelectedIndex]);
				Navigation.PushAsync(new VerifyPage());
			};

            foreach(var r in ra)
            {
				ca.Add (r.Amount.ToString ());
				refillAmountPicker.Items.Add(r.Amount.ToString());
            }


            var qa =rRepo.GetAllRefillQuantities();
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

