 
using SmartPM.Models;
using System;
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
using SmartPM.Views;
using SmartPM.Views.Team;
using SmartPM.Views.PM;
using SmartPM.Views.Admin;

namespace SmartPM.ViewModels
{
    public class AuthenViewModel : INotifyPropertyChanged
    {
        private AuthenModel userAccount = new AuthenModel();     
        public AuthenModel AuthenModel
        {
            get { return userAccount; }
            set
            {
                userAccount = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand
        {
            get
            {
                return new Command(() =>
               {
                   if (string.IsNullOrEmpty(userAccount.Username))
                   {
                       App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Username", "Oke");
                   }
                   else if (string.IsNullOrEmpty(userAccount.Password))
                   {
                       App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Password", "Oke");
                   }
                   else
                   {
                      // App.Current.MainPage = new NavigationPage(new AdminDashboard(userAccount));
                       RenderAPIauthen(userAccount.Username, userAccount.Password);
                   }
                   
               });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void RenderAPIauthen(string username, string password)
        {
            string json = await AuthenRequest(username, password);
            JObject obj = JObject.Parse(json);
            bool check = (bool)obj["success"];
            string user = (string)obj["username"];
            if (check == true)
            {
                string jsonid = await GetId(user);
                JObject obj2 = JObject.Parse(jsonid);
                string userid = (string)obj2["userId"];

                string jsonGid = await GetGroupId(userid);
                JObject obj3 = JObject.Parse(jsonGid);
                string groupid = (string)obj3["groupId"];

                if (groupid == "99")
                {
                    var page = new AdminDashboard();
                    NavigationPage.SetHasBackButton(page, false);
                    App.Current.MainPage = new NavigationPage(page);
                    
               

                }
              
                else if (groupid == "10" || groupid == "50")
                {

                    var page = new TeamDashboardScreen(userid, groupid);
                    NavigationPage.SetHasBackButton(page, false);
                    App.Current.MainPage = new NavigationPage(page);
                    /*
                    var page = new TeamMainScreen();
                    NavigationPage.SetHasBackButton(page, false);
                    App.Current.MainPage = new NavigationPage(page);
                    /*
                    App.Current.MainPage = new TabbedPage
                    {
                        Children = {
                            new TeamDashboardScreen(),
                            new TeamProfileScreen()
                        }
                    };*/
                }
                else
                {
                    App.Current.MainPage = new AdminDashboard(AuthenModel);
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Notification", "Error Login", "Okay");
            }
        }

        public async Task<string> AuthenRequest(string username, string password)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("eusername", username));
                postData.Add(new KeyValuePair<string, string>("epassword", password));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
<<<<<<< HEAD
                   // client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest/Authen", content))
=======
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest/Authen", content))
>>>>>>> ae80719abb4ed8d1b8713c53873450e560fada70
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

        public async Task<string> GetId(string user)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("user", user));


                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest/GetId", content))
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

        public async Task<string> GetGroupId(string gid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", gid));

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

    }
}
