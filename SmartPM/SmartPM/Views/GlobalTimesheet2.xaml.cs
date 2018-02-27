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
using System.Collections.ObjectModel;

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
            obj.projectId = model.projectId;

            if (checkConnect() == true)
                RenderFilterTask(obj.userId,obj.groupId, obj.projectId);
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
      
        public async void RenderFilterTask(string gid, string uid, string pid)
        {
            var ResultTask = new ObservableCollection<TaskModel>();
            try
            {
             
                string filterTask = await FilterTimesheetService.FilterTask(gid, uid, pid);
                ResultTask = JsonConvert.DeserializeObject<ObservableCollection<TaskModel>>(filterTask);
                if (ResultTask != null)
                    PickerPhase.ItemsSource = ResultTask;
                else
                { 
                    var act = await DisplayAlert("Notice","ไม่มีเฟสที่รับผิดชอบ หรือ เฟสเสร็จสิ้นแล้ว" ,"Ok", "Cancle");
                    if (act)
                        await Navigation.PopAsync();
                }
            }
            catch
            {
                //await DisplayAlert("Notice", "Fail to load content", "Cancle");
            }
        }
     
        
        private void OnPhaseSelectedIndexChanged(object sender,SelectedItemChangedEventArgs e)
        {
            var modelPicker = (Picker)sender;
            int selectedIndex = modelPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var model = (TaskModel)modelPicker.SelectedItem;
                obj.TaskName = model.taskName;
                obj.taskId = model.taskId;
            }

          

        }
        
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