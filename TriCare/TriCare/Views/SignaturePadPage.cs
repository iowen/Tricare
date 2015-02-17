using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Utilities;
using TriCare.Models;
using TriCare.Data;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class SignaturePadPage : ContentPage
    {

        private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.png";
        private readonly ISignatureService signatureService;
        private readonly IFileSystem fileSystem;
        private SignaturePadView curView;
		private Button saveButton;
		private Label rLabel;
		private ActivityIndicator indi;

        public SignaturePadPage(ISignatureService signatureService,IFileSystem fileSystem)
        {
			this.BackgroundColor = Color.White;
            this.signatureService = signatureService;
			var overlay = new AbsoluteLayout();
			var content = new StackLayout();
			indi = new ActivityIndicator();
            //this.dialogs = dialogs;
            this.fileSystem = fileSystem;

			rLabel = new Label {
				Text = "Rotate to Sign",
				TextColor = Color.Red,
				FontSize = 24,
				IsVisible = false,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				WidthRequest= 200.0d

		};
			curView = new SignaturePadView {  
				CaptionText = " ",
				CaptionTextColor = Color.Blue,
				ClearText = "Clear",
				ClearTextColor = Color.Red,
				PromptText = "",
				PromptTextColor = Color.Blue,
				SignatureLineColor = Color.Gray,
				StrokeColor = Color.White,
				StrokeWidth = 2,
			};

			saveButton = new Button { Text = "Sign & Send" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            saveButton.Clicked += saveButton_Clicked;
		//	if (Device.OS == TargetPlatform.iOS) {		
//            Grid grid = new Grid
//            {
//				VerticalOptions = LayoutOptions.FillAndExpand,
//				HorizontalOptions = LayoutOptions.FillAndExpand,
//                RowDefinitions = 
//				{
//					new RowDefinition { Height = new GridLength(200, GridUnitType.Absolute)},
//					new RowDefinition { Height = new GridLength(60, GridUnitType.Absolute) },
//				},
//                ColumnDefinitions = 
//				{
//					new ColumnDefinition { Width = GridLength.Auto},
//				}
//            };
//				curView.HeightRequest = grid.RowDefinitions [0].Height.Value;
//
//	
//			grid.Children.Add (rLabel);
//            grid.Children.Add(curView);
//			Grid.SetRow (rLabel, 1);
//            Grid.SetRow(curView, 0);
//
//            grid.Children.Add(saveButton);
//            Grid.SetRow(saveButton, 0);
//
			curView.HeightRequest = 200;
            content =  new StackLayout
            {
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20),
                Children = {
					rLabel,curView,saveButton
                    
                }
            
        };
			AbsoluteLayout.SetLayoutFlags(content, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(content, new Rectangle(0f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			AbsoluteLayout.SetLayoutFlags(indi, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(indi, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			overlay.Children.Add(content);
			overlay.Children.Add(indi);
			Content = new StackLayout () {
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {overlay}
			};
			if (Width < Height) {
				// Orientation got changed! Do your changes here
				rLabel.IsVisible = true;
				curView.IsVisible = false;
				saveButton.IsVisible = false;
			}
         
        }
		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);

			if (width > height) {
				// Orientation got changed! Do your changes here
				curView.IsVisible = true;
				saveButton.IsVisible = true;
				rLabel.IsVisible = false;
				rLabel.WidthRequest = 0;
					curView.WidthRequest = width - 40;
			} else {
				curView.IsVisible = false;
				saveButton.IsVisible = false;
				rLabel.IsVisible = true;
				// Orientation got changed! Do your changes here

					curView.WidthRequest = 0;		
				rLabel.WidthRequest = 200;

			}

		}
        private async void saveButton_Clicked(object sender, EventArgs e)
        {
			indi.IsRunning = true;
			saveButton.IsEnabled = false;
			curView.IsEnabled = false;
			if(!App.IsConnected())
			{
				await DisplayAlert ("Error", "Prescriptions cannot be created without an internet connection.", "OK", "close");
				indi.IsRunning = false;
				saveButton.IsEnabled = true;
				curView.IsEnabled = true;
				return;
			}
            var fileName = String.Format(FILE_FORMAT, DateTime.Now);
            IFile file = null;
			byte[] bytes = curView.GetImage(ImageFormatType.Png).ToArray();

			var ingList = new List<MedicineIngredientForPrescriptionModel> ();
			foreach (var i in App.CurrentPrescription.Medicine.Ingredients) 
			{
				ingList.Add (new MedicineIngredientForPrescriptionModel{IngredientId = i.IngredientId, Percentage = i.Percentage,PrescriptionMedicineIngredientId = 0 });
			}
			var presc = new CreatePrescriptionModel 
			{ 
				PrescriptionId = 0,
				PrescriptionMedicineId = 0,
				PrescriptionRefillId = 0,
				PrescriberId =App.CurrentPrescription.Prescriber.PrescriberId,
				PatientId = App.CurrentPrescription.Patient.PatientId,
				MedicineId = App.CurrentPrescription.Medicine.MedicineId,
				Ingredients = ingList,
				Created = DateTime.Now,
				RefillAmount = App.CurrentPrescription.Refill.Amount,
				RefillQuantity = App.CurrentPrescription.Refill.Quantity
			};
			var rep = new PrescriptionRepo ();
			var a = await rep.AddPrescription (presc, bytes);
			indi.IsRunning = false;
			if (a == "success") {
				var Command = new Command(async o => {
					await DisplayAlert ("Success", "Prescription was sucessfully created.", "OK", "close");
					await App.np.PopToRootAsync(false);
					await App.np.PushAsync (new HomePage ());
				});
				Command.Execute(new []{"run"});
			}
			else{
					var Command = new Command(async o => {
					saveButton.IsEnabled = true;
					curView.IsEnabled = true;
						await DisplayAlert ("Failure", "An Error Occured. Please Try Again.", "OK", "close");
					});
					Command.Execute(new []{"run"});
				}
        }
			
    }
}
