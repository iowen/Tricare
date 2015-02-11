using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TriCare.Data;
using TriCare.Models;
using TriCare.Validators;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace TriCare.Views
{
	public class TermsPage : ContentPage
	{

		public TermsPage (Prescriber prescriber)
		{
			App.IsHome = false;
			App.DisableLogout ();

			var terms = new Label (){ Text = "Terms and agreements" };
			var continueButton = new Button (){ Text = "Accept" };
			var cancelButton = new Button () { Text = "Cancel" };

			continueButton.Clicked += async (sender, e) => {
				this.IsBusy = true;
				continueButton.IsEnabled = false;
				cancelButton.IsEnabled = false;

				var prescriberRepo = new PrescriberRepo();
				var res = await prescriberRepo.AddPrescriber(prescriber);
				var resultInt = int.Parse(res.ToString());
				if (resultInt > 0)
				{
					var sRepo = new SyncRepo();
					var sModel = new SyncModel();
					sModel.SyncType = 'a';
					sModel.LastAppDataSync = sRepo.GetLastAppUpdate ();

					await sRepo.GetSyncData(sModel);
					this.IsBusy = false;
					await DisplayAlert ("Alert", "Please check your email for steps on verifying your account.", "OK", "close");
					await App.np.PopToRootAsync();
				}
				else
				{
					continueButton.IsEnabled = true;
					cancelButton.IsEnabled = true;
					this.IsBusy = false;
					await DisplayAlert("Error", "An Error Occured Please Try Again", "OK");
				}
			};

			cancelButton.Clicked += async (sender, e) => {

					await App.np.PopToRootAsync();

			};

			var sl = new StackLayout ();
			sl.Children.Add (cancelButton);
			sl.Children.Add (terms);
			sl.Children.Add (continueButton);
			sl.VerticalOptions = LayoutOptions.FillAndExpand;
			sl.HorizontalOptions = LayoutOptions.FillAndExpand;
			Content = sl;
		}
	}
}

