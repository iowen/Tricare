using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TriCare;
using TriCare.iOS;
using TriCare.Utilities;
using TriCare.Data;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(LoginScrollView), typeof(IOSLoginScrollViewRenderer))]

namespace TriCare.iOS
{
	public class IOSLoginScrollViewRenderer : ScrollViewRenderer
	{
	
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			this.Bounces = true;
		}
	}
}

