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
using SmartPM.Models.Timesheet;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.ObjectModel;
using SmartPM.Services;
using SmartPM.Models;
using Plugin.Connectivity;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GlobalTimesheet : ContentPage
	{
        
    
        TimesheetOneModel objTimesheet = new TimesheetOneModel();

        public GlobalTimesheet (TimesheetOneModel parsModel)
		{
			InitializeComponent ();
      
            objTimesheet.userId = parsModel.userId;
            objTimesheet.groupId = parsModel.groupId;



            if (checkConnect() == true) { 
                RenderFilterPro(objTimesheet.userId, objTimesheet.groupId);
                renderReqUserInfo(objTimesheet.userId);
            }
            else
                Title = "Internet not connect";

            Labelfullname.Text = objTimesheet.fullName;

            Labeljob.Text = objTimesheet.jobResp;
          
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
        private async void RenderFilterPro(string uid, string gid)
        {
            var ResultProject = new ObservableCollection<AProjectList>();
            try
            {
                string filterProject = await FilterTimesheetService.FilterProject(uid, gid);
                ResultProject = JsonConvert.DeserializeObject<ObservableCollection<AProjectList>>(filterProject);
                PickerProject.ItemsSource = ResultProject;

            }
            catch
            {
                //await DisplayAlert("Notice", "Time Out", "Cancle");
            }

        }

        public async void renderReqUserInfo(string id)
        {
            try
            {
                string resultInfo = await FilterTimesheetService.ReqUserInfo(id);
                JObject data = JObject.Parse(resultInfo);
                string fname = (string)data["firstname"];
                string lname = (string)data["lastname"];
                string jobRes = (string)data["jobResponsible"];
                objTimesheet.fullName = fname + " " + lname;
                objTimesheet.jobResp = jobRes;
            }
            catch
            {
               // await DisplayAlert("Notice", "Fail to load content" ,"Cancle");
            }

        }
 
     
        
        private void OnProjectSelectedIndexChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var modelPicker = (Picker)sender;
            int selectedIndex = modelPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var model = (AProjectList)modelPicker.SelectedItem;
                objTimesheet.projectName = model.projectName;
                objTimesheet.projectId = model.projectNumber;
            }


        }

   

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(objTimesheet.projectName))
            {
               await DisplayAlert("Notic", "!!! กรุนาเลือกโปรเจค","Ok");

            }
            else
                //await DisplayAlert("Notic", objTimesheet.projectId + objTimesheet.projectName, "Ok");
             await Navigation.PushAsync(new GlobalTimesheet2(objTimesheet));
            //_uid, _ufname, _ulname, _job, _gid,_pname
        }

        
    }
	
}