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



        public App()
        {
            InitializeComponent();
<<<<<<< HEAD
            MainPage = new TestConcate();
=======
            //MainPage = new UserProfileScreen();

            /*
<<<<<<< HEAD
                      MainPage = new NavigationPage(new TeamDashboardScreen("10","20"))

=======
           MainPage = new NavigationPage(new TeamDashboardScreen("10","20"))
=======
>>>>>>> 883e90c1ef59b1ace11026e2284b9f40fb92ff0b
            
          /* MainPage = new NavigationPage(new TeamDashboardScreen("100017","50"))

>>>>>>> 064ebc055a958c1e94045921145701ee3eb8b7b1
            {
                BarBackgroundColor = Color.FromHex("#E91E63"),
                BarTextColor = Color.White
            };*/
>>>>>>> 6aa3eb650d4ab246bd5460df533a30fbb378fce4

                      MainPage = new NavigationPage(new TeamDashboardScreen("100017","50"))


                       {
                           BarBackgroundColor = Color.FromHex("#E91E63"),
                           BarTextColor = Color.White
                       };




                       //var page = new PMDashboardScreen();
                       ///NavigationPage.SetHasBackButton(page, false);


                       //string id = "10009";
                       // string gid = "10";
                       //MainPage = new NavigationPage(new TodoTimeline());






                       // MainPage = new NavigationPage(new TeamDashboardScreen("100017","50"));*/
            // MainPage = new TempPage("100001");
            /*var page = new TabbedPage
            {
                Children =
                {
                    new GlobalTimesheetList(),
                    new GlobalTimesheet()
                    
                }
            };
            MainPage = new NavigationPage(page);*/


           // MainPage = new NavigationPage(new TeamDashboardScreen("100017", "50"));
            MainPage = new LoginScreen();
        
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
