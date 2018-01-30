﻿using System;
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
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SmartPM.Views;


namespace SmartPM.Views.Admin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAccount : ContentPage
	{
        private AuthenModel authenModel;
        AccountEditModel _DataContex = new AccountEditModel();
        private string uid;
        private string ugroup;
        private string ustat;
        
		public EditAccount (string id)
		{

			InitializeComponent ();
            uid = id;

            if (InternetCheckConnectivity() == false)
                DisplayAlert("Notification", "ไม่ได้เชื่อมต่อ Internet", "CanSel");
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
            string jsonResult = await DetailsServices(id);
            List<AccountEditModel> accountDetail = new List<AccountEditModel>();
            accountDetail = JsonConvert.DeserializeObject<List<AccountEditModel>>(jsonResult);

            foreach (var dataValue in accountDetail)
            {
                _DataContex.username = dataValue.username;
                _DataContex.password = dataValue.password;
                _DataContex.firstname = dataValue.firstname;
                _DataContex.lastname = dataValue.lastname;
                _DataContex.jobResponsible = dataValue.jobResponsible;
                _DataContex.status = dataValue.status;
                _DataContex.GroupName = dataValue.GroupName;
            }
            BindingContext = _DataContex;

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
            _DataContex.userEditBy = authenModel.Username;
            _DataContex.status = ustat;
            _DataContex.groupId = ugroup;


            string json2 = await EditService(uid, _DataContex.username, _DataContex.password, _DataContex.firstname, _DataContex.lastname, _DataContex.jobResponsible, _DataContex.userEditBy, _DataContex.status, _DataContex.groupId);
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

        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        public async Task<string> EditService(string id, string Vusername, string Vpassword, string VFirstname, string VLastname, string Jobresp, string userLogged, string stat, string gid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", id));
                postData.Add(new KeyValuePair<string, string>("Vusername", Vusername));
                postData.Add(new KeyValuePair<string, string>("Vpassword", Vpassword));
                postData.Add(new KeyValuePair<string, string>("VFirstname", VFirstname));
                postData.Add(new KeyValuePair<string, string>("VLastname", VLastname));
                postData.Add(new KeyValuePair<string, string>("Jobresp", Jobresp));
                postData.Add(new KeyValuePair<string, string>("userLogged", userLogged));
                postData.Add(new KeyValuePair<string, string>("stat", stat));
                postData.Add(new KeyValuePair<string, string>("gid", gid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/UserManagement/EditService", content))
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

        public async Task<string> DetailsServices(string id)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", id));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/UserManagement/DetailsService", content))
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