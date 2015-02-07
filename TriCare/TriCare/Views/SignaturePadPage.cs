﻿using System;
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
            //this.fileViewer = fileViewer;

       //     this.Configure = new Command(() => App.NavigateTo<SignaturePadConfigViewModel>());
          //  this.Create = new Command(this.OnCreate);
          //  this.List = new ObservableList<Signature>();
			rLabel = new Label {
				Text = "Rotate to Sign",
				TextColor = Color.Red,
				FontSize = 24,
				IsVisible = false,

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
            Grid grid = new Grid
            {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
                RowDefinitions = 
				{
					new RowDefinition { Height = new GridLength(200, GridUnitType.Absolute)},
					new RowDefinition { Height = new GridLength(60, GridUnitType.Absolute) },
				},
                ColumnDefinitions = 
				{
					new ColumnDefinition { Width = GridLength.Auto },
				}
            };
				curView.HeightRequest = grid.RowDefinitions [0].Height.Value;

	
			grid.Children.Add (rLabel);
            grid.Children.Add(curView);
			Grid.SetRow (rLabel, 1);
            Grid.SetRow(curView, 0);
            grid.Children.Add(saveButton);
            Grid.SetRow(saveButton, 1);

		
            content =  new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(20),
                Children = {
					grid
                    
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
			/*}else if (Device.OS == TargetPlatform.Android){
				Grid grid = new Grid
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					RowDefinitions = 
					{
						new RowDefinition { Height =GridLength.Auto},
						new RowDefinition { Height = new GridLength(60, GridUnitType.Absolute) },
					},
					ColumnDefinitions = 
					{
						new ColumnDefinition { Width = GridLength.Auto },
					}
					};


				grid.Children.Add(curView);
				Grid.SetRow(curView, 0);

				grid.Children.Add(saveButton);
				Grid.SetRow(saveButton, 1);


				Content =  new StackLayout
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					Padding = new Thickness(20),
					Children = {
						grid,

					}

				};
			}*/
         
        }
		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);

			if (width > height) {
				// Orientation got changed! Do your changes here
				curView.IsVisible = true;
				saveButton.IsVisible = true;
				rLabel.IsVisible = false;

					curView.WidthRequest = width - 40;

			} else {
				curView.IsVisible = false;
				saveButton.IsVisible = false;
				rLabel.IsVisible = true;
				// Orientation got changed! Do your changes here

					curView.WidthRequest = 0;		

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
			//using (var ms = curView.GetImage(ImageFormatType.Png))
      //      {
      //          bytes = ms.ToArray();
//                file = this.fileSystem.AppData.CreateFile(fileName);
//                using (var fs = file.OpenWrite())
//                    fs.Write(bytes, 0, bytes.Length);
				//DisplayActionSheet ("fname", file.FullName, "close");
      //     }
			// make pdf Send fax and email 
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
        }


    //    public override void OnAppearing()
      //  {
            //this.List.Clear();

            //var signatures = this.fileSystem
            //    .AppData
            //    .Files
            //    .Select(x => new Signature
            //    {
            //        FileName = x.Name,
            //        FilePath = x.FullName,
            //        FileSize = x.Length
            //    })
            //    .ToList();

            //this.List.AddRange(signatures);
          //  this.NoData = false;// !this.List.Any();
        //}

       // public ObservableList<Signature> List { get; private set; }

        private bool noData;
        public bool NoData
        {
            get { return this.noData; }
            set { }//this.SetProperty(ref this.noData, value); }
        }


        //public ICommand Configure { get; private set; }
        //public ICommand Create { get; private set; }


        private void OnCreate()
        {
        }


        //private Command<Signature> selectCmd;
        //public Command<Signature> Select
        //{
        //    get
        //    {
        //        this.selectCmd = this.selectCmd ?? new Command<Signature>(s =>
        //            this.dialogs.ActionSheet(new ActionSheetConfig()
        //                .Add("View", () =>
        //                {
        //                    if (!this.fileViewer.Open(s.FilePath))
        //                        this.dialogs.Alert(String.Format("Could not open file {0}", s.FileName));
        //                })
        //                .Add("Delete", async () =>
        //                {
        //                    var r = await this.dialogs.ConfirmAsync(String.Format("Are you sure you want to delete {0}", s.FileName));
        //                    if (!r)
        //                        return;

        //                    var file = this.fileSystem.GetFile(s.FilePath);
        //                    file.Delete();
        //                    this.List.Remove(s);
        //                    this.NoData = !this.List.Any();
        //                })
        //                .Add("Cancel")
        //            )
        //        );
        //        return this.selectCmd;
        //    }
        //}
    }
}
