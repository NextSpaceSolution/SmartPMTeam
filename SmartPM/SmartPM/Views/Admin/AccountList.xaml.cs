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


namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountList : ContentPage
    {
        private string id { get; set; }
        private string ulogged { get; set; }
        public AccountList(AuthenModel authen)
        {

            InitializeComponent();
            ulogged = authen.Username;
            /*
            if (InternetCheckConnectivity() == true)
               // GetAccount();
            else
                ConnectivityLabel.Text = "เนตหมดหรอ Kruf";

    */


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

        private async void userlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Accountlists = e.Item as AccountModel;
            id = Accountlists.userId;
           

            var page = new dummyView();
            await Navigation.PushAsync(page);
        }
        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }


        public async void GetAccount()
        {

            //Check network status    
            var client = new HttpClient();
            var response = await client.GetAsync("http://192.168.88.200:56086/APIResponseAuthen/GetAllEmp");
            string contactsJson = response.Content.ReadAsStringAsync().Result;
            var list = new List<AccountModel>();
            if (contactsJson != "")
            {
                //Converting JSON Array Objects into generic list   
                list = JsonConvert.DeserializeObject<List<AccountModel>>(contactsJson);
            }
            //Binding listview with server response     
            userlist.ItemsSource = list;

        }

    }
}
