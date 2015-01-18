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
using TriCare.Views;
using TriCare;
using TriCare.Droid;
using Xamarin.Forms;
using System.ComponentModel;
using TriCare.Utilities;
using System.IO;
using Color = Xamarin.Forms.Color;
using NativeView = SignaturePad.SignaturePadView;
using System.Drawing;
using Android.Graphics;

[assembly: ExportRenderer(typeof(TriCare.Views.ExtendedTabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace TriCare.Droid
{
	public class ExtendedTabbedPageRenderer : TabbedRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
		{
			base.OnElementChanged(e);


		}
    }
}