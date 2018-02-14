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
using SmartPM.Models.Timesheet;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SmartPM.Views.Admin;
using Plugin.Connectivity;
using SmartPM.Views;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlobalTimesheetList : ContentPage
    {
        ObservableCollection<TimesheetList> timeList = new ObservableCollection<TimesheetList>();
        /*{
            new TimesheetList()
            {
                projectName = "CUSCO",
                taskName = "Gettering Requirement",
                jobName = "kickoff meeting",
                recordDate = "15-2-2018"
            },
            new TimesheetList
            {
                 projectName = "MACGEEN",
                taskName = "Gettering Requirement",
                jobName = "kickoff meeting",
                recordDate = "14-2-2018"
            },
            new TimesheetList()
            {
                projectName = "THAILAND ECONOMIC",
                taskName = "Gettering Requirement",
                jobName = "kickoff meeting",
                recordDate = "13-2-2018"
            }
            
        };*/
        string uid;
		public GlobalTimesheetList (string id)
		{
            InitializeComponent();
            //RenderReqTimesheetList();
            uid = id;
            RenderReqTimesheetList(uid);
            
        }

        private void MainSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                TimesheetlistItem.ItemsSource = timeList;
            }
            else
            {               
                TimesheetlistItem.ItemsSource = timeList.Where(n => n.projectName.ToLower().StartsWith(e.NewTextValue.ToLower()));
            }
            /*
            var keyword = MainSearch.Text;
            TimesheetlistItem.ItemsSource =
                timeList.Where(name => name.ToLower().Contains(keyword.ToLower()));
                */
        }
        private async void projectlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            /*
            var Projectlists = e.Item as AProjectList;
            string id = Projectlists.projectName;


            var page = new dummyView();
            //App.Current.MainPage = new NavigationPage(page);
            await Navigation.PushAsync(page);*/
        }
        
        public async void RenderReqTimesheetList(string uid)
        {

            var jsonResult = await reqTimesheet(uid);
            timeList = JsonConvert.DeserializeObject<ObservableCollection<TimesheetList>>(jsonResult);
            TimesheetlistItem.ItemsSource = timeList;
            this.IsBusy = false;

        }
        public async Task<string> reqTimesheet(string uid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                   // client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/listTimesheet", content))
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