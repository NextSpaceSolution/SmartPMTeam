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
	public partial class ProjectDetailScreen : ContentPage
	{
        private AuthenModel userAccount = new AuthenModel();
        public ProjectDetailScreen ()
		{
			InitializeComponent ();

            List<AProjectList> list = new List<AProjectList>
            {
                new AProjectList
                {
                    projectName = "dummyProject XXX",
                    projectManager = "dummyManager001",
                    projectStart = "dummyStartDate",
                    projectEnd = "dummyEndDate",
                    projectCost = "10,000,000 Baht",
                    projectCreateBy = "dummyManger001",
                    customerName = "dummyCustomer",
                    customerTel = "01-00-0000",
                    actualStart = "N/A",
                    actualEnd = "N/A",
                    note = "dummy Text",
                    variant = "N/A",
                    projectStatus ="N/A"

                }
            };
            projectlist.ItemsSource = list;
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
    }
}