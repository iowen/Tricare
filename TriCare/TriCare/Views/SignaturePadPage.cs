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
        public SignaturePadPage(ISignatureService signatureService,IFileSystem fileSystem)
        {
			this.BackgroundImage = "tricareBG.png";

            this.signatureService = signatureService;
            //this.dialogs = dialogs;
            this.fileSystem = fileSystem;
            //this.fileViewer = fileViewer;

       //     this.Configure = new Command(() => App.NavigateTo<SignaturePadConfigViewModel>());
          //  this.Create = new Command(this.OnCreate);
          //  this.List = new ObservableList<Signature>();
			rLabel = new Label {
				Text = "Rotate to Sign",
				IsVisible = false
			};
			curView = new SignaturePadView {  
				CaptionText = " ",
				CaptionTextColor = Color.Blue,
				ClearText = "Clear",
				ClearTextColor = Color.Red,
				PromptText = "",
				PromptTextColor = Color.Blue,
				SignatureLineColor = Color.Blue,
				StrokeColor = Color.Black,
				StrokeWidth = 2,
			};
			saveButton = new Button { Text = "Sign & Send" , BackgroundColor = Color.FromRgba(128, 128, 128, 128),TextColor = Color.White};
            saveButton.Clicked += saveButton_Clicked;
		//	if (Device.OS == TargetPlatform.iOS) {		
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
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
            Grid.SetRow(curView, 0);

            grid.Children.Add(saveButton);
            Grid.SetRow(saveButton, 1);

		
            Content =  new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20),
                Children = {
					grid
                    
                }
            
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
				if (Device.OS == TargetPlatform.iOS) {		
					curView.WidthRequest = 440;
				}
				if (Device.OS == TargetPlatform.Android) {		
					curView.WidthRequest = 600;

				}
			} else {
				curView.IsVisible = false;
				saveButton.IsVisible = false;
				rLabel.IsVisible = true;
				// Orientation got changed! Do your changes here
				if (Device.OS == TargetPlatform.iOS) {		
					curView.WidthRequest = 0;		
				}
				if (Device.OS == TargetPlatform.Android) {		
					curView.WidthRequest = 0;
				//	curView.RelScaleTo(300);

				}
			}

		}
        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            var fileName = String.Format(FILE_FORMAT, DateTime.Now);
            IFile file = null;
			using (var ms = curView.GetImage(ImageFormatType.Png))
            {
                var bytes = ms.ToArray();
                file = this.fileSystem.AppData.CreateFile(fileName);
                using (var fs = file.OpenWrite())
                    fs.Write(bytes, 0, bytes.Length);
				DisplayActionSheet ("fname", file.FullName, "close");
            }
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
				PrescriberId =App.CurrentPrescription.Prescriber.PrescriberId,
				PatientId = App.CurrentPrescription.Patient.PatientId,
				MedicineId = App.CurrentPrescription.Medicine.MedicineId,
				Ingredients = ingList,
				Created = DateTime.Now,
				RefillAmount = App.CurrentPrescription.Refill.Amount,
				RefillQuantity = App.CurrentPrescription.Refill.Quantity
			};
			var rep = new PrescriptionRepo ();
			var a = await rep.AddPrescription (presc);
			DisplayAlert ("ret", a, "close");
		//	this.Navigation.PushAsync (new HomePage ());
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
