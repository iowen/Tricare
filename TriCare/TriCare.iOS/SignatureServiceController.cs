﻿using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Xamarin.Forms.Platform.iOS;
namespace TriCare.iOS
{

	public class SignatureServiceController : UIViewController {

		private SignatureServiceView view;

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


		public override void LoadView() {
			base.LoadView();

			this.view = new SignatureServiceView();
			this.View = this.view;
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

			this.view.BackgroundColor = this.config.MainBackgroundColor.ToUIColor();
			this.view.Signature.BackgroundColor = this.config.SignatureBackgroundColor.ToUIColor();
			this.view.Signature.Caption.TextColor = this.config.CaptionTextColor.ToUIColor();
			this.view.Signature.Caption.Text = this.config.CaptionText;
			this.view.Signature.ClearLabel.SetTitle(this.config.ClearText, UIControlState.Normal);
			this.view.Signature.ClearLabel.SetTitleColor(this.config.ClearTextColor.ToUIColor(), UIControlState.Normal);
			this.view.Signature.SignatureLineColor = this.config.SignatureLineColor.ToUIColor();
			this.view.Signature.SignaturePrompt.Text = this.config.PromptText;
			this.view.Signature.SignaturePrompt.TextColor = this.config.PromptTextColor.ToUIColor();
			this.view.Signature.StrokeColor = this.config.StrokeColor.ToUIColor();
			this.view.Signature.StrokeWidth = this.config.StrokeWidth;
			this.view.Signature.Layer.ShadowOffset = new SizeF(0, 0);
			this.view.Signature.Layer.ShadowOpacity = 1f;

			if (this.onResult == null) {
				this.view.CancelButton.Hidden = true;
				this.view.SaveButton.Hidden = true;
				this.view.Signature.ClearLabel.Hidden = true;
				this.view.Signature.LoadPoints(this.points.Select(x => new PointF { X = x.X, Y = x.Y }).ToArray());
			}
			else {
				this.view.SaveButton.SetTitle(this.config.SaveText, UIControlState.Normal);
				this.view.SaveButton.TouchUpInside += (sender, args) => {
					if (this.view.Signature.IsBlank)
						return;

					var points = this.view
						.Signature
						.Points
						.Select(x => new DrawPoint(x.X, x.Y));

					using (var image = this.view.Signature.GetImage()) {
						using (var stream = GetImageStream(image, this.config.ImageType)) {
							this.DismissViewController(true, null);
							this.onResult(new SignatureResult(false, stream, points));
						}
					}
				};

				this.view.CancelButton.SetTitle(this.config.CancelText, UIControlState.Normal);
				this.view.CancelButton.TouchUpInside += (sender, args) => {
					this.DismissViewController(true, null);
					this.onResult(new SignatureResult(true, null, null));
				};
			}
		}


		public void LoadSignature(params PointF[] points) {
			this.view.Signature.LoadPoints(points);
		}


		//        public override void TouchesBegan(NSSet touches, UIEvent evt) {
		//            base.TouchesBegan(touches, evt);
		//            if (this.onResult == null)
		//                this.DismissViewController(true, null);
		//        }


		private static Stream GetImageStream(UIImage image, ImageFormatType formatType) {
			if (formatType == ImageFormatType.Jpg)
				return image.AsJPEG().AsStream();

			return image.AsPNG().AsStream();
		}
	}
}

