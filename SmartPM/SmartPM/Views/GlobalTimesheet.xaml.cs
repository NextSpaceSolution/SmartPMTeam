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
            var ResultProject = new List<AProjectList>();
            var tempResultProject = new List<AProjectList>();
            try
            {
                string filterProject = await FilterTimesheetService.FilterProject(uid, gid);
                ResultProject = JsonConvert.DeserializeObject<List<AProjectList>>(filterProject);

                foreach (var item in ResultProject)
                {
                    tempResultProject.Add(new AProjectList
                    {
                        projectNumber = item.projectNumber,
                        projectName = item.projectName
                    });
                }

                foreach (var item in tempResultProject)
                {
                    project.Items.Add(item.projectName);
                }
            }
            catch
            {

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
                await DisplayAlert("Notice", "Fail to load content" ,"Cancle");
            }

        }
 
        private void project_SelectedIndexChanged(object sender, EventArgs e)
        {
           objTimesheet.projectName  = project.Items[project.SelectedIndex];
           

        }

        /*
        private void OnProjectSelectedIndexChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var modelPicker = (Picker)sender;
            int selectedIndex = modelPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var model = (TimesheetOneModel)modelPicker.SelectedItem;
                objTimesheet.projectId = model.projectId;
            }


        }*/

   

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(objTimesheet.projectName))
            {
               await DisplayAlert("Notic", "!!! กรุนาเลือกโปรเจค","Ok");

            }
            else
                await Navigation.PushAsync(new GlobalTimesheet2(objTimesheet));
            //_uid, _ufname, _ulname, _job, _gid,_pname
        }

        
    }
	
}