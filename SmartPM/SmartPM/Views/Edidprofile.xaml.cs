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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Edidprofile : ContentPage
	{
        private AuthenModel userAccount = new AuthenModel();
        public Edidprofile ()
		{
			InitializeComponent ();

            AccountModel edi = new AccountModel();
            edi.userId = "ABC123";
            edi.username = "XYZ456";
            edi.firstname = "Monkey";
            edi.lastname = "D Luffy";
            edi.jobResponsible = "Analisys and Develop and Testing";
            edi.groupId = "99";
            edi.groupName = "TeamDevelop";
            edi.status = "working";
            edi.picture = "userTemp";
            edi.email = "momon@sadaharu.com";
            edi.tel = "085-555-5555";
            BindingContext = edi;
        }

            


        public async void changepic(object sender, EventArgs e)
        {

        }

        public async void submit(object sender, EventArgs e)
        {

        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
    }
}