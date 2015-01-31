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
using TriCare.Data;
using TriCare;
using TriCare.Droid;
using Xamarin.Forms;
using System.ComponentModel;
using TriCare.Utilities;
using System.IO;

using System.Drawing;
using Android.Graphics;

[assembly: ExportRenderer(typeof(PhoneNumberEntry), typeof(AndroidEntryRenderer))]
namespace TriCare.Droid
{
	public class AndroidEntryRenderer : EntryRenderer
    {
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);
			if (Control != null) {   // perform initial setup
				// do whatever you want to the UITextField here!
				Control.AfterTextChanged += (sender, el) => {
					Control.SetSelection(Control.Text.Length);

				};
			}
		}
							
    }
}