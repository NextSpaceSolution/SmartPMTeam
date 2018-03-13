using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartPM.Views;
using SmartPM.Views.PM;
using SmartPM.Views.Team;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using SmartPM.Helpers;
using SmartPM.Services;
using Com.OneSignal;


namespace SmartPM
{
	public partial class App : Application
	{

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MoodBoardEditComment());
            MainPage = new NavigationPage(new LoginScreen());
            /*OneSignal.Current.StartInit("8e9e2a3a-dfdb-49f0-925c-c756cf54011a")
                  .EndInit();
            MainPage = new TestValidationInput();*/
            /*
            if (string.IsNullOrEmpty(Settings.UserName) || string.IsNullOrEmpty(Settings.PassWord))
                MainPage = new LoginScreen();
            else

<<<<<<< HEAD
=======
            try
>>>>>>> ce2ac5fd00f847396d3f0b2134e178ac3ded7224
            {
                OneSignal.Current.StartInit("8e9e2a3a-dfdb-49f0-925c-c756cf54011a")
                      .EndInit();
                MainPage = new LoginScreen();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }


                    /* if (string.IsNullOrEmpty(Settings.UserName) || string.IsNullOrEmpty(Settings.PassWord))
                         MainPage = new LoginScreen();
                     else
                     {
                         if (Settings.Group == "99")
                         {
                             var page = new AdminDashboard();
                             NavigationPage.SetHasBackButton(page, false);
                             App.Current.MainPage = new NavigationPage(page);



                         }

                         else if (Settings.Group == "10" ||Settings.Group == "50")
                         {

                             var page = new TeamDashboardScreen(Settings.UserId, Settings.Group);
                             NavigationPage.SetHasBackButton(page, false);
                             App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };

                         }
                     }*/
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
