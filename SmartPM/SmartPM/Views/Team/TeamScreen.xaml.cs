using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SmartPM.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SmartPM.Views.Admin;
using Plugin.Connectivity;
using SmartPM.Views;
using SmartPM.Views.PM;


namespace SmartPM.Views.Team
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamScreen : ContentPage
	{

        private AuthenModel userAccount = new AuthenModel();
        public TeamScreen ()
		{
			InitializeComponent ();

            List<TeamModels> list = new List<TeamModels>
            {
                new TeamModels
                {
                    projectnumber = "Project 001",
                    managername = "ProjectManager 001",
                    employeename1 = "Employee001",
                    employeename2 = "Employee002",
                    employeename3 = "Employee003",
                    employeename4 = "Employee004",
                    employeename5 = "Employee005",
                    pictureteam1 = "userTemp",
                    pictureteam2 = "userTemp",
                    pictureteam3 = "userTemp",
                    pictureteam4 = "userTemp",
                    pictureteam5 = "userTemp",
                    picturemanager = "luffy"
                    
                }
            };
            projectlist.ItemsSource = list;

        }
        private async void Add_Newteam(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new PMNewteamScreen());
        }

        private async void teamlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var page = new PMTeamdetailScreen();
            //App.Current.MainPage = new NavigationPage(page);
            await Navigation.PushAsync(page);
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
    }
}