using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriCare.Views
{
   public class acCell :ViewCell
    {
       public acCell()
        {
			var sl = new StackLayout () {
				Padding = new Thickness (20),

			};
			var labelF = new Label
			{

				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Start,
				TextColor = Color.White,
			};
			labelF.SetBinding(Label.TextProperty, "NameFriendly");
			sl.Children.Add (labelF);
            View = sl;
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
