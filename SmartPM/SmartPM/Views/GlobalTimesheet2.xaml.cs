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

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlobalTimesheet2 : ContentPage
    {
        
       
        TimesheetOneModel obj = new TimesheetOneModel();
        public GlobalTimesheet2 (TimesheetOneModel model)
		{
            InitializeComponent();
            /*
            this.uid = uid;
            this.fname = fname;
            this.lname = lname;
            this.job = job;
            this.gid = gid;
            this.pname = pname;


           
            */


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
            
            /*
            Labelfname.Text = fname;
            Labellname.Text = lname;
            Labeljob.Text = lname;
            LabelProname.Text = pname;*/
            /*
            phase.Items.Add("Phase001");        
            phase.Items.Add("Phase003");
            phase.Items.Add("Phase006");
            
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
        public async void RenderFindPro(string name)
        {
            string jsonResult = await FindProId(name);
            JObject data = JObject.Parse(jsonResult);
            obj.projectId = (string)data["projectNumber"];
            RenderFilterTask(obj.groupId, obj.userId, obj.projectId);
        }

        public async void RenderFilterTask(string gid, string uid, string pid)
        {
            var ResultTask = new List<TaskModel>();
            var tempResultTask = new List<TaskModel>();
            string filterTask = await FilterTask(gid, uid, pid);
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
            {   if (item != null)
                    phase.Items.Add(item.taskName);
                else
                    phase.Items.Add("Non of above");


            }
        }

        private void phase_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj.TaskName = phase.Items[phase.SelectedIndex];
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GlobalTimesheet3(obj));
        }


        public async Task<string> FindProId(string name)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("pname", name));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/GetProId", content))
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

        public async Task<string> FilterTask(string gid, string uid, string pid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("gid", gid));
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                postData.Add(new KeyValuePair<string, string>("pid", pid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/FilterTask", content))
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