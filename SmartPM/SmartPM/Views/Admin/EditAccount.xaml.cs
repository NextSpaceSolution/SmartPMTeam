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
using Plugin.Connectivity;
using SmartPM.Services;


namespace SmartPM.Views.Admin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAccount : ContentPage
	{
        AccountEditModel _DataContex = new AccountEditModel();
        private string uid;
        private string ulogged;
        private string ugroup;
        private string ustat;
        
		public EditAccount (string id, string ulog)
		{

			InitializeComponent ();
            uid = id;
            ulogged = ulog;

            group.Items.Add("Admin");
            group.Items.Add("ProjectManager");
            group.Items.Add("TeamDeveloper");

            stataccount.Items.Add("Active");
            stataccount.Items.Add("DeActived");

            if (InternetCheckConnectivity() == false)
                CheckConnectivityLabel.Text = "Internet not connect";
            else
                RenderAPIAccountEdit(uid);



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

        public async void RenderAPIAccountEdit(string id)
        {
            try
            {
                string jsonResult = await AccountsService.DetailsServices(id);
                JObject dataemp = JObject.Parse(jsonResult);

                _DataContex.username = (string)dataemp["username"];
                _DataContex.password = (string)dataemp["password"];
                _DataContex.firstname = (string)dataemp["firstname"];
                _DataContex.lastname = (string)dataemp["lastname"];
                _DataContex.jobResponsible = (string)dataemp["jobResponsible"];
                _DataContex.status = (string)dataemp["status"];
                _DataContex.groupName = (string)dataemp["groupName"];

                BindingContext = _DataContex;
            }
            catch { }
        }



        private void group_SelectedIndexChanged(object sender, EventArgs e)
        {
            var getGroupId = group.Items[group.SelectedIndex];
            if (getGroupId == "Admin")
            {
                ugroup = "99";
            }
            else if (getGroupId == "ProjectManager")
            {
                ugroup = "50";
            }
            else if (getGroupId == "TeamDeveloper")
            {
                ugroup = "10";
            }
            else
                ugroup = "10";
        }
        private void status_SelectedIndexChanged(object sender, EventArgs e)
        {
            var getStat = stataccount.Items[stataccount.SelectedIndex];
            if (getStat == "Active")
                ustat = "A";
            else if (getStat == "DeActived")
                ustat = "D";
            else
                ustat = "A";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            _DataContex.username = username.Text;
            _DataContex.password = password.Text;
            _DataContex.firstname = firstname.Text;
            _DataContex.lastname = lastname.Text;
            _DataContex.jobResponsible = jobResp.Text;
            _DataContex.userEditBy = ulogged;
            _DataContex.status = ustat;
            _DataContex.groupId = ugroup;

            try
            {
                string json2 = await AccountsService.EditService(uid, _DataContex.username, _DataContex.password, _DataContex.firstname, _DataContex.lastname, _DataContex.jobResponsible, _DataContex.userEditBy, _DataContex.status, _DataContex.groupId);
                JObject obj2 = JObject.Parse(json2);
                string msg = (string)obj2["msg"];
                if (msg == "Success")
                {
                    this.IsBusy = false;
                    await DisplayAlert("Notification", "บันทึกข้อมูลเรียบร้อย", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    this.IsBusy = false;
                    await DisplayAlert("Notification", "ไม่สามารถบันทึกข้อมูลได้", "Cancle");
                }
            }
            catch { }

        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

       
    }
}