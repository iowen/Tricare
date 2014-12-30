using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriCare.Utilities
{
    public abstract class BaseSignatureService : ISignatureService
    {
        public SignaturePadConfiguration Configuration { get; private set; }

        protected BaseSignatureService()
        {
            this.Configuration = new SignaturePadConfiguration
            {
                ImageType = ImageFormatType.Png,
                MainBackgroundColor = Color.FromHex("#FFFFFF"),
                CaptionTextColor = Color.Black,
                ClearTextColor = Color.Black,
                PromptTextColor = Color.White,
                StrokeColor = Color.Black,
                StrokeWidth = 2f,
                SignatureBackgroundColor = Color.White,
                SignatureLineColor = Color.Black,

                SaveText = "Save",
                CancelText = "Cancel",
                ClearText = "Clear",
                PromptText = "",
                CaptionText = "Please Sign Here"
            };
        }


        public abstract void Request(Action<SignatureResult> onResult);
        //public abstract void Load(IEnumerable<DrawPoint> points);
    }
}
