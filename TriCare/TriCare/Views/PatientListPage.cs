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
    public class PatientListPage : ContentPage
    {
		ListView listView;
		List<Patient> patientList;
		public PatientListPage (bool isDuringPrescription = false)
		{
			this.BackgroundImage = "tricareBG.png";

			Title = "My Patients";
			patientList = new List<Patient> ();
			listView = new ListView ();
			var pRepo = new PatientRepo();
			patientList = pRepo.GetAllPatientsForPrescriber(int.Parse(App.Token));
			listView.BackgroundColor = Color.Transparent;
			listView.ItemsSource = patientList;
			listView.ItemTemplate = new DataTemplate 
					(typeof (PatientCell));
			listView.ItemSelected += (sender, e) => {
                var selected = (Patient)e.SelectedItem;
                if (!isDuringPrescription)
                {
                    var patientPage = new PatientPage(selected);
                    Navigation.PushAsync(patientPage);
                }
                else
                {
					Navigation.PushAsync(new PatientPage(selected, true));
                }
			};
			SearchBar searchBar = new SearchBar
			{
				
			};
			searchBar.SearchButtonPressed += OnSearchBarButtonPressed;


			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{
					Text="Patient", 
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

		}
		public void OnSearchBarButtonPressed(object sender, EventArgs args)
		{
			// Get the search text.
			SearchBar searchBar = (SearchBar)sender;
			string searchText = searchBar.Text;
			if (!string.IsNullOrWhiteSpace (searchText.Trim ())) {
				var result = patientList.Where (a => a.FirstName.ToLower ().Contains (searchText) || a.LastName.ToLower().Contains (searchText)).ToList ();
				listView.ItemsSource = result;
			} else {
				listView.ItemsSource = patientList;
			}
		}
    }
}
