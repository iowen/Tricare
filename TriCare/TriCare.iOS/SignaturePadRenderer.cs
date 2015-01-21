using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TriCare;
using TriCare.iOS;
using TriCare.Utilities;
using TriCare.Views;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

using NativeView = SignaturePad.SignaturePadView;

[assembly: ExportRenderer(typeof(TriCare.Views.SignaturePadView), typeof(SignaturePadRenderer))]

namespace TriCare.iOS
{
	public class SignaturePadRenderer : ViewRenderer<TriCare.Views.SignaturePadView, NativeView> {

		private MemoryStream ConvertStreamToMemoryStream(Stream input)
		{
			var output = new MemoryStream();
			byte[] buffer = new byte[16*1024];
			int read;
			while((read = input.Read (buffer, 0, buffer.Length)) > 0)
				output.Write (buffer, 0, read);
			return output;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<SignaturePadView> e) {
			base.OnElementChanged(e);

			if (e.OldElement != null || this.Element == null)
				return;

			var view = new NativeView();
			var el = e.NewElement;
			view.BackgroundColor = UIColor.Black;
			if (!String.IsNullOrWhiteSpace(el.CaptionText))
				view.Caption.Text = el.CaptionText;

			if (el.CaptionTextColor != Color.Default)
				view.Caption.TextColor = el.CaptionTextColor.ToUIColor();

			if (!String.IsNullOrWhiteSpace(el.ClearText))
				view.ClearLabel.SetTitle("Clear", UIControlState.Normal);

			if (el.ClearTextColor != Color.Default)
				view.ClearLabel.SetTitleColor(el.ClearTextColor.ToUIColor(), UIControlState.Normal);

			if (!String.IsNullOrWhiteSpace(el.PromptText))
				view.SignaturePrompt.Text = el.PromptText;

			if (el.PromptTextColor != Color.Default)
				view.SignaturePrompt.TextColor = el.PromptTextColor.ToUIColor();

			if (el.SignatureLineColor != Color.Default)
				view.SignatureLineColor = el.SignatureLineColor.ToUIColor();

			if (el.StrokeColor != Color.Default)
				view.StrokeColor = el.StrokeColor.ToUIColor();

			if (el.StrokeWidth > 0)
				view.StrokeWidth = (float)el.StrokeWidth;

			this.Element.SetInternals(
				imgFormat => imgFormat == ImageFormatType.Jpg
				? ConvertStreamToMemoryStream(view.GetImage().AsJPEG().AsStream())
				: ConvertStreamToMemoryStream(view.GetImage().AsPNG().AsStream()) ,
				() => view.Points.Select(x => new DrawPoint(x.X, x.Y)), 
				x => view.LoadPoints(x.Select(y => new System.Drawing.PointF(y.X, y.Y)).ToArray()),
				() => view.IsBlank
			);

			this.SetNativeControl(view);
		}


		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
			base.OnElementPropertyChanged(sender, e);

			if (this.Element == null || this.Control == null)
				return;

			var el = this.Element;
			if (e.PropertyName == SignaturePadView.CaptionTextProperty.PropertyName)
				this.Control.Caption.Text = el.CaptionText;

			else if (e.PropertyName == SignaturePadView.CaptionTextColorProperty.PropertyName)
				this.Control.Caption.TextColor = el.CaptionTextColor.ToUIColor();

			else if (e.PropertyName == SignaturePadView.ClearTextProperty.PropertyName)
				this.Control.ClearLabel.SetTitle(el.ClearText, UIControlState.Normal);

			else if (e.PropertyName == SignaturePadView.ClearTextColorProperty.PropertyName)
				this.Control.ClearLabel.SetTitleColor(el.ClearTextColor.ToUIColor(), UIControlState.Normal);

			else if (e.PropertyName == SignaturePadView.PromptTextProperty.PropertyName)
				this.Control.SignaturePrompt.Text = el.PromptText;

			else if (e.PropertyName == SignaturePadView.PromptTextColorProperty.PropertyName)
				this.Control.SignaturePrompt.TextColor = el.PromptTextColor.ToUIColor();

			else if (e.PropertyName == SignaturePadView.SignatureLineColorProperty.PropertyName)
				this.Control.SignatureLineColor = el.SignatureLineColor.ToUIColor();

			else if (e.PropertyName == SignaturePadView.StrokeColorProperty.PropertyName)
				this.Control.StrokeColor = el.StrokeColor.ToUIColor();

			else if (e.PropertyName == SignaturePadView.StrokeWidthProperty.PropertyName)
				this.Control.StrokeWidth = (float)el.StrokeWidth;
		}
	}
}

