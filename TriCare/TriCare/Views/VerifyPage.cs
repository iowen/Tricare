﻿using System;
using System.Collections.Generic;
using TriCare.Data;
using TriCare.Models;
using TriCare.Utilities;
using TriCare.Views;
using Xamarin.Forms;

namespace TriCare
{
	public class VerifyPage :ContentPage
	{
		ListView listView;

		public VerifyPage ()
		{
			this.BackgroundImage = "tricareBG.png";
			App.EnableLogout ();
			Title = "Verify";

			var confirmButton = new Button { Text = "Confirm", BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White  };
			confirmButton.Clicked += (sender, e) =>
			{
				//  this.Navigation.PushAsync(new RegisterPage());
				var sigserv = DependencyService.Get<ISignatureService>();
				var fileSys = DependencyService.Get<IFileSystem>();
				this.Navigation.PushAsync(new SignaturePadPage(sigserv, fileSys));

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
					Text="Formula", 
					Font=Font.SystemFontOfSize (NamedSize.Large)});
			}

			layout.Children.Add (new Label { TextColor = Color.White, Text = "Tap an field for more info." });
			layout.Children.Add (confirmButton);
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
				NameFriendly = "Patient : " + App.CurrentPrescription.Patient.NameFriendly.Trim ()
			};

			var presLabel = new StringLabel () {
				NameFriendly = "Prescriber : " + App.CurrentPrescription.Prescriber.NameFriendly.Trim ()
			};
			var medLabel = new StringLabel () {
				NameFriendly = "Medicine : " + App.CurrentPrescription.Medicine.MedicineName.Trim(),
			};

			var lr = new List<StringLabel> (){ pLabel,presLabel,medLabel};
			listView.ItemsSource = lr;
		}
	}
}

