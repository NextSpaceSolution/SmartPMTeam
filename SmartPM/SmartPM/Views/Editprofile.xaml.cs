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
    public partial class Editprofile : ContentPage
    {

        private AuthenModel userAccount = new AuthenModel();

        AccountModel acc = new AccountModel();

        private string  uid;
        public Editprofile(string id)
        {
            InitializeComponent();
            uid = id;
            RenderAPI(uid);
        }

        public async void changepic(object sender, EventArgs e)
        {

        }

        public async void submit(object sender, EventArgs e)
        {
            acc.firstname = firstname.Text;
            acc.lastname = lastname.Text;
            acc.jobResponsible = jobResponsible.Text;
            acc.userTel = userTel.Text;
            acc.lineId = lineId.Text;
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }
        public async void RenderAPI(string id)
        {
            string jsonResult = await Edit(id);
            JObject dataemp = JObject.Parse(jsonResult);

            acc.firstname = (string)dataemp["firstname"];
            acc.lastname = (string)dataemp["lastname"];
            acc.jobResponsible = (string)dataemp["jobResponsible"];
            acc.userTel = (string)dataemp["userTel"];
            acc.lineId = (string)dataemp["lineId"];

            BindingContext = acc;

        }

        public async Task<string> Edit(string id)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", id));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107  :56086/UserManagement/Edit", content))
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