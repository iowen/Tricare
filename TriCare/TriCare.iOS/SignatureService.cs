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
using TriCare.iOS;
using TriCare.Utilities;
using Xamarin.Forms;
using System.IO;
[assembly: Dependency(typeof(SignatureService))]
namespace TriCare.Droid
{
   public class SignatureService : BaseSignatureService
    {
      
	public override void Request(Action<SignatureResult> onResult) {
			var controller = new SignatureServiceController(this.Configuration, onResult);
			this.Show(controller);
		}


		//public override void Load(IEnumerable<DrawPoint> points) {
		//    var controller = new SignatureServiceController(this.Configuration, points);
		//    this.Show(controller);
		//}


		private void Show(SignatureServiceController controller) {
			var vc = Utils.GetTopViewController();
			vc.PresentViewController(controller, true, null);
		}


    }
}