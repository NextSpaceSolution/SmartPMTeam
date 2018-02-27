using SmartPM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Xaml;
using SmartPM.Services;
namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateAccount : ContentPage
	{
        private CreatAccountModel newAccont = new CreatAccountModel();
        private AuthenModel authens = new AuthenModel();
		public CreateAccount (AuthenModel authen)
		{
            InitializeComponent();
            group.Items.Add("Admin");
            group.Items.Add("Project Manager");
            group.Items.Add("Team Developer");
            authens = authen;
        }

        private void group_SelectedIndexChanged(object sender, EventArgs e)
        {
            var getGroupId = group.Items[group.SelectedIndex];
            if (getGroupId == "Admin")
            {
                newAccont.acGroup = "99";
            }
            else if (getGroupId == "ProjectManager")
            {
                newAccont.acGroup = "50";
            }
            else if (getGroupId == "Team Developer")
            {
                newAccont.acGroup = "10";
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            newAccont.acUsername = username.Text;
            newAccont.acPassword = password.Text;
            newAccont.acFirstname = firstname.Text;
            newAccont.acLastname = lastname.Text;
            newAccont.acUserLogged = authens.Username; 
            newAccont.acJobresp = title.Text;


            if (string.IsNullOrEmpty(newAccont.acUsername))
            {
                await App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Username", "Oke");
            }
            else if (string.IsNullOrEmpty(newAccont.acPassword))
            {
                await App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Password", "Oke");
            }
            else if (string.IsNullOrEmpty(newAccont.acFirstname))
            {
                await App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Firstname", "Oke");
            }
            else if (string.IsNullOrEmpty(newAccont.acLastname))
            {
                await App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Lastname", "Oke");
            }
            else if (string.IsNullOrEmpty(newAccont.acJobresp))
            {
                await App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Jobresp", "Oke");
            }
            else if (string.IsNullOrEmpty(newAccont.acUserLogged))
            {
                await App.Current.MainPage.DisplayAlert("Notification", "ต้อง Login ก่อนถึงจะมีสิทธ์สร้าง User ได้", "Oke");
                App.Current.MainPage = new LoginScreen();
            }
            else if (string.IsNullOrEmpty(newAccont.acGroup))
            {
                newAccont.acGroup = "10";
            }
            else
            {
                try
                {
                    string json = await AccountsService.AddRequest(newAccont.acUsername, newAccont.acPassword, newAccont.acFirstname, newAccont.acLastname, newAccont.acJobresp, newAccont.acUserLogged, newAccont.acGroup);
                    JObject obj = JObject.Parse(json);
                    string msg = (string)obj["msg"];
                    if (msg == "Success")
                    {
                        await DisplayAlert("Notification", "บันทึกข้อมูลเรียบร้อย", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {

                        await DisplayAlert("Notification", "ไม่สามารถบันทึกข้อมูลได้", "Cancle");
                    }
                }
                catch { }


            }


        }

       
    }
}