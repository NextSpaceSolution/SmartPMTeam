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
	public partial class ProjectList : ContentPage
	{

        private AuthenModel userAccount = new AuthenModel();
        public ProjectList ()
		{
			InitializeComponent ();
            List<AProjectList> list = new List<AProjectList>
            {
                new AProjectList
                {
                    projectName = "โปรเจค SpacXcross",
                    projectManager = "Elon Maskie",
                    projectStart = "32 มกราคม 2561 - 32 มกราคม 2580",
                    projectEnd = "0 Days",
                    projectCost = "10,000,000,000 Baht",
                   backclr = "#4CAF50",
                     picture = "thumTime"
                },
               
                  new AProjectList
                {
                    projectName = "โปรเจค NextEarth",
                    projectManager = "Elon Maskie",
                    projectStart = "30 กุมภาพันธ์ 2561 - 32 มกราคม 2580",
                    projectEnd = "0 Days",
                    projectCost = "100,000,000,000,000,000 Baht",
                    backclr = "#c8cd20",
                     picture = "thumTime"
                },

                                    new AProjectList
                {
                    projectName = "โปรเจค TheEndOfEarth",
                    projectManager = "Cloee Aisas",
                    projectStart = "25 ตุลาคม 2561 - 32 มกราคม 2580",
                    projectEnd = "0 Days",
                    projectCost = "100,000,000,000,000,000 Baht",
                    backclr = "#e83030",
                     picture = "thumTime"
                }
            };
            projectlist.ItemsSource = list;
        }

        private async void projectlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Projectlists = e.Item as AProjectList;
            string id = Projectlists.projectName;


            var page = new ProjectDashboardScreen();
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