﻿using System;
using System.Collections.Generic;
using TriCare.Data;
using Xamarin.Forms;

namespace TriCare
{
	public class RefillPage :ContentPage
	{


		public RefillPage ()
		{
			this.BackgroundColor = Color.White;

            var rRepo = new RefillRepo();
            Title = "Refills";
            var ra =rRepo.GetAllRefillAmounts();
			var qa =rRepo.GetAllRefillQuantities();

			var medL = new Label {
				TextColor = Color.Navy,
				Text = "Medicine : "+App.CurrentPrescription.Medicine.MedicineName.Trim()
			};
			var amL = new Label {
				TextColor = Color.Navy,
				Text = "Refill Amount in Grams",

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
			var continueButton = new Button { 
				Text = "Continue", 
				BackgroundColor = Color.FromRgba(128, 128, 128, 128),
				TextColor = Color.White
			};
			continueButton.IsEnabled = false;

			refillAmountPicker.SelectedIndexChanged += (sender, e) => {
				if(refillAmountPicker.SelectedIndex >= 0 && refillQuantPicker.SelectedIndex >= 0)
				{
					continueButton.IsEnabled = true;
				}
				else{
					continueButton.IsEnabled = false;
				}
			};

			refillQuantPicker.SelectedIndexChanged += (sender, e) => {
				if(refillAmountPicker.SelectedIndex >= 0 && refillQuantPicker.SelectedIndex >= 0)
				{
					continueButton.IsEnabled = true;
				}
				else{
					continueButton.IsEnabled = false;
				}
			};

			continueButton.Clicked += async (sender, e) =>
			{
				App.CurrentPrescription.Refill = new TriCare.Models.RefillModel();
				App.CurrentPrescription.Refill.Amount = ra[refillAmountPicker.SelectedIndex].RefillAmountId;
				App.CurrentPrescription.Refill.Quantity = qa[refillQuantPicker.SelectedIndex].RefillQuantityId;
				await App.np.PushAsync(new VerifyPage());
			};

            foreach(var r in ra)
            {
					refillAmountPicker.Items.Add (r.Amount.ToString ()+" Grams");

            }


			foreach (var q in qa) {
				if (q.Quantity > 0) {
					refillQuantPicker.Items.Add (q.Quantity.ToString ());
				} else {
					if (q.Quantity == 0)
						refillQuantPicker.Items.Add ("NR");
					else
						refillQuantPicker.Items.Add ("PRN");
				}
			}


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(40),
                Children = {
					medL, amL, refillAmountPicker, aqL,refillQuantPicker,continueButton
                }
            };
		}
	}
}

