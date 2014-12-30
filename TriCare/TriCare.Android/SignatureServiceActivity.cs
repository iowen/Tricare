using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using NativeView = global::SignaturePad.SignaturePadView;
using TriCare.Utilities;
using System.IO;
using Android.Graphics;
using SignaturePad;

namespace TriCare.Droid
{

    [Activity]
    public class SignatureServiceActivity : Activity
    {
        private NativeView signatureView;
        private Button btnSave;
        private Button btnCancel;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var signature = new SignaturePadView(this);

            this.SetContentView(Resource.Layout.SignaturePad);

            var rootView = this.FindViewById<RelativeLayout>(Resource.Id.rootView);
            this.signatureView = this.FindViewById<NativeView>(Resource.Id.signatureView);
            this.btnSave = this.FindViewById<Button>(Resource.Id.btnSave);
            this.btnCancel = this.FindViewById<Button>(Resource.Id.btnCancel);

            var cfg = SignatureService.CurrentConfig;

            rootView.SetBackgroundColor(cfg.MainBackgroundColor.ToAndroid());
            this.signatureView.BackgroundColor = Color.White;
            this.signatureView.Caption.Text = cfg.CaptionText;
            this.signatureView.Caption.SetTextColor(cfg.CaptionTextColor.ToAndroid());
            this.signatureView.ClearLabel.Text = cfg.ClearText;
            this.signatureView.ClearLabel.SetTextColor(cfg.ClearTextColor.ToAndroid());
            this.signatureView.SignatureLineColor = cfg.SignatureLineColor.ToAndroid();
            this.signatureView.SignaturePrompt.Text = cfg.PromptText;
            this.signatureView.SignaturePrompt.SetTextColor(cfg.PromptTextColor.ToAndroid());
            this.signatureView.StrokeColor = cfg.StrokeColor.ToAndroid();
            this.signatureView.StrokeWidth = cfg.StrokeWidth;

         /*   this.btnSave.Text = "Save";
            this.btnCancel.Text = "Cancel";
            this.btnSave.Visibility = ViewStates.Visible;
            this.btnSave.Visibility = ViewStates.Visible;*/
            //if (SignatureService.CurrentConfig. != null)
            //{
            //    this.btnSave.Visibility = ViewStates.Gone;
            //    this.btnCancel.Visibility = ViewStates.Gone;
            //    //                this.signatureView.Enabled = false;
            //    this.signatureView.LoadPoints(
            //        SignatureService
            //            .CurrentPoints
            //            .Select(x => new PointF { X = x.X, Y = x.Y })
            //            .ToArray()
            //    );
            //}
        }


        protected override void OnResume()
        {
            base.OnResume();
            this.btnSave.Click += this.OnSave;
            this.btnCancel.Click += this.OnCancel;
        }


        protected override void OnPause()
        {
            base.OnPause();
            this.btnSave.Click -= this.OnSave;
            this.btnCancel.Click -= this.OnCancel;
        }


        private void OnSave(object sender, EventArgs args)
        {
            if (this.signatureView.IsBlank)
                return;

            var points = this.signatureView
                .Points
                .Select(x => new DrawPoint(x.X, x.Y));

            using (var image = this.signatureView.GetImage())
            {
                using (var stream = new MemoryStream())
                {
                    var format = SignatureService.CurrentConfig.ImageType == TriCare.Utilities.ImageFormatType.Png
                        ? Android.Graphics.Bitmap.CompressFormat.Png
                        : Android.Graphics.Bitmap.CompressFormat.Jpeg;
                    image.Compress(format, 100, stream);
                    SignatureService.OnResult(new SignatureResult(false, stream, points));
                    this.Finish();
                }
            }
        }


        private void OnCancel(object sender, EventArgs args)
        {
            SignatureService.OnResult(new SignatureResult(true, null, null));
            this.Finish();
        }
    }
}