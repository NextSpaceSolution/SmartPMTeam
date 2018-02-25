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
using System.Collections.ObjectModel;
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
          
            Labelfullname.Text = model.fullName;
            Labeljob.Text = model.jobResp;
            LabelProname.Text = model.projectName;
            Taskname.Text = model.TaskName;

            obj.fullName = model.fullName;
            obj.jobResp = model.jobResp;
            obj.projectName = model.projectName;
            obj.TaskName = model.TaskName;
            obj.projectId = model.projectId;
            obj.taskId = model.taskId;
            obj.userId = model.userId;
            obj.groupId = model.groupId;

            if (checkConnect() == true)
                RenderFilterFunction(obj.userId,obj.groupId, obj.projectId, obj.taskId);
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
       
        public async void RenderFilterFunction(string uid, string gid, string pid, string taid)
        {
            var ResultFunc = new ObservableCollection<TimesheetfunctionModel>();
            try
            {

              
                string filterFunc = await FilterTimesheetService.FilterFunction(uid, gid,pid, taid);
                ResultFunc = JsonConvert.DeserializeObject<ObservableCollection<TimesheetfunctionModel>>(filterFunc);
                PickerFunction.ItemsSource = ResultFunc;
            
            }
            catch
            {
                await DisplayAlert("Notice", "Fail to load content", "Cancle");
            }
        }



        private void OnFunctionSelectedIndexChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var modelPicker = (Picker)sender;
            int selectedIndex = modelPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var model = (TimesheetfunctionModel)modelPicker.SelectedItem;
                obj.functionId = model.functionId;
                obj.functionName = model.functionName;
            }



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