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
using SmartPM.Models;
using SmartPM.Models.Timesheet;
using Xamarin.Forms.Xaml;
using System;
using Plugin.Connectivity;
using SmartPM.Services;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlobalTimesheet3 : ContentPage
    {
     
        TimesheetOneModel obj = new TimesheetOneModel();
        public GlobalTimesheet3 (TimesheetOneModel model)
		{
			InitializeComponent ();
            /*
            this.uid = uid;
            this.fname = fname;
            this.lname = lname;
            _job = jobr;
            this.gid = gid;
            this.pid = pid;
            this.tname = tname;
            */
            Labelfullname.Text = model.fullName;
            Labeljob.Text = model.jobResp;
            LabelProname.Text = model.projectName;
            Taskname.Text = model.TaskName;

            obj.fullName = model.fullName;
            obj.jobResp = model.jobResp;
            obj.projectName = model.projectName;
            obj.TaskName = model.TaskName;
            obj.projectId = model.projectId;
            obj.userId = model.userId;
            obj.groupId = model.groupId;

            if (checkConnect() == true)
                RenderFindTid(obj.projectId,obj.TaskName);
            else
                Title = "Internet not connect";

            

            
           /* job.Items.Add("Job001");
            /*job.Items.Add("Job003");
            */

        }

        public bool checkConnect()
        {
            var isConnected = CrossConnectivity.Current.IsConnected;
            if (isConnected == true)
            {
                return true;
            }
            else
                return false;
        }
        public async void RenderFindTid(string pid, string tname)
        {
            try
            {
                string jsonResult = await FilterTimesheetService.FindTaskId(pid, tname);
                JObject data = JObject.Parse(jsonResult);
                obj.taskId = (string)data["taskId"];
                RenderFilterFunction(obj.userId, obj.projectId, obj.taskId);
            }
            catch
            {
                await DisplayAlert("Notice", "Fail to load content", "Cancle");
            }
          
        }

        public async void RenderFilterFunction(string uid, string pid, string taid)
        {
            try
            {

                var ResultFunc = new List<TimesheetfunctionModel>();
                var tempResultFunc = new List<TimesheetfunctionModel>();
                string filterFunc = await FilterTimesheetService.FilterFunction(uid, pid, taid);
                ResultFunc = JsonConvert.DeserializeObject<List<TimesheetfunctionModel>>(filterFunc);

                foreach (var item in ResultFunc)
                {
                    tempResultFunc.Add(new TimesheetfunctionModel
                    {
                        functionId = item.functionId,
                        functionName = item.functionName

                    });
                }

                foreach (var item in tempResultFunc)
                {
                    if (item != null)
                        job.Items.Add(item.functionName);
                    else
                        job.Items.Add("Non of above");


                }

            }
            catch
            {
                await DisplayAlert("Notice", "Fail to load content", "Cancle");
            }
        }

        private void job_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj.functionName = job.Items[job.SelectedIndex];
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(obj.functionName))
            {
                await DisplayAlert("Notic", "!!! กรุนาเลือกชื่องาน", "Ok");

            }
            else
                await Navigation.PushAsync(new GlobalTimesheetSubmit(obj));
        }


        
    }
}