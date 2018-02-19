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
using SmartPM.Models;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TempPage : ContentPage
    {
        private ObservableCollection<ProjectTImeline> _items = new ObservableCollection<ProjectTImeline>();
        private ObservableCollection<ProjectInfo> data1 = new ObservableCollection<ProjectInfo>();
        ProjectInfo data2 = new ProjectInfo();
        public ObservableCollection<ProjectTImeline> Items
        {
            get { return _items; }
            set => _items = value;

        }


        public TempPage(string id)
        {
            InitializeComponent();
            //Gettimeline();
            string pid = id;
            RenderReqTimelineLog(pid);
            RenderAPI(pid);
        

        }
        public async void RenderAPI(string pid)
        {
            var jsonResult = await GetProInfo(pid);
            data1 = JsonConvert.DeserializeObject<ObservableCollection<ProjectInfo>>(jsonResult);
        
           
                foreach (var item in data1)
                {
                    data2.projectName = item.projectName;
                
                }
            Headerx.Text = data2.projectName;
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
            public async Task<string> GetProInfo(string pid)
            {
                try
                {
                    // This is the postdata
                    var postData = new List<KeyValuePair<string, string>>(2);
                    postData.Add(new KeyValuePair<string, string>("pid", pid));

                    HttpContent content = new FormUrlEncodedContent(postData);

                    using (var client = new HttpClient())
                    {
                        //client.Timeout = new TimeSpan(0, 0, 15);
                        using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest2/getProjectInfo", content))
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
	
