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
using SmartPM.Services;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfileScreen : ContentPage
    {

        private AuthenModel userAccount = new AuthenModel();
        AccountModel acc = new AccountModel();
        List<AccountModel> lst = new List<AccountModel>();
        public string user = "";
        public UserProfileScreen(string id)
        {
            InitializeComponent();
            user = id;
            //labeluserID.Text = id;

            try
            {
                RenderUser(user);
            }
             

            catch (Exception e)
            {
                DisplayAlert("Error", e.Message, "OK");
            }
}

       public async void RenderUser(string id )
        {

            try
            {
                string resultInfo = await AccountsService.DetailsM(id);
                JObject data = JObject.Parse(resultInfo);
                acc.userId = (string)data["userId"];
                acc.username = (string)data["username"];
                acc.firstname = (string)data["firstname"];
                acc.lastname = (string)data["lastname"];
                acc.jobResponsible = (string)data["jobResponsible"];
                acc.userTel = (string)data["userTel"];
                acc.lineId = (string)data["lineId"];
                acc.status = (string)data["status"];

                if (acc.userId == null)
                {
                    acc.userId = "No userID";
                }
                if (acc.username == null)
                {
                    acc.username = "No username";
                }
                if (acc.firstname == null)
                {
                    acc.firstname = "No firstname";
                }
                if (acc.lastname == null)
                {
                    acc.lastname = "No lastname";
                }
                if (acc.userTel == null)
                {
                    acc.userTel = "No Tel.";
                }
                if (acc.lineId == null)
                {
                    acc.lineId = "No lineID";
                }
                if (acc.jobResponsible == null)
                {
                    acc.jobResponsible = "No jobResponsible";
                }
                if (acc.status == null)
                {
                    acc.status = "No status";
                }
                else
                {

                }
            }

            catch (Exception e)
            {
                DisplayAlert("Error", e.Message, "OK");
            }




            labeluserID.Text = acc.userId;
            userName.Text = acc.username;
            firstName.Text = acc.firstname;
            lastName.Text = acc.lastname;
            tele.Text = acc.userTel;
            Line.Text = acc.lineId;
            jobres.Text = acc.jobResponsible;
            Status.Text = acc.status;



        }



        public async void Button_click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Editprofile(acc));
        }

       private async void logout(object sender, EventArgs e)
        {

            /*userAccount = null;
            App.Current.MainPage = new LoginScreen();*/
        }
        
       


        

    }
}