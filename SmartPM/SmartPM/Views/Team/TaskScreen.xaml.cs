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

        public List<TaskModel2> tdata = new List<TaskModel2>();
        public string uid { get; set; }
        public string gid { get; set; }

        public string pid { get; set; }
        public TaskScreen (string user, string group, string project)
		{
			InitializeComponent ();


            Title = user;

            uid = user;
            gid = group;
            pid = project; 

            RenderAPI(gid, uid, pid);


            

            /*List<TaskModel2> task = new List<TaskModel2>

            {
                new TaskModel2
                {
                    taskId = "t001",
                    projectNumber = "p001",
                    taskName = "Gettering Requirement",
                    taskStart = "31/01/2018",
                    taskEnd = "01/02/2018",
                    actualStart = "01/02/2018",
                    actualEnd = "01/02/2018",
                    variant = "0",
                   
                },
                new TaskModel2
                {
                    taskId = "t002",
                    projectNumber = "p001",
                    taskName = "System Analysis ",
                    taskStart = "02/02/2018",
                    taskEnd = "05/02/2018",
                    actualStart = "03/02/2018",
                    actualEnd = "05/02/2018",
                    variant = "2",
 
                },
                new TaskModel2
                {
                     taskId = "t003",
                    projectNumber = "p001",
                    taskName = "Development",
                    taskStart = "02/02/2018",
                    taskEnd = "05/02/2018",
                    actualStart = "03/02/2018",
                    actualEnd = "05/02/2018",
                    variant = "2",

                },
                  new TaskModel2
                {
                     taskId = "t004",
                    projectNumber = "p001",
                    taskName = "Tesing",
                    taskStart = "02/02/2018",
                    taskEnd = "05/02/2018",
                    actualStart = "03/02/2018",
                    actualEnd = "05/02/2018",
                    variant = "2",

                },
                    new TaskModel2
                {
                     taskId = "t005",
                    projectNumber = "p001",
                    taskName = "Deploy",
                    taskStart = "02/02/2018",
                    taskEnd = "05/02/2018",
                    actualStart = "03/02/2018",
                    actualEnd = "05/02/2018",
                    variant = "2",

                },
            };


            Tasklist.ItemsSource = task;*/

		}


        private async void tasklist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var tasklists = e.Item as TaskModel;
            string id = tasklists.taskId;

            var page = new TaskFunctionScreen(uid, gid, pid, id);
            await Navigation.PushAsync(page);
            //await Navigation.PushAsync(new TaskFunctionScreen(uid, gid, pid, id));
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }




        public async void RenderAPI(string gid, string uid, string pid)
        {
            var list = new List<TaskModel2>();
            var jsonResult = await FilterTask(gid , uid , pid);
            list = JsonConvert.DeserializeObject<List<TaskModel2>>(jsonResult);
            Tasklist.ItemsSource = list;
            this.IsBusy = false;

        }
        

        public async Task<string> FilterTask(string gid, string uid, string pid)
        {
            try
            {
                // This is the Postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string , string>("gid", gid));
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                postData.Add(new KeyValuePair<string, string>("pid", pid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                   // client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest2/FilterTask", content))
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
