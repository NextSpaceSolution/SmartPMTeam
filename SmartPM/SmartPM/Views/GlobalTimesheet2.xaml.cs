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
using SmartPM.Services;
using Plugin.Connectivity;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlobalTimesheet2 : ContentPage
    {
        
       
        TimesheetOneModel obj = new TimesheetOneModel();
        public GlobalTimesheet2 (TimesheetOneModel model)
		{
            InitializeComponent();

            obj.fullName = model.fullName;
            obj.jobResp = model.jobResp; ;
            obj.projectName = model.projectName;
           
            Labelfullname.Text = model.fullName;
            Labeljob.Text = model.jobResp;
            LabelProname.Text = model.projectName;
            obj.userId = model.userId;
            obj.groupId = model.groupId;
            if (checkConnect() == true)
                RenderFindPro(obj.projectName);
            else
                Title = "Internet not connect";
            
      

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
        public async void RenderFindPro(string name)
        {
            try
            {
                string jsonResult = await FilterTimesheetService.FindProId(name);
                JObject data = JObject.Parse(jsonResult);
                obj.projectId = (string)data["projectNumber"];
                RenderFilterTask(obj.groupId, obj.userId, obj.projectId);
            }
            catch {
                await DisplayAlert("Notice", "Fail to load content", "Cencle");
            }
        }

        public async void RenderFilterTask(string gid, string uid, string pid)
        {
            try
            {
                var ResultTask = new List<TaskModel>();
                var tempResultTask = new List<TaskModel>();
                string filterTask = await FilterTimesheetService.FilterTask(gid, uid, pid);
                ResultTask = JsonConvert.DeserializeObject<List<TaskModel>>(filterTask);

                foreach (var item in ResultTask)
                {
                    tempResultTask.Add(new TaskModel
                    {
                        taskId = item.taskId,
                        taskName = item.taskName

                    });
                }

                foreach (var item in tempResultTask)
                {
                    if (item != null)
                        phase.Items.Add(item.taskName);
                    else
                        phase.Items.Add("Non of above");


                }
            }
            catch
            {
                await DisplayAlert("Notice", "Fail to load content", "Cancle");
            }
        }

        private void phase_SelectedIndexChanged(object sender, EventArgs e)
        {

            obj.TaskName = phase.Items[phase.SelectedIndex];
        }

        /*
        private void OnPhaseSelectedIndexChanged(object sender,SelectedItemChangedEventArgs e)
        {
            var modelPicker = (Picker)sender;
            int selectedIndex = modelPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var model = (TimesheetOneModel)modelPicker.SelectedItem;
                obj.taskId = model.taskId;
            }

          

        }
        */
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(obj.TaskName))
            {
                await DisplayAlert("Notic", "!!! กรุนาเลือกเฟส", "Ok");

            }
            else
                await Navigation.PushAsync(new GlobalTimesheet3(obj));
        }


        
    }
}