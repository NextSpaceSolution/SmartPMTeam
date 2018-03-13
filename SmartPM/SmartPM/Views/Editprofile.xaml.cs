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
using SmartPM.Models;
using Xamarin.Forms.Xaml;
using System;
using SmartPM.Services;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Editprofile : ContentPage
    {

        AuthenModel userAccount = new AuthenModel();

        AccountModel acc = new AccountModel();

        private string uid;
        public Editprofile(AccountModel acc)
        {
            InitializeComponent();
            uid = acc.userId;
            

            firstname.Text = acc.firstname ;
            lastname.Text = acc.lastname ;
            jobResponsible.Text = acc.jobResponsible;
            userTel.Text = acc.userTel;
            lineId.Text = acc.lineId;


        }

        public async void changepic(object sender, EventArgs e)
        {

        }

        public async void submit(object sender, EventArgs e)
        {
            if (newpass != null)
            {
                if (oldpass.Text == acc.password)
                {
                   // acc.password = newpass.Text;
                    RenderAPI();
                }

                else
                {
                    DisplayAlert("Error", "Old password not match", "OK");
                }
            }
            else
            {
                RenderAPI();
            }
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
        public async void RenderAPI()
        {

            
            acc.firstname = firstname.Text;
            acc.lastname = lastname.Text;
            acc.jobResponsible = jobResponsible.Text;
            acc.userTel = userTel.Text;
            acc.lineId = lineId.Text;



        }



    }
}