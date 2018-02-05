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
        public UserProfileScreen()
        {
            InitializeComponent();

            AccountModel acc = new AccountModel();
            acc.userId = "ABC123";
            acc.username = "XYZ456";
            acc.firstname = "Monk";
            acc.lastname = "D Luffy";
            acc.jobResponsible = "Analisys and Develop and Testing";
            acc.groupId = "99";
            acc.groupName = "TeamDevelop";
            acc.status = "working";
            acc.picture = "userTemp";

            BindingContext = acc;
        }


        public async void Button_click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new dummyView());
        }


    }
}