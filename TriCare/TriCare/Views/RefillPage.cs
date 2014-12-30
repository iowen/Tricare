using System;
using System.Collections.Generic;
using TriCare.Data;
using Xamarin.Forms;

namespace TriCare
{
	public class RefillPage :ContentPage
	{
		public RefillPage ()
		{
			this.BackgroundImage = "tricareBG.png";

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
					BackgroundColor =Color.Transparent

			};
			var refillQuantPicker = new Picker {
				Title = "Quantity",
				BackgroundColor =Color.Transparent
			};
            foreach(var r in ra)
            {
				refillAmountPicker.Items.Add(r.Amount.ToString());
            }


            var qa =rRepo.GetAllRefillQuantities();
            foreach(var q in qa)
            {
				refillQuantPicker.Items.Add(q.Quantity.ToString());
            }


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(40),
                Children = {
					medL, amL, refillAmountPicker, aqL,refillQuantPicker
                }
            };
		}
	}
}

