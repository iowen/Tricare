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
			this.BackgroundImage = "tricareBG.png";
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
				var selected = (StringLabel)e.SelectedItem;

				//var patientPage = new PatientPage(selected);
				//                  Navigation.PushAsync(patientPage);

			};

			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{
					Text="View Prescription", 
					Font=Font.SystemFontOfSize (NamedSize.Large)});
			}

			layout.Children.Add (new Label { TextColor = Color.White, Text = "Tap an field for more info." });
			layout.Children.Add (downloadButton);
			layout.Children.Add (	new ScrollView
				{
					Content = listView,
					VerticalOptions = LayoutOptions.FillAndExpand
				}
			);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;


		}
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			var pLabel = new StringLabel () {
				NameFriendly = "Patient : " + model.Patient.NameFriendly.Trim ()
			};

			var presLabel = new StringLabel () {
				NameFriendly = "Prescriber : " + model.Prescriber.NameFriendly.Trim ()
			};
			var medLabel = new StringLabel () {
				NameFriendly = "Medicine : " + model.Medicine.MedicineName.Trim(),
			};

			var lr = new List<StringLabel> (){ pLabel,presLabel,medLabel};
			listView.ItemsSource = lr;
		}
	}
}
