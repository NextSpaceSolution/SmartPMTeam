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


namespace SmartPM.Views.Team
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectDashboardScreen : ContentPage
	{

       


        private AuthenModel userAccount = new AuthenModel();

        AProjectList temp = new AProjectList();

        AProjectList pdata = new AProjectList();

        public string uid { get; set; }
        public string gid { get; set; }
        public string pid { get; set; }
        public ProjectDashboardScreen (string user ,string group  , string project)
        {
            uid = user;
            gid = group;
            pid = project;

			InitializeComponent ();
           
        }




        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new LoginScreen());
        }

        private async void TapGestureRecognizer_Tapped(object sender, ItemTappedEventArgs e)
        {

            var Projectlists = e.Item as AProjectList;
            string id = Projectlists.projectNumber;
            await Navigation.PushAsync(new ProjectDetailScreen(id));


        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskScreen(uid,gid,pid));
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamScreen());
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new TempPage());
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
    }
}