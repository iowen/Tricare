using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms.Platform.Android;
using TriCare.Views;
using TriCare;
using TriCare.Droid;
using Xamarin.Forms;
using TriCare.Utilities;
using System.IO;

using System.Drawing;
using Android.Graphics;

[assembly: ExportRenderer(typeof(TEntry), typeof(TEntryRenderer))]
namespace TriCare.Droid
{
	public class TEntryRenderer : EntryRenderer
	{
		// Override the OnElementChanged method so we can tweak this renderer post-initial setup
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

//			if (Control != null) { 
//				// do whatever you want to the textField here!
//				Control.s
//			}
		}
	}
}

