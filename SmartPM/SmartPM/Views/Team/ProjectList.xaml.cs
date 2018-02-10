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
using System.IO;

namespace SmartPM.Views.Team
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectList : ContentPage
    {

        private AuthenModel userAccount = new AuthenModel();

        public   AProjectList pdata =  new AProjectList();
        public string uid { get; set; }
        public string gid { get; set; }
        public ProjectList(string id ,string groupid)
        {
            InitializeComponent();

            uid = id;
            gid = groupid;
            RenderAPI(uid,gid);

           /* List<AProjectList> list = new List<AProjectList>
            {
                new AProjectList
                {
                    projectName = "โปรเจค SpacXcross",
                    projectManager = "Elon Maskie",
                    projectStart = "32 มกราคม 2561 - 32 มกราคม 2580",
                    projectEnd = "0 Days",
                    projectCost = "10,000,000,000 Baht",
                   backclr = "#4CAF50",
                     picture = "thumTime"
                },

                  new AProjectList
                {
                    projectName = "โปรเจค NextEarth",
                    projectManager = "Elon Maskie",
                    projectStart = "30 กุมภาพันธ์ 2561 - 32 มกราคม 2580",
                    projectEnd = "0 Days",
                    projectCost = "100,000,000,000,000,000 Baht",
                    backclr = "#c8cd20",
                     picture = "thumTime"
                },

                                    new AProjectList
                {
                    projectName = "โปรเจค TheEndOfEarth",
                    projectManager = "Cloee Aisas",
                    projectStart = "25 ตุลาคม 2561 - 32 มกราคม 2580",
                    projectEnd = "0 Days",
                    projectCost = "100,000,000,000,000,000 Baht",
                    backclr = "#e83030",
                     picture = "thumTime"
                }
            };
            projectlist.ItemsSource = list;*/
        }

        public async void RenderAPI(string id, string gid)
        {
            string jsonResult = await FilterProject(id, gid);
            JObject prodata = JObject.Parse(jsonResult);

            pdata.projectName = (string)prodata["projectName"];
            pdata.projectManager = (string)prodata["projectManager"];
            pdata.projectStart = (string)prodata["projectStart"];
            pdata.projectEnd = (string)prodata["projectEnd"];
            pdata.projectCost = (string)prodata["projectCost"];
            BindingContext = pdata;
        }

        public async Task<string> FilterProject(string uid , string gid )
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", uid));
                postData.Add(new KeyValuePair<string, string>("groupid", gid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://localhost:56086/APIRest2/FilterProject", content))
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


        private async void projectlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Projectlists = e.Item as AProjectList;
            string id = Projectlists.projectName;


            var page = new ProjectDashboardScreen();
            //App.Current.MainPage = new NavigationPage(page);
            await Navigation.PushAsync(page);
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

    }
}