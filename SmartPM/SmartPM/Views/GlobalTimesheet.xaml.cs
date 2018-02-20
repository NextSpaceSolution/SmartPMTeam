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
            /*
            _uid = uid
            _job = job;
          _gid = gid;*/
            // objTimesheet.firstName = "ประหยัด";
            //  objTimesheet.lastName = "ไม่มีตังกินข้าว";
            //objTimesheet.fullName = objTimesheet.firstName + " " + objTimesheet.lastName;
            //objTimesheet.jobResp = "ผู้บัญชาการต่ำสุด";
            objTimesheet.userId = parsModel.userId;
            objTimesheet.groupId = parsModel.groupId;



            /*
            _ufname = "ประหยัด";
            _ulname = "ไม่มีตังกินข้าว";
            _fullname = _ufname + "  " + _ulname;
            */
            if (checkConnect() == true) { 
                RenderFilterPro(objTimesheet.userId, objTimesheet.groupId);
                renderReqUserInfo(objTimesheet.userId);
            }
            else
                Title = "Internet not connect";

            Labelfullname.Text = objTimesheet.fullName;
            // Labellname.Text = _ulname;
            Labeljob.Text = objTimesheet.jobResp;
            /*
            project.Items.Add("Project001");
            project.Items.Add("Project002");
            project.Items.Add("dummyProject");    
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
        private async void RenderFilterPro(string uid, string gid)
        {
            var ResultProject = new List<AProjectList>();
            var tempResultProject = new List<AProjectList>();
            try
            {
                string filterProject = await FilterProject(uid, gid);
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
            string resultInfo = await ReqUserInfo(id);
            JObject data = JObject.Parse(resultInfo);
            string fname = (string)data["firstname"];
            string lname = (string)data["lastname"];
            string jobRes = (string)data["jobResponsible"];
            objTimesheet.fullName = fname + " " + lname;
            objTimesheet.jobResp = jobRes;

        }
 
        private void project_SelectedIndexChanged(object sender, EventArgs e)
        {
           objTimesheet.projectName  = project.Items[project.SelectedIndex];
           

        }

   

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

        public async Task<string> FilterProject(string uid, string gid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                postData.Add(new KeyValuePair<string, string>("gid", gid));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/FilterProject", content))
                    {
                        if (((int)response.StatusCode >= 200) && ((int)response.StatusCode <= 299))
                        {
                            using (var responseContent = response.Content)
                            {
                                string result = await responseContent.ReadAsStringAsync();
                                Console.WriteLine(result);
                                return result;
                            }
                        }
                        else
                        {
                            return "error " + Convert.ToString(response.StatusCode);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return Convert.ToString(ex);
            }

        }

        public async Task<string> ReqUserInfo(string id)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", id));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest/GetUserInfo", content))
                    {
                        if (((int)response.StatusCode >= 200) && ((int)response.StatusCode <= 299))
                        {
                            using (var responseContent = response.Content)
                            {
                                string result = await responseContent.ReadAsStringAsync();
                                Console.WriteLine(result);
                                return result;
                            }
                        }
                        else
                        {
                            return "error " + Convert.ToString(response.StatusCode);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return Convert.ToString(ex);
            }

        }
    }
	
}