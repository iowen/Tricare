﻿using System;
using System.Collections.Generic;
using TriCare.Data;
using TriCare.Models;
using TriCare.Utilities;
using TriCare.Views;
using Xamarin.Forms;

namespace TriCare
{
	public class ViewPrescriptionPage :ContentPage
	{
		ListView listView;
		PrescriptionModel model;
		public ViewPrescriptionPage (PrescriptionModel prescription)
		{
			this.BackgroundColor = Color.White;
			Title = "View Prescription";
			model = prescription;
			var downloadButton = new Button { Text = "Download", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White  };
			downloadButton.Clicked += (sender, e) =>
			{
				DisplayAlert("alert","Would Download","close");
			};
			//get Patient Name

			listView = new ListView ();
			listView.BackgroundColor = Color.Transparent;
			listView.ItemTemplate = new DataTemplate 
				(typeof (VerifyCell));

			listView.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null)
					return;               
				listView.SelectedItem = null;
			};

			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{
					Text="View Prescription", 
					Font=Font.SystemFontOfSize (NamedSize.Large)});
			}

			layout.Children.Add (new Label { TextColor = Color.White, Text = "Tap an field for more info." });
		//	layout.Children.Add (downloadButton);
			layout.Children.Add(new StackLayout{	
				Children = {
					new ScrollView
					{
						Content = listView,
						VerticalOptions = LayoutOptions.FillAndExpand,
						HorizontalOptions = LayoutOptions.CenterAndExpand
					}
				}
			}
			);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			layout.HorizontalOptions = LayoutOptions.CenterAndExpand;
			Content = layout;


		}
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			var rRepo = new RefillRepo ();
			var am = rRepo.GetRefillAmountForId (model.Refill.Amount);
			var aq = rRepo.GetRefillQuantityForId (model.Refill.Quantity);

			var dLabel = new StringLabel () {
				NameFriendly = "Date : " + model.CreatedFriendly.Trim()
			};

			var pLabel = new StringLabel () {
				NameFriendly = "Patient : " + model.Patient.NameFriendly.Trim ()
			};

			var presLabel = new StringLabel () {
				NameFriendly = "Prescriber : " + model.Prescriber.NameFriendly.Trim ()
			};
			var medLabel = new StringLabel () {
				NameFriendly = "Medicine : " + model.Medicine.MedicineName.Trim(),
			};
			var rAmountLabel = new StringLabel () {
				NameFriendly = "Refill Amount : " + am.ToString(),
			};
			var rQuantLabel = new StringLabel () {
				NameFriendly = "Refill Quantity : " + aq.ToString(),
			};

			var lr = new List<StringLabel> (){ dLabel, pLabel,presLabel,medLabel, rAmountLabel, rQuantLabel};
			listView.ItemsSource = lr;
		}
	}
}

