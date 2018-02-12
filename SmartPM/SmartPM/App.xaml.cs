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
<<<<<<< HEAD
		public App ()
		{
			InitializeComponent();
            MainPage = new GlobalTimesheetSubmit("92","10","20");
            
           /*MainPage = new NavigationPage(new TeamDashboardScreen())
=======

        public App()
        {
            InitializeComponent();
            //MainPage = new UserProfileScreen();
            /*
           MainPage = new NavigationPage(new TeamDashboardScreen())
>>>>>>> 7f306aa6df9585444b9a1188b9b6d8fb299b2fd9
            {
                BarBackgroundColor = Color.FromHex("#E91E63"),
                BarTextColor = Color.White
            };*/
<<<<<<< HEAD
/*<<<<<<< HEAD
           
=======
>>>>>>> 5ee1fdb56e909cee428a7c90df0d79abdabc275c
=======
>>>>>>> 7f306aa6df9585444b9a1188b9b6d8fb299b2fd9
            //var page = new PMDashboardScreen();
            ///NavigationPage.SetHasBackButton(page, false);


            MainPage = new NavigationPage(new GlobalTimesheet());


            //MainPage = new NavigationPage( new MainPage());
            // MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
