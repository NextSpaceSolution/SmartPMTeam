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
        public UserProfileScreen()
        {
            InitializeComponent();

            AccountModel acc = new AccountModel();
            acc.userId = "ABC123";
            acc.username = "XYZ456";
            acc.firstname = "Monkey";
            acc.lastname = "D Luffy";
            acc.jobResponsible = "Analisys and Develop and Testing";
            acc.groupId = "99";
            acc.groupName = "TeamDevelop";
            acc.status = "working";
            acc.picture = "userTemp";
            acc.email = "momon@sadaharu.com";
            acc.tel = "085-555-5555";
            BindingContext = acc;
        }


        public async void Button_click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Edidprofile());
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

    }
}