using SmartPM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using SmartPM.Models.Timesheet;
using Plugin.Connectivity;
using SmartPM.Views.Team;
using SmartPM.Views;
using System.Collections.ObjectModel;

namespace SmartPM.Views.Team
{
	[XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class TeamDashboardScreen : ContentPage
	{
        TimesheetOneModel objTimesheet = new TimesheetOneModel();
        AccountModel acc = new AccountModel();
        List<AccountModel> lst = new List<AccountModel>();
        private string userId { get; set; }
        private string groupId { get; set; }
   
        public TeamDashboardScreen (string id, string gid)
		{
            
            InitializeComponent ();
            userId = id;
            groupId = gid;



            if (InternetCheckConnectivity() == false)
                Title = "Internet not connected";
            else {
                renderReqUserInfo(userId);
                RenderAPI(userId);
                //Title = userId + gid;
                 }

        }

        private bool InternetCheckConnectivity()
        {

            var isConnected = CrossConnectivity.Current.IsConnected;
            if (isConnected == true)
            {
                return true;
            }
            return false;
        }

        public async void RenderAPI(string id)
        {
            string resultInfo = await getUserInfo(id);
            JObject data = JObject.Parse(resultInfo);
            string uid = (string)data["userId"];
            string gid = (string)data["groupId"];
            string fname = (string)data["firstname"];
            string lname = (string)data["lastname"];
            string jobRes = (string)data["jobResponsible"];
            objTimesheet.userId = uid;
            objTimesheet.groupId = gid;
            objTimesheet.fullName = fname + " " + lname;
            objTimesheet.jobResp = jobRes;

            fullname.Text = objTimesheet.fullName;
            job.Text = objTimesheet.jobResp;


        }


        public async void renderReqUserInfo(string id)
        {
            string resultInfo = await ReqUserInfo(id);
            JObject data = JObject.Parse(resultInfo);
            string uid = (string)data["userId"];
            string gid = (string)data["groupId"];
            string fname = (string)data["firstname"];
            string lname = (string)data["lastname"];
            string jobRes = (string)data["jobResponsible"];
            objTimesheet.userId = uid;
            objTimesheet.groupId = gid;
            objTimesheet.fullName = fname + " " + lname;
            objTimesheet.jobResp = jobRes;

        }

        /*
        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            userId = null;
            await Navigation.PushAsync(new LoginScreen());
        }
        */
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            /*var page = new ProjectList()
            {
                BarBackgroundColor = Color.FromHex("#546E7A")
            };*/
            await Navigation.PushAsync(new ProjectList(userId,groupId));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (groupId == "50")
            {
                var page = new TabbedPage
                {

                    Children =
                    {
                        new ApproveTimesheet(userId),                 
                        new GlobalTimesheet(objTimesheet),
                        new GlobalTimesheetList(userId),


                }
                };
                await Navigation.PushAsync(page);
            }
            else
            { 
                var page = new TabbedPage
                {
                
                    Children =
                    {
                         new GlobalTimesheet(objTimesheet),
                        new GlobalTimesheetList(userId),
                       

                    }
                };
                await Navigation.PushAsync(page);
            }
            
            //UserId,Firstname,Lastname,JobResponsible,GroupId
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfileScreen(userId));
        }

        
        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoodBoard());
        }

        public async Task<string> getUserInfo(string id)
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


        private async void logout(object sender, EventArgs e)
        {


            //userAccount = null;

            App.Current.MainPage = new LoginScreen();
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