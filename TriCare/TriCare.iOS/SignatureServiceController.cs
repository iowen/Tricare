using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using UIKit;
using Foundation;
using Xamarin.Forms.Platform.iOS;
using TriCare;
using TriCare.iOS;
using TriCare.Utilities;
namespace TriCare.iOS
{

	public class SignatureServiceController : UIViewController {

		private SignaturePad.SignaturePadView view;

		private IEnumerable<DrawPoint> points;
		private Action<SignatureResult> onResult;
		private readonly SignaturePadConfiguration config;


		public SignatureServiceController(SignaturePadConfiguration config, Action<SignatureResult> onResult) {
			this.config = config;
			this.onResult = onResult;
		}


		public SignatureServiceController(SignaturePadConfiguration config, IEnumerable<DrawPoint> points) {
			this.config = config;
			this.points = points;
		}



		public override void ViewDidLoad() {

			base.ViewDidLoad();
			var frame = UIScreen.MainScreen.ApplicationFrame;


		

			var sbframe = UIApplication.SharedApplication.StatusBarFrame;
			var portrait = UIApplication.SharedApplication.StatusBarOrientation.HasFlag(UIDeviceOrientation.Portrait);

			var width = portrait
				? frame.Size.Width
				: frame.Size.Width - sbframe.Width;

			var height = portrait
				? frame.Size.Height - sbframe.Height 
				: frame.Size.Height;

			var x = portrait
				? 0
				: frame.Location.X + sbframe.Width;

			var y = portrait
				? frame.Location.Y + sbframe.Height
				: 0;

			frame = new CoreGraphics.CGRect(x, y, width, height);

			this.view.Frame = frame;//UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone
			//	? new RectangleF (10, 10, Bounds.Width - 20, Bounds.Height - 60)
			//	: new RectangleF (84, 84, Bounds.Width - 168, Bounds.Width / 2);

			this.view.BackgroundColor = this.config.MainBackgroundColor.ToUIColor();
			this.view.BackgroundColor = this.config.SignatureBackgroundColor.ToUIColor();
			this.view.Caption.TextColor = this.config.CaptionTextColor.ToUIColor();
			this.view.Caption.Text = this.config.CaptionText;

			this.view.ClearLabel.SetTitle(this.config.ClearText, UIControlState.Normal);
			this.view.ClearLabel.SetTitleColor(this.config.ClearTextColor.ToUIColor(), UIControlState.Normal);
			this.view.ClearLabel.TitleLabel.AdjustsFontSizeToFitWidth = true;
			this.view.SignatureLineColor = this.config.SignatureLineColor.ToUIColor();
			this.view.SignaturePrompt.Text = this.config.PromptText;
			this.view.SignaturePrompt.TextColor = this.config.PromptTextColor.ToUIColor();
			this.view.StrokeColor = this.config.StrokeColor.ToUIColor();
			this.view.StrokeWidth = this.config.StrokeWidth;
			this.view.Layer.ShadowOffset = new SizeF(0, 0);
			this.view.Layer.ShadowOpacity = 1f;
			View.AddSubview (this.view);
			if (this.onResult == null) {
			//	this.view.CancelButton.Hidden = true;
			//	this.view.SaveButton.Hidden = true;
				this.view.ClearLabel.Hidden = true;
				this.view.LoadPoints(this.points.Select(xx => new CoreGraphics.CGPoint { X = xx.X, Y = xx.Y }).ToArray());
			}
			else {
			//	this.view.SaveButton.SetTitle(this.config.SaveText, UIControlState.Normal);
			/*	this.view.SaveButton.TouchUpInside += (sender, args) => {
					if (this.view.IsBlank)
						return;

					var points = this.view
						.Points
						.Select(xx => new DrawPoint(xx.X, xx.Y));

					using (var image = this.view.GetImage()) {
						using (var stream = GetImageStream(image, this.config.ImageType)) {
							this.DismissViewController(true, null);
							this.onResult(new SignatureResult(false, stream, points));
						}
					};
				}  */

			//	this.view.CancelButton.SetTitle(this.config.CancelText, UIControlState.Normal);
			//	this.view.CancelButton.TouchUpInside += (sender, args) => {
				//	this.DismissViewController(true, null);
			//		this.onResult(new SignatureResult(true, null, null));
			//	};
			}

		}

		private static MemoryStream ConvertStreamToMemoryStream(Stream input)
		{
			var output = new MemoryStream();
			byte[] buffer = new byte[16*1024];
			int read;
			while((read = input.Read (buffer, 0, buffer.Length)) > 0)
				output.Write (buffer, 0, read);
			return output;
		}

		public void LoadSignature(params CoreGraphics.CGPoint[] points) {
			this.view.LoadPoints(points);
		}


		        public override void TouchesBegan(NSSet touches, UIEvent evt) {
		            base.TouchesBegan(touches, evt);
		            if (this.onResult == null)
		                this.DismissViewController(true, null);
		        }


		private static MemoryStream GetImageStream(UIImage image, ImageFormatType formatType) {
			if (formatType == ImageFormatType.Jpg)
				return ConvertStreamToMemoryStream(image.AsJPEG ().AsStream ());

			return ConvertStreamToMemoryStream(image.AsPNG ().AsStream ());
		}
	}
}

