using SmartPM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using SmartPM.Views;
using SmartPM.Views.Team;
using SmartPM.Views.PM;
using SmartPM.Views.Admin;
using Xamarin.Forms.Xaml;

namespace SmartPM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestValidationInput : ContentPage
	{
		public TestValidationInput ()
		{
			InitializeComponent ();
           
		}

        public async void AlertMassage(string msg)
        {
            await App.Current.MainPage.DisplayAlert("Notification", msg , "Oke");
        }
        public async void onSubmit(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MainEntry.Text))
            {
                AlertMassage("ห้ามว่าง");
                MainEntry.Focus();
            }
            else if (string.IsNullOrWhiteSpace(SecondEntry.Text))
            {
                AlertMassage("สัสบอกว่าห้ามว่าง");
                SecondEntry.Focus();
            }
            else if (string.IsNullOrWhiteSpace(ThirdEntry.Text))
            {
                AlertMassage("สัสบอกว่าห้ามว่างควย");
                ThirdEntry.Focus();
            }
            else
            {
                await Navigation.PushAsync(new TestConcate());
            }
            
        }
	}
}