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

[assembly: ExportRenderer(typeof(TriCare.Views.ExtendedTabbedPage), typeof(ExtendedTabbedPageRenderer))]

namespace TriCare.iOS
{
	public class ExtendedTabbedPageRenderer : TabbedRenderer {
	
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			TabBar.TintColor = MonoTouch.UIKit.UIColor.White;
			TabBar.BarTintColor = MonoTouch.UIKit.UIColor.FromRGBA(52, 63, 169, 128);
			TabBar.BackgroundColor = MonoTouch.UIKit.UIColor.Blue;

		}
	}
}

