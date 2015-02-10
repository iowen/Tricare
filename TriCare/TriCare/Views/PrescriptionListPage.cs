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
			Title = "Prescriptions";
			prescriptionList = new List<PrescriptionModel> ();
			listView = new ListView ();
			var pRepo = new PrescriptionRepo();
			listView.BackgroundColor = Color.Transparent;
			prescriptionList = pRepo.GetPrescriptionsForPrescriber(int.Parse(App.Token));
			listView.ItemsSource = prescriptionList;		
			listView.ItemTemplate = new DataTemplate 
					(typeof (PrescriptionListCell));
			listView.ItemSelected  += async (sender, e) => {
				if (e.SelectedItem == null)
					return;
                var selected = (PrescriptionModel)e.SelectedItem;
				var prescriptionPage = new ViewPrescriptionPage(selected.PrescriptionId);
				await App.np.PushAsync(prescriptionPage);
				listView.SelectedItem = null;
			};
			SearchBar searchBar = new SearchBar
			{

			};
			searchBar.SearchButtonPressed += OnSearchBarButtonPressed;
			listView.HasUnevenRows = true;
			searchBar.TextChanged += OnSearchBarTextChanged;
			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{
					Text="Prescriptions", 
					Font=Font.SystemFontOfSize (NamedSize.Large)});
			}
			layout.Children.Add (searchBar);
			layout.Children.Add (new StackLayout{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
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


		}
		public void OnSearchBarTextChanged(object sender, EventArgs args)
		{
			var sb = sender as SearchBar;
			if(String.IsNullOrWhiteSpace(sb.Text))
				listView.ItemsSource = prescriptionList;
		}

		public void OnSearchBarButtonPressed(object sender, EventArgs args)
		{
			// Get the search text.
			SearchBar searchBar = (SearchBar)sender;
			string searchText = searchBar.Text;
			DateTime dateInput;
			if (!string.IsNullOrWhiteSpace (searchText.Trim ())) {
				if (DateTime.TryParse (searchText.Trim (), out dateInput)) {
					var result1 = prescriptionList.Where (a => a.Created.Date.Equals(dateInput.Date)).ToList ();
					listView.ItemsSource = result1;
					return;
				}
				var result = prescriptionList.Where (a => a.MedicineNameFriendly.ToLower().Contains (searchText.ToLower()) || a.PatientNameFriendly.ToLower().Contains (searchText.ToLower())).ToList ();
				listView.ItemsSource = result;
				return;
			} else {
				listView.ItemsSource = prescriptionList;
			}
		}
    }
}
