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

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfileScreen : ContentPage
    {

        private AuthenModel userAccount = new AuthenModel();
        AccountModel acc = new AccountModel();
        List<AccountModel> lst = new List<AccountModel>();
        public string user;
        public UserProfileScreen(string id)
        {
            InitializeComponent();

            user = id;
            RenderUser(id);
            RenderGroup(id);

            /* AccountModel acc = new AccountModel();
             acc.userId = "ABC123";
             acc.username = "XYZ456";
             acc.firstname = "Monkey";
             acc.lastname = "D Luffy";
             acc.jobResponsible = "Analisys and Develop and Testing";
             acc.userTel = "085-555-5555";
             acc.lineId = "line.me";
             acc.status = "working";
             BindingContext = acc;*/
        }


        public async void RenderUser(string id)
        {
            string resultInfo = await getUserInfo(id);
            JObject data = JObject.Parse(resultInfo);
            string uid = (string)data["userId"];
            string fname = (string)data["firstname"];
            string lname = (string)data["lastname"];
            string job = (string)data["jobResponsible"];
            string tel = (string)data["userTel"];
            string line = (string)data["lineId"];
            string status = (string)data["status"];
        }

        public async void RenderGroup(string id)
        {
            string resultInfo = await GetGroupId(id); 
             JObject data = JObject.Parse(resultInfo);
            string gid = (string)data["groupId"];
        }



        public async void Button_click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Editprofile(""));
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

        public async Task<string> GetGroupId(string id)
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
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest/GetGroupId", content))
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
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest/GetUserInfo", content))
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