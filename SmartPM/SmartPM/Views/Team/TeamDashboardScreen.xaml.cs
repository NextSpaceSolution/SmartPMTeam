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
using Plugin.Connectivity;
using SmartPM.Views.Team;
using SmartPM.Views;
using System.Collections.ObjectModel;

namespace SmartPM.Views.Team
{
	[XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class TeamDashboardScreen : ContentPage
	{

        string a;
        string b;
        string c;

        string UserId;
        string Firstname;
        string Lastname;
        string JobResponsible;
        string GroupId;
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
                RenderUserInfo(userId);
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

        public async void RenderUserInfo(string userId)
        {
            string _userResult = await getUserInfo(userId);
            JObject data = JObject.Parse(_userResult);
            UserId = userId;
            Firstname = (string)data["firstname"];
            Lastname = (string)data["lastname"];
            JobResponsible = (string)data["jobResponsible"];
            GroupId = groupId;
            
           // _userInfo = JsonConvert.DeserializeObject<ObservableCollection<UserInfo>>(_userResult);
     
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
            await Navigation.PushAsync(new ProjectList("100017", "50"));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GlobalTimesheet());
            //UserId,Firstname,Lastname,JobResponsible,GroupId
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserProfileScreen());
        }

        /*
        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new dummyView());
        }*/

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

<<<<<<< HEAD
            //userAccount = null;
=======
            
>>>>>>> 064ebc055a958c1e94045921145701ee3eb8b7b1
            App.Current.MainPage = new LoginScreen();
        }
    }
}