﻿using SmartPM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using SmartPM.Views.Team;
using SmartPM.Views;

namespace SmartPM.Views.Team
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamDashboardScreen : ContentPage
	{
        private AuthenModel userAccount = new AuthenModel();
        string a;
        string b;
        string c;
		public TeamDashboardScreen ()
		{
            
            InitializeComponent ();

        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            userAccount = null;
            await Navigation.PushAsync(new LoginScreen());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            /*var page = new ProjectList()
            {
                BarBackgroundColor = Color.FromHex("#546E7A")
            };*/
            await Navigation.PushAsync(new ProjectList());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GlobalTimesheet());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfileScreen());
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TempPage(a,b,c));
        }
    }
}