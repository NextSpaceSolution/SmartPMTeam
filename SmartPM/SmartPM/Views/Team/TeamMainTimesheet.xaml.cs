using SmartPM.Models;
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

namespace SmartPM.Views.Team
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamMainTimesheet : ContentPage
    {
        private AuthenModel userAccount = new AuthenModel();
        public TeamMainTimesheet()
        {
            InitializeComponent();

            List<TimeSheetModel> list = new List<TimeSheetModel>
            {
                new TimeSheetModel
                {
                    userId = "userLogged",
                    userName = "nameUserlogged",
                    firstname = "Fullname UserLogged",
                    lastname = "lnameUserLogged",
                    userPosition = "User Position",
                    date = "Get Realtime by SubmitDate",
                    timesheetId = "No.001",
                    projectname = "Get And Match for User then can user select",
                    projectPhase = "Get And Match for User  then can user select",
                    jobResponsible = "Get And Match for User  then can user select",
                    Duration = "70hr",
                    OT = "10hr",
                    total = "Duration + OT"

                },

                  new TimeSheetModel
                {

                    userId = "userLogged",
                    userName = "nameUserlogged",
                    firstname = "Fullname UserLogged",
                    lastname = "lnameUserLogged2",
                    userPosition = "User Position",
                    timesheetId = "No.002",
                     date = "Get Realtime by SubmitDate",
                    projectname = "Get And Match for User then can user select",
                    projectPhase = "Get And Match for User can user select",
                    jobResponsible = "Get And Match for User can user select",
                    Duration = "7000hr",
                    OT = "1000hr",
                    total = "Duration + OT"

                }
            };
            Teamtimesheetlist.ItemsSource = list;
        }
        private async void timesheetlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //await Navigation.PushAsync(new TeamTimesheetScreen());
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamTimesheetScreen());
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
    }
    }