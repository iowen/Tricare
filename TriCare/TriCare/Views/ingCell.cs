using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TriCare.Views
{
   public class IngCell :ViewCell
    {
       public IngCell()
        {
			var labelF = new Label
			{
				YAlign = TextAlignment.Center,
				XAlign = TextAlignment.Center,
				TextColor = Color.Navy
			};
			labelF.SetBinding(Label.TextProperty, "Name");

            View = labelF;
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
