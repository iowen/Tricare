using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriCare.Views
{
   public class PatientCell :ViewCell
    {
       public PatientCell()
        {
			var labelF = new Label
			{
				YAlign = TextAlignment.Center
			};
			labelF.SetBinding(Label.TextProperty, "FirstName");
			var labelL = new Label
			{
				YAlign = TextAlignment.Center
			};
			labelL.SetBinding(Label.TextProperty, "LastName");

            var pId = new Label
            {
                IsVisible = false
            };
            pId.SetBinding(Label.TextProperty, "PatientId");

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { labelF, labelL }
            };
            View = layout;
        }

       protected override void OnBindingContextChanged()
       {
           // Fixme : this is happening because the View.Parent is getting 
           // set after the Cell gets the binding context set on it. Then it is inheriting
           // the parents binding context.
           View.BindingContext = BindingContext;
           base.OnBindingContextChanged();
       }
    }
}
