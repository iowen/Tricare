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
using TriCare;
using TriCare.Droid;
using TriCare.Utilities;
using Xamarin.Forms;
using System.IO;
[assembly: Dependency(typeof(SignatureService))]
namespace TriCare.Droid
{
   public class SignatureService : BaseSignatureService
    {
        internal static SignaturePadConfiguration CurrentConfig { get; private set; }
        internal static Action<SignatureResult> OnResult { get; private set; }
		private MemoryStream _img;


	/*	public override MemoryStream GetCurrentImage()
		{
			return _img;
		}

		public override void SetCurrentImage(MemoryStream img)
		{
			_img = new MemoryStream ();
			img.CopyTo (_img);
		}
*/
        public override void Request(Action<SignatureResult> onResult)
        {
            //CurrentPoints = null;
            CurrentConfig = this.Configuration;
            OnResult = onResult;
            Forms.Context.StartActivity(typeof(SignatureServiceActivity));
			//F
        }

    }
}