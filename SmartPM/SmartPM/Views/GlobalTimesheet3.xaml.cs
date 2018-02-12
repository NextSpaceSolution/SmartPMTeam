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

           // RenderFindTid(tname)

            
            job.Items.Add("Job001");
            job.Items.Add("Job003");
            

        }

        /*
        public async void RenderFindTid(string tname)
        {
            string jsonResult = await FindTaskId(tname);
            JObject data = JObject.Parse(jsonResult);
            tid = (string)data["taskId"];
            RenderFilterFunction(gid, uid, pid, tid);
        }*/

        public async void RenderFilterFunction(string gid, string uid, string pid, string taid)
        {
            var ResultFunc = new List<TimesheetfunctionModel>();
            var tempResultFunc = new List<TimesheetfunctionModel>();
            string filterFunc = await FilterFunction(gid, uid, pid, taid);
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
            await Navigation.PushAsync(new GlobalTimesheetSubmit(obj));
        }


        public async Task<string> FindTaskId(string name)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("tname", name));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/GetTaskId", content))
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

        public async Task<string> FilterFunction(string gid, string uid, string pid, string tid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("gid", gid));
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                postData.Add(new KeyValuePair<string, string>("pid", pid));
                postData.Add(new KeyValuePair<string, string>("tid", tid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/FilterFunction", content))
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