using SmartPM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using Xamarin.Forms.Xaml;


namespace SmartPM.Views.Team
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamTimesheetScreen : ContentPage
    {
        private AuthenModel userAccount = new AuthenModel();
        public TeamTimesheetScreen()
        {
            InitializeComponent();
            project.Items.Add("Project001");
            project.Items.Add("Project002");
            project.Items.Add("dummyProject");

            phase.Items.Add("Phase001");
            phase.Items.Add("Phase002");
            phase.Items.Add("Phase003");
            phase.Items.Add("Phase004");
            phase.Items.Add("Phase005");
            phase.Items.Add("Phase006");

            jobResp.Items.Add("Job001");
            jobResp.Items.Add("Job002");
            jobResp.Items.Add("Job003");
            jobResp.Items.Add("Job004");
            jobResp.Items.Add("Job005");
            jobResp.Items.Add("Job006");


            
            List<TimeSheetModel> timesheets = new List<TimeSheetModel>
            {
                new TimeSheetModel
                {
                    userId = "001",
                    userName = "thisuser",
                    userPosition = "President",
                    timesheetId = "ts001",
                    date = "Get Date Realtiem",
                    projectname = "p0001",
                    projectPhase = "Task Gettering Requirement",
                    jobResponsible = "Pai pop look ka",
                    Duration = "4000 hr",
                    OT = "3000",
                    totalDuration = "7000 hr",
                    totalOT = "8000 hr",
                    total = "Duration + OT",
                    RFCno = "Null"

                }
            };
        }

        private void project_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void phase_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void job_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
    }
}