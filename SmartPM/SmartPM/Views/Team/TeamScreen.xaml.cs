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
using SmartPM.Views.PM;


namespace SmartPM.Views.Team
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamScreen : ContentPage
    {

        private AuthenModel userAccount = new AuthenModel();
        ProjectInfo pdata = new ProjectInfo();
        List<ProjectInfo> list = new List<ProjectInfo>();
        List<TeamInfo> tlist = new List<TeamInfo>();

        public TeamScreen(string pid)
        {
            InitializeComponent();

            RenderHead(pid);
            RenderTeam(pid);
            /*List<TeamModels> list = new List<TeamModels>
            {
                new TeamModels
                {
                    projectnumber = "Project 001",
                    managername = "ProjectManager 001",
                    employeename1 = "Employee001",
                    employeename2 = "Employee002",
                    employeename3 = "Employee003",
                    employeename4 = "Employee004",
                    employeename5 = "Employee005",
                    pictureteam1 = "userTemp",
                    pictureteam2 = "userTemp",
                    pictureteam3 = "userTemp",
                    pictureteam4 = "userTemp",
                    pictureteam5 = "userTemp",
                    picturemanager = "luffy"
                    
                }
            };
            projectlist.ItemsSource = list;*/

        }

        private async void teamlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           /* var page = new PMTeamdetailScreen();
            //App.Current.MainPage = new NavigationPage(page);
            await Navigation.PushAsync(page);*/
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

        public async void RenderHead(string pid)
        {
            var jsonResult = await GetProInfo(pid);
            list = JsonConvert.DeserializeObject<List<ProjectInfo>>(jsonResult);
            if (list == null)
            {
                DisplayAlert("NNNN", "Novalue", "OK");
            }
            else
            {
                foreach (var item in list)
                {
                    pdata.projectName = item.projectName;
                    pdata.projectManagerfName = item.projectManagerfName + "  " + item.projectManagerlName;
                    pdata.projectStart = item.projectStart;
                    pdata.projectEnd = item.projectEnd;
                    pdata.actualStart = item.actualStart;
                    pdata.actualEnd = item.actualEnd;
                    pdata.projectCreateBy = item.projectCreateBy;
                    pdata.projectCost = item.projectCost;
                    pdata.customerName = item.customerName;
                    pdata.customerTel = item.customerTel;
                    pdata.variant = item.variant;
                    pdata.projectStatus = item.projectStatus;
                    pdata.note = item.note;


                }

                projectname.Text = pdata.projectName;
                projecmanager.Text = pdata.projectManagerfName;
            }
        }

            public async void RenderTeam(string pid)
            {


                var jsonResult = await getTeamforProject(pid);
                tlist = JsonConvert.DeserializeObject<List<TeamInfo>>(jsonResult);
            Teamlist.ItemsSource = tlist;
                this.IsBusy = false;



            }

            public async Task<string> GetProInfo(string pid)
            {
                try
                {
                    // This is the postdata
                    var postData = new List<KeyValuePair<string, string>>(2);
                    postData.Add(new KeyValuePair<string, string>("pid", pid));

                    HttpContent content = new FormUrlEncodedContent(postData);

                    using (var client = new HttpClient())
                    {
                        //client.Timeout = new TimeSpan(0, 0, 15);
                        using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest2/getProjectInfo", content))
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

            public async Task<string> getTeamforProject(string pid)
            {
                try
                {
                    // This is the postdata
                    var postData = new List<KeyValuePair<string, string>>(2);
                    postData.Add(new KeyValuePair<string, string>("pid", pid));

                    HttpContent content = new FormUrlEncodedContent(postData);

                    using (var client = new HttpClient())
                    {
                        //client.Timeout = new TimeSpan(0, 0, 15);
                        using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest2/getTeamforProject", content))
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
