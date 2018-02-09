using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SmartPM.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SmartPM.Views.Admin;
using Plugin.Connectivity;
using SmartPM.Views;
using SmartPM.Views.Team;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskScreen : ContentPage
	{

        private AuthenModel userAccount = new AuthenModel();

        public TaskModel tdata = new TaskModel();
        public string uid { get; set; }
        public string gid { get; set; }

        public string pid { get; set; }
        public TaskScreen (string user, string group, string project)
		{
			InitializeComponent ();
            uid = user;
            gid = group;
            pid = project;

            RenderAPI(uid, gid, pid);

            /*List<TaskModel> task = new List<TaskModel>
            {
                new TaskModel
                {
                    taskId = "t001",
                    projectnumber = "p001",
                    taskname = "Gettering Requirement",
                    taskstart = "31/01/2018",
                    taskend = "01/02/2018",
                    actualstart = "01/02/2018",
                    actualend = "01/02/2018",
                    variant = "0",
                     team = "Employee1,dummyEmployee",
                     backclr = "#4CAF50",
                     picture = "thumTime"
                },
                new TaskModel
                {
                    taskId = "t002",
                    projectnumber = "p001",
                    taskname = "System Analysis And Design",
                    taskstart = "02/02/2018",
                    taskend = "05/02/2018",
                    actualstart = "03/02/2018",
                    actualend = "05/02/2018",
                    variant = "2",
                    team = "Employee2,dummyEmployee",
                     backclr = "#4CAF50",
                     picture = "thumTime"
                },
                new TaskModel
                {
                     taskId = "t003",
                    projectnumber = "p001",
                    taskname = "Development",
                    taskstart = "02/02/2018",
                    taskend = "05/02/2018",
                    actualstart = "03/02/2018",
                    actualend = "05/02/2018",
                    variant = "2",
                    team = "Employee3,dummyEmployee",
                    backclr = "#4CAF50",
                     picture = "thumTime"
                },
                  new TaskModel
                {
                     taskId = "t004",
                    projectnumber = "p001",
                    taskname = "Tesing",
                    taskstart = "02/02/2018",
                    taskend = "05/02/2018",
                    actualstart = "03/02/2018",
                    actualend = "05/02/2018",
                    variant = "2",
                    team = "Employee4,dummyEmployee",
                    backclr = "#4CAF50",
                     picture = "thumTime"
                },
                    new TaskModel
                {
                     taskId = "t005",
                    projectnumber = "p001",
                    taskname = "Deploy",
                    taskstart = "02/02/2018",
                    taskend = "05/02/2018",
                    actualstart = "03/02/2018",
                    actualend = "05/02/2018",
                    variant = "2",
                    team = "Employee5,dummyEmployee",
                    backclr = "#c8cd20",
                    picture = "thumTime"
                },
            };*/

           
            //Tasklist.ItemsSource = task;

		}

        public async void RenderAPI(string uid, string gid, string pid)
        {
            string jsonResult = await FilterTask(uid, gid,pid);
            JObject taskdata = JObject.Parse(jsonResult);

            tdata.taskId = (string)taskdata["taskId"];
            tdata.projectnumber = (string)taskdata["projectnumber"];
            tdata.taskname = (string)taskdata["taskname"];
            tdata.taskstart = (string)taskdata["taskstart"];
            tdata.taskend = (string)taskdata["taskend"];
            tdata.actualstart = (string)taskdata["actualstart"];
            tdata.actualend = (string)taskdata["actualend"];
            tdata.variant = (string)taskdata["variant"];
            tdata.team = (string)taskdata["team"];
            tdata.backclr = (string)taskdata["backclr"];
            tdata.picture = (string)taskdata["picture"];
            BindingContext = taskdata;
        }
        

        public async Task<string> FilterTask(string uid , string gid,string pid)
        {
            try
            {
                // This is the Postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string , string>("id",uid));
                postData.Add(new KeyValuePair<string, string>("group", gid));
                postData.Add(new KeyValuePair<string, string>("project", pid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://localhost:56086/APIRest2/FilterTask", content))
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



        private async void tasklist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new TaskFunctionScreen());
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
    }
}
