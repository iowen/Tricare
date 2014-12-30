﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Utilities;
using Xamarin.Forms;

namespace TriCare.Views
{
    public class SignaturePadPage : ContentPage
    {

        private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.png";
        private readonly ISignatureService signatureService;
        private readonly IFileSystem fileSystem;
        private SignaturePadView curView;


        public SignaturePadPage(ISignatureService signatureService,IFileSystem fileSystem)
        {
            this.signatureService = signatureService;
            //this.dialogs = dialogs;
            this.fileSystem = fileSystem;
            //this.fileViewer = fileViewer;

       //     this.Configure = new Command(() => App.NavigateTo<SignaturePadConfigViewModel>());
          //  this.Create = new Command(this.OnCreate);
          //  this.List = new ObservableList<Signature>();

			curView = new SignaturePadView {  
				CaptionText = "Sign Here",
				CaptionTextColor = Color.Blue,
				ClearText = "Clear Me!",
				ClearTextColor = Color.Red,
				PromptText = "",
				PromptTextColor = Color.Blue,
				SignatureLineColor = Color.Aqua,
				StrokeColor = Color.Black,
				StrokeWidth = 2
			};
            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += saveButton_Clicked;
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = 
				{
					new RowDefinition { Height = GridLength.Auto },
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
         
        }

        private void saveButton_Clicked(object sender, EventArgs e)
        {
            var fileName = String.Format(FILE_FORMAT, DateTime.Now);
            IFile file = null;
			using (var ms = curView.GetImage(ImageFormatType.Png))
            {
                var bytes = ms.ToArray();
                file = this.fileSystem.AppData.CreateFile(fileName);
                using (var fs = file.OpenWrite())
                    fs.Write(bytes, 0, bytes.Length);
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
