using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriCare.Utilities;
using Xamarin.Forms;

namespace TriCare.Views
{
	public class SignaturePadView : Xamarin.Forms.View
    {
        private Func<ImageFormatType, MemoryStream> getImageFunc;
        private Func<IEnumerable<DrawPoint>> getDrawPointsFunc;
        private Action<IEnumerable<DrawPoint>> loadDrawPoints;
        private Func<bool> isBlankFunc;


        public MemoryStream GetImage(ImageFormatType imageFormat)
        {
            return this.getImageFunc(imageFormat);
        }


        public IEnumerable<DrawPoint> GetDrawPoints()
        {
            return this.getDrawPointsFunc();
        }


        public void LoadDrawPoints(IEnumerable<DrawPoint> drawPoints)
        {
            this.loadDrawPoints(drawPoints);
        }


        public bool IsBlank
        {
            get { return this.isBlankFunc(); }
        }


        public void SetInternals(Func<ImageFormatType, MemoryStream> getImage, Func<IEnumerable<DrawPoint>> getPoints, Action<IEnumerable<DrawPoint>> loadPoints, Func<bool> isBlank)
        {
            this.getImageFunc = getImage;
            this.getDrawPointsFunc = getPoints;
            this.loadDrawPoints = loadPoints;
            this.isBlankFunc = isBlank;
        }

        #region Properties

        public static readonly BindableProperty CaptionTextProperty = BindableProperty.Create("CatptionText", typeof(string), typeof(SignaturePadView), string.Empty, BindingMode.OneWay,null, null, null, null);
        public static readonly BindableProperty CaptionTextColorProperty = BindableProperty.Create("CatptionTextColor", typeof(Color), typeof(SignaturePadView), Color.Default, BindingMode.OneWay,null, null, null, null);
        public static readonly BindableProperty ClearTextProperty = BindableProperty.Create("ClearText", typeof(string), typeof(SignaturePadView), string.Empty, BindingMode.OneWay, null, null, null, null);
        public static readonly BindableProperty ClearTextColorProperty = BindableProperty.Create("ClearTextColor", typeof(Color), typeof(SignaturePadView), Color.Default, BindingMode.OneWay, null, null, null, null);
        public static readonly BindableProperty PromptTextProperty = BindableProperty.Create("PromptText", typeof(string), typeof(SignaturePadView), string.Empty, BindingMode.OneWay, null, null, null, null);
        public static readonly BindableProperty PromptTextColorProperty = BindableProperty.Create("PromptTextColor", typeof(Color), typeof(SignaturePadView), Color.Default, BindingMode.OneWay, null, null, null, null);
        public static readonly BindableProperty SignatureLineColorProperty = BindableProperty.Create("SignatureLineColor", typeof(Color), typeof(SignaturePadView), Color.Default, BindingMode.OneWay, null, null, null, null);
        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create("StrokeColor", typeof(Color), typeof(SignaturePadView), Color.Default, BindingMode.OneWay, null, null, null, null);
        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create("StrokeWidth", typeof(double), typeof(SignaturePadView), (double)0, BindingMode.OneWay, null, null, null, null);


        public string CaptionText
        {
            get { return (string)this.GetValue(SignaturePadView.CaptionTextProperty); }
            set { this.SetValue(SignaturePadView.CaptionTextProperty, value); }
        }


        public Color CaptionTextColor
        {
            get { return (Color)this.GetValue(SignaturePadView.CaptionTextColorProperty); }
            set { this.SetValue(SignaturePadView.CaptionTextColorProperty, value); }
        }


        public string ClearText
        {
            get { return (string)this.GetValue(SignaturePadView.ClearTextProperty); }
            set { this.SetValue(SignaturePadView.ClearTextProperty, value); }
        }


        public Color ClearTextColor
        {
            get { return (Color)this.GetValue(SignaturePadView.ClearTextColorProperty); }
            set { this.SetValue(SignaturePadView.ClearTextColorProperty, value); }
        }


        public string PromptText
        {
            get { return (string)this.GetValue(SignaturePadView.PromptTextProperty); }
            set { this.SetValue(SignaturePadView.PromptTextProperty, value); }
        }


        public Color PromptTextColor
        {
            get { return (Color)this.GetValue(SignaturePadView.PromptTextColorProperty); }
            set { this.SetValue(SignaturePadView.PromptTextColorProperty, value); }
        }


        public Color SignatureLineColor
        {
            get { return (Color)this.GetValue(SignaturePadView.SignatureLineColorProperty); }
            set { this.SetValue(SignaturePadView.SignatureLineColorProperty, value); }
        }


        public double StrokeWidth
        {
            get { return (double)this.GetValue(SignaturePadView.StrokeWidthProperty); }
            set { this.SetValue(SignaturePadView.StrokeWidthProperty, value); }
        }


        public Color StrokeColor
        {
            get { return (Color)this.GetValue(SignaturePadView.StrokeColorProperty); }
            set { this.SetValue(SignaturePadView.StrokeColorProperty, value); }
        }

        #endregion
    }
}
