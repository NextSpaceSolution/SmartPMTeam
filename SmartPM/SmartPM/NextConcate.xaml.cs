using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NextConcate : ContentPage
	{
		public NextConcate (string d)
		{
			InitializeComponent ();
            BindingContext = d;
		}
	}
}