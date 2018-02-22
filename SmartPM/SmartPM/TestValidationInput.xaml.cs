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
using Com.OneSignal;

namespace SmartPM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestValidationInput : ContentPage
	{
		public TestValidationInput ()
		{
			InitializeComponent ();
           
           
            SomeMethod();

        }

        void SomeMethod()
        {
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }

        private void IdsAvailable(string userID, string pushToken)
        {
            Player_ID.Text = userID;
            Push_ID.Text = pushToken;
        }


    }
}