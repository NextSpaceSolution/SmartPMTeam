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


namespace SmartPM.Views.Team
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectDashboardScreen : ContentPage
    {




        private AuthenModel userAccount = new AuthenModel();

        ProjectInfo temp = new ProjectInfo();
        List<ProjectInfo> list = new List<ProjectInfo>();



        AProjectList pdata = new AProjectList();

        public string uid { get; set; }
        public string gid { get; set; }
        public string pid { get; set; }
        public ProjectDashboardScreen(string user, string group, string project)
        {
            uid = user;
            gid = group;
            pid = project;

            InitializeComponent();
            RenderAPI(pid);
        }




        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new LoginScreen());
        }

        private async void TapGestureRecognizer_Tapped(object sender, ItemTappedEventArgs e)
        {


            await Navigation.PushAsync(new ProjectDetailScreen(pid));


        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskScreen(uid, gid, pid));
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamScreen(pid));
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TempPage(pid));
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

        public async void RenderAPI(string pid)
        {
            var jsonResult = await GetProInfo(pid);
            list = JsonConvert.DeserializeObject<List<ProjectInfo>>(jsonResult);
            foreach(var item in list)
            {
                pdata.projectName = item.projectName;
                pdata.customerName = item.customerName;
            }

            projectname.Text = pdata.projectName;
            customer.Text = pdata.customerName;

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
    }
}
