using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SmartPM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TempPage : ContentPage
    {
        private ObservableCollection<TempTimelineModel> _items = new ObservableCollection<TempTimelineModel>();
       
        public ObservableCollection<TempTimelineModel> Items
        {
            get { return _items; }
            set => _items = value;

        }


        public TempPage()
        {
            InitializeComponent();
            Gettimeline();
        

        }

 
        public async void Gettimeline()
        {

            //Check network status    
            var client = new HttpClient();
            var response = await client.GetAsync("http://192.168.88.200:56086/APIRest2/Gettimeline");
            string contactsJson = response.Content.ReadAsStringAsync().Result;
            if (contactsJson != "")
            {
                //Converting JSON Array Objects into generic list   
                _items = JsonConvert.DeserializeObject<ObservableCollection<TempTimelineModel>>(contactsJson);
            }
            //Binding listview with server response     
            listItems.ItemsSource = Items;

        }
    }
	
}