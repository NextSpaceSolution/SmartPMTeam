using SmartPM.Models;
using System;
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
using SmartPM.Views;

namespace SmartPM.ViewModels
{
    class CreateAccountViewModel : INotifyPropertyChanged
    {
        private CreatAccountModel createAcount = new CreatAccountModel();
        private AuthenModel authen = new AuthenModel();
        public CreatAccountModel CreateAccountModel
        {
            get { return createAcount; }
            set
            {
                createAcount = value;
                OnPropertyChanged();

            }
        }

        public Command SummitCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (string.IsNullOrEmpty(createAcount.acUsername))
                    {
                        App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Username", "Oke");
                    }
                    else if (string.IsNullOrEmpty(createAcount.acPassword))
                    {
                        App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Password", "Oke");
                    }
                    else if (string.IsNullOrEmpty(createAcount.acFirstname))
                    {
                        App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Firstname", "Oke");
                    }
                    else if (string.IsNullOrEmpty(createAcount.acLastname))
                    {
                        App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Lastname", "Oke");
                    }
                    else if (string.IsNullOrEmpty(createAcount.acJobresp))
                    {
                        App.Current.MainPage.DisplayAlert("Notification", "กรุณาใส่ Jobresp", "Oke");
                    }
                    else if (string.IsNullOrEmpty(createAcount.acUserLogged))
                    {
                        App.Current.MainPage.DisplayAlert("Notification", "ต้อง Login ก่อนถึงจะมีสิทธ์สร้าง User ได้", "Oke");
                        App.Current.MainPage = new LoginScreen();
                    }
                    else if (string.IsNullOrEmpty(createAcount.acGroup))
                    {
                        createAcount.acGroup = "10";
                    }
                    else
                    {
                        App.Current.MainPage = new dummyView();
                        createAcount.acUserLogged = authen.Username;
                       // RenderAPICreate();
                    }

                });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async void RenderAPICreate()
        {
            string user = createAcount.acUsername;
            string pass = createAcount.acPassword;
            string fname = createAcount.acFirstname;
            string lname = createAcount.acLastname;
            string userLogged = createAcount.acUserLogged;
            string jobresp = createAcount.acJobresp;
            string usergroup = createAcount.acGroup;

            string json = await ClientCreat(user, pass, fname, lname, jobresp, userLogged, usergroup);
            JObject obj = JObject.Parse(json);
            string msg = (string)obj["msg"];
            if (msg == "Success")
            {
                await App.Current.MainPage.DisplayAlert("Notification", "บันทึกข้อมูลเรียบร้อย", "OK");
                //await Navigation.PopAsync();
            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Notification", "ไม่สามารถบันทึกข้อมูลได้", "Cancle");
            }


        }

        public async Task<string> ClientCreat(string Vusername, string Vpassword, string VFirstname, string VLastname, string Jobresp, string userLogged, string gid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("Vusername", Vusername));
                postData.Add(new KeyValuePair<string, string>("Vpassword", Vpassword));
                postData.Add(new KeyValuePair<string, string>("VFirstname", VFirstname));
                postData.Add(new KeyValuePair<string, string>("VLastname", VLastname));
                postData.Add(new KeyValuePair<string, string>("Jobresp", Jobresp));
                postData.Add(new KeyValuePair<string, string>("userLogged", userLogged));
                postData.Add(new KeyValuePair<string, string>("gid", gid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/UserManagement/AddUserService", content))
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
