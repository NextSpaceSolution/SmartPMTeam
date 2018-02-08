using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartPM.Views;
using SmartPM.Views.PM;
using SmartPM.Views.Team;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace SmartPM
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            //MainPage = new UserProfileScreen();
            
           MainPage = new NavigationPage(new TeamDashboardScreen())
            {
                BarBackgroundColor = Color.FromHex("#E91E63"),
                BarTextColor = Color.White
            };
<<<<<<< HEAD
           
=======
>>>>>>> 5ee1fdb56e909cee428a7c90df0d79abdabc275c
            //var page = new PMDashboardScreen();
            //NavigationPage.SetHasBackButton(page, false);
            //MainPage = new LoginScreen();
<<<<<<< HEAD
            //MainPage = new TeamDashboardScreen();
            // MainPage = new MainPage();
=======
           
                
            //MainPage = new NavigationPage( new MainPage());
           // MainPage = new MainPage();
>>>>>>> 5ee1fdb56e909cee428a7c90df0d79abdabc275c
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
