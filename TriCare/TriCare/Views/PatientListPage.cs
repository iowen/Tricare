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
	public class PatientListPage : MasterDetailPage
    {
		ListView listView;
		List<Patient> patientList;
		ContentPage cPage;
		public PatientListPage (bool isDuringPrescription = false)
		{
			cPage = new ContentPage();
			this.BackgroundColor = Color.White;
			Button filter = new Button (){ Image = "options.png" , BackgroundColor=Color.White,HorizontalOptions = LayoutOptions.StartAndExpand};
			filter.Clicked += (sender, e) => {
				this.IsPresented = !this.IsPresented;
			};

			Title = "Patients";
			patientList = new List<Patient> ();
			listView = new ListView ();
			var pRepo = new PatientRepo();
			patientList = pRepo.GetAllPatientsForPrescriber(int.Parse(App.Token));
			listView.BackgroundColor = Color.Transparent;
			listView.ItemsSource = patientList;
			listView.ItemTemplate = new DataTemplate 
					(typeof (PatientCell));
			listView.ItemSelected += async (sender, e) => {
				if (e.SelectedItem == null)
					return;
                var selected = (Patient)e.SelectedItem;
                if (!isDuringPrescription)
                {
                    var patientPage = new PatientPage(selected);
					await App.np.PushAsync(patientPage);
                }
                else
                {
					await App.np.PushAsync(new PatientPage(selected, true));
                }
				listView.SelectedItem = null;
			};
			SearchBar searchBar = new SearchBar
			{

			};
			searchBar.SearchButtonPressed += OnSearchBarButtonPressed;
			searchBar.TextChanged += OnTextChanged;
			var sblayout = new StackLayout (){ Orientation = StackOrientation.Horizontal };
			sblayout.Children.Add (filter);
			sblayout.Children.Add (searchBar);
			// Accomodate iPhone status bar.
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
			var layout = new StackLayout(){HorizontalOptions =LayoutOptions.StartAndExpand};
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{
					Text="Patient", 
					Font=Font.SystemFontOfSize (NamedSize.Large)});
			}
			layout.Children.Add (filter);
			layout.Children.Add (searchBar);
			layout.Children.Add(new StackLayout{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					new ScrollView
					{
						Content = listView,
						VerticalOptions = LayoutOptions.FillAndExpand,
						HorizontalOptions = LayoutOptions.FillAndExpand
					}
				}
			}
			);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			layout.HorizontalOptions = LayoutOptions.FillAndExpand;
			cPage.Content = layout;
			cPage.BackgroundColor = Color.White;
			this.Detail = cPage;
			this.Master = MenuPage();
		
		}

		protected override void OnAppearing ()
		{

			base.OnAppearing ();
			listView.ItemsSource = patientList;
		}
		public void OnSearchBarButtonPressed(object sender, EventArgs args)
		{
			// Get the search text.
			SearchBar searchBar = (SearchBar)sender;
			string searchText = searchBar.Text;
			if (!string.IsNullOrWhiteSpace (searchText.Trim ())) {
				var result = patientList.Where (a => a.FirstName.ToLower ().Contains (searchText.ToLower()) || a.LastName.ToLower().Contains (searchText.ToLower())).ToList ();
				listView.ItemsSource = result;
			} else {
				listView.ItemsSource = patientList;
			}
		}
		public ContentPage MenuPage ()
		{
			ContentPage mPage = new ContentPage ();
			Icon = "settings.png";
			mPage.Title = "menu"; // The Title property must be set.
			BackgroundColor = Color.FromHex ("333333");
			var Menu = new ListView ();

			var menuLabel = new ContentView {
				Padding = new Thickness (10, 36, 0, 5),
				Content = new Label {
					TextColor = Color.FromHex ("AAAAAA"),
					Text = "MENU", 
				}
			};

			var layout = new StackLayout { 
				Spacing = 0, 
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			layout.Children.Add (menuLabel);
			layout.Children.Add (Menu);

			mPage.Content = layout;
			return mPage;
		}
		public void OnTextChanged(object sender, EventArgs args)
		{
			// Get the search text.
			SearchBar searchBar = (SearchBar)sender;
			string searchText = searchBar.Text;
			if (string.IsNullOrWhiteSpace (searchText.Trim ())) {
//				var result = patientList.Where (a => a.FirstName.ToLower ().Contains (searchText) || a.LastName.ToLower().Contains (searchText)).ToList ();
//				listView.ItemsSource = result;
//			} else {
				listView.ItemsSource = patientList;
			}
		}
    }
}
