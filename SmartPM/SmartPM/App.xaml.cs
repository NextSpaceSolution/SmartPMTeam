﻿using System;
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
            //MainPage = new UserProfileScreen();
            /*
           MainPage = new NavigationPage(new TeamDashboardScreen("10","20"))
            {
                BarBackgroundColor = Color.FromHex("#E91E63"),
                BarTextColor = Color.White
            };*/
            //var page = new PMDashboardScreen();
            ///NavigationPage.SetHasBackButton(page, false);

            //string id = "10009";
            // string gid = "10";
            //MainPage = new NavigationPage(new TodoTimeline());


            //MainPage = new NavigationPage( new MainPage());
            // MainPage = new NavigationPage(new TeamDashboardScreen("100017","50"));
            MainPage = new TempPage("100001");
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
