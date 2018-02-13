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
using SmartPM.Views;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GlobalTimesheetList : ContentPage
	{
		public GlobalTimesheetList ()
		{
            InitializeComponent();
            //RenderReqTimesheetList();
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

        public async void RenderReqTimesheetList()
        {

            var list = new List<AProjectList>();
            var jsonResult = await getProject();
            list = JsonConvert.DeserializeObject<List<AProjectList>>(jsonResult);
            projectlist.ItemsSource = list;
            this.IsBusy = false;



        }
        public async Task<string> getProject()
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/getProject", content))
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