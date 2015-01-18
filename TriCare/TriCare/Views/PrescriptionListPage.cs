using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Data;
using TriCare.Models;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class PrescriptionListPage : ContentPage
    {
		ListView listView;
		List<PrescriptionModel> prescriptionList;
		public PrescriptionListPage (bool isDuringPrescription = false)
		{
			this.BackgroundColor = Color.White;
			App.EnableLogout ();
			Title = "My Patients";
			prescriptionList = new List<PrescriptionModel> ();
			listView = new ListView ();
			var pRepo = new PrescriptionRepo();
			prescriptionList = pRepo.GetPrescriptionsForPrescriber(int.Parse(App.Token));
			listView.BackgroundColor = Color.Transparent;
			listView.ItemsSource = prescriptionList;
			listView.ItemTemplate = new DataTemplate 
					(typeof (PrescriptionListCell));
			listView.ItemSelected += (sender, e) => {
                var selected = (PrescriptionModel)e.SelectedItem;
                    var prescriptionPage = new ViewPrescriptionPage(selected);
				Navigation.PushAsync(prescriptionPage);
                
			};
			SearchBar searchBar = new SearchBar
			{
				
			};
			searchBar.SearchButtonPressed += OnSearchBarButtonPressed;
			listView.HasUnevenRows = true;

			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{
					Text="Prescriptions", 
					Font=Font.SystemFontOfSize (NamedSize.Large)});
			}
			layout.Children.Add (searchBar);
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
			listView.ItemsSource = prescriptionList;
		}
		public void OnSearchBarButtonPressed(object sender, EventArgs args)
		{
			// Get the search text.
			SearchBar searchBar = (SearchBar)sender;
			string searchText = searchBar.Text;
			if (!string.IsNullOrWhiteSpace (searchText.Trim ())) {
				var result = prescriptionList.Where (a => a.MedicineNameFriendly.ToLower().Contains (searchText) || a.PatientNameFriendly.ToLower().Contains (searchText)).ToList ();
				listView.ItemsSource = result;
			} else {
				listView.ItemsSource = prescriptionList;
			}
		}
    }
}
