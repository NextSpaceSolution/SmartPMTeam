using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views.Team
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskFunctionScreen : ContentPage
	{
        private AuthenModel userAccount = new AuthenModel();

        public List<TaskFunctionModel> fdata = new List<TaskFunctionModel>();
        public string uid { get; set; }
        public string gid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }


        public TaskFunctionScreen (string user ,string group ,string project, string task)
		{
           

			InitializeComponent ();

            uid = user;
            gid = group;
            pid = project;
            tid = task;

            RenderAPI(uid, gid, pid, tid);

            /*List<TaskFunctionModel> taskfunc = new List<TaskFunctionModel>
            {
                new TaskFunctionModel
                {
                    taskId = "t001",
                    projectNumber = "p001",
                    functionId = "001",
                    functionName = "pai pob look ka",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                    team = "employ1",
                    backclr = "#4CAF50",
                     picture = "thumTime"

                },
                new TaskFunctionModel
                {
                     taskId = "t001",
                    projectNumber = "p001",
                    functionId = "002",
                    functionName = "keb kwam tong karn",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                     team = "employ2",
                    backclr = "#4CAF50",
                     picture = "thumTime"

                },
                new TaskFunctionModel
                {
                    taskId = "t001",
                    projectNumber = "p001",
                    functionId = "003",
                    functionName = "saroob kwam tong karn",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                     team = "employ3",
                     backclr = "#4CAF50",
                     picture = "thumTime"


                },
                  new TaskFunctionModel
                {
                      taskId = "t001",
                    projectNumber = "p001",
                    functionId = "004",
                    functionName = "Requirement Analysis",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                     team = "employ4",
                     backclr = "#4CAF50",
                     picture = "thumTime"

                },
                    new TaskFunctionModel
                {
                     taskId = "t001",
                    projectNumber = "p001",
                    functionId = "005",
                    functionName = "dummy Function",
                    functionstart = "N/A",
                    functionend = "N/A",
                    actualstart = "N/A",
                    actualend = "N/A",
                    variant = "N/A",
                     team = "employ5",
                     backclr = "#4CAF50",
                     picture = "thumTime"

                },
            }
            Taskflist.ItemsSource = taskfunc;*/
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

        public async void RenderAPI(string uid, string gid, string pid , string tid)
        {
            var list = new List<TaskFunctionModel>();
            var jsonResult = await FilterFunction(uid, gid, pid, tid);
            list = JsonConvert.DeserializeObject<List<TaskFunctionModel>>(jsonResult);
            Taskflist.ItemsSource = list;
            this.IsBusy = false;

        }


        public async Task<string> FilterFunction(string uid, string gid, string pid, string tid)
        {
            try
            {
                // This is the Postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("gid", gid));
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                postData.Add(new KeyValuePair<string, string>("pid", pid));
                postData.Add(new KeyValuePair<string, string>("tid", tid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest2/FilterFunction", content))
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
                            return "error" + Convert.ToString(response.StatusCode);
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