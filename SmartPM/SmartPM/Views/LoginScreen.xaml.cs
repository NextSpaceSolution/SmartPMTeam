using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartPM.Models;
using SmartPM.Views.Team;
using Newtonsoft.Json.Linq;
using SmartPM.Services;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using SmartPM.Helpers;
using System.Net.Http;
using System.Net;
using SmartPM.Helpers;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginScreen : ContentPage
    {
        private AuthenModel userAccount = new AuthenModel();
        public LoginScreen()
        {

            InitializeComponent();
            MainUser.Text = Settings.UserName;
            MainPassword.Text = Settings.PassWord;


        }

        public void btn_ClickSiginEx(object sender, EventArgs e)
        {
            userAccount.Username = MainUser.Text;
            userAccount.Password = MainPassword.Text;

            if (string.IsNullOrWhiteSpace(userAccount.Username))
            {
                App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Username", "Oke");
                MainUser.Focus();
            }
            else if (string.IsNullOrWhiteSpace(userAccount.Password))
            {
                App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Password", "Oke");
                MainPassword.Focus();
            }
            else
            {
                // App.Current.MainPage = new NavigationPage(new AdminDashboard(userAccount));
                //RenderAPIauthen(userAccount.Username, userAccount.Password);

                if (rememberSwitch.IsToggled)
                {
                    Settings.UserName = MainUser.Text;
                    Settings.PassWord = MainPassword.Text;
                    //App.Current.MainPage = new dummyParamether(MainUser.Text, MainPassword.Text);
                    RenderAPIauthen(userAccount.Username, userAccount.Password);
                }
                else
                {
                    Settings.UserName = string.Empty;
                    Settings.PassWord = string.Empty;
                    Settings.Group = string.Empty;
                    Settings.UserId = string.Empty;
                    //App.Current.MainPage = new dummyParamether(MainUser.Text, MainPassword.Text);
                   // App.Current.MainPage = new NavigationPage(new ApproveTimesheet(uid)) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
                
                }
            }

        }

        public async void RenderAPIauthen(string username, string password)
        {
            try
            {
                string json = await Authentication.AuthenRequest(username, password);
                JObject obj = JObject.Parse(json);
                bool check = (bool)obj["success"];
                string user = (string)obj["username"];
                if (check == true)
                {
                    string jsonid = await Authentication.GetId(user);
                    JObject obj2 = JObject.Parse(jsonid);
                    string userid = (string)obj2["userId"];

                    Settings.UserId = userid;

                    string jsonGid = await Authentication.GetGroupId(userid);
                    JObject obj3 = JObject.Parse(jsonGid);
                    string groupid = (string)obj3["groupId"];

                    Settings.Group = groupid;

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
                        App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
                      
                    }
                    else
                    {
                        // App.Current.MainPage = new AdminDashboard(AuthenModel);
                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "Error Login", "Okay");
                }
            }
            catch
            {
                await DisplayAlert("Notification","Fail Connect to Server", "Cancle");
            }
        }
       

    }
}