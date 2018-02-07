using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SmartPM.Views;

namespace SmartPM
{
	public partial class MainPage : ContentPage
	{
        string a;
        string b;
        string c;
        public MainPage()
        {
            InitializeComponent();

        }

        async void next(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TempPage(a,b,c));
        }

   
    }
}
