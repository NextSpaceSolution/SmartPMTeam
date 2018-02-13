using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SmartPM.Models.Timesheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TempPage : ContentPage
    {
        private ObservableCollection<ProjectTImeline> _items = new ObservableCollection<ProjectTImeline>();
       
        public ObservableCollection<ProjectTImeline> Items
        {
            get { return _items; }
            set => _items = value;

        }


        public TempPage(string id)
        {
            InitializeComponent();
            //Gettimeline();
            string pid = "100001";
            RenderReqTimelineLog(pid);
        

        }

        public async void RenderReqTimelineLog(string pid)
        {
            var jsonResult = await reqTimelineLog(pid);
            Items = JsonConvert.DeserializeObject<ObservableCollection<ProjectTImeline>>(jsonResult);
            if (Items != null)
                listItems.ItemsSource = Items;
            else
                Title = "ยังไม่มีข้อมูล";
        }

        public async Task<string> reqTimelineLog(string pid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("pid", pid));



                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                   // client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/FilterFunctionLog", content))
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