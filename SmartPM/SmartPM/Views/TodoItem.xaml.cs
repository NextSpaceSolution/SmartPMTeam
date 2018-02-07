using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Views;
using SmartPM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoItem : ContentPage
	{
        private string d { get; set; }
        private string h { get; set; }
        private string de { get; set; }
		public TodoItem ()
		{
			InitializeComponent ();
		}

        public async void OnSaveActivated(object sender, EventArgs e)
        {
            d = date.Text;
            h = header.Text;
            de = descrips.Text;
            
            await Navigation.PushAsync(new TempPage(d,h,de));
        }
	}
}