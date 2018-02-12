using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPM.Views;
using SmartPM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoItem : ContentPage
	{
        private DateTime d { get; set; }
        private string h { get; set; }
        private string de { get; set; }
		public TodoItem ()
		{
			InitializeComponent ();
		}

        public async void OnSaveActivated(object sender, EventArgs e)
        {
            string pid = "100001";
            d = date_pick.Date;
            h = header.Text;
            de = descrips.Text;
            await CreateTimeline(pid, d, h, de);
            await Navigation.PopAsync();
        }

        public async Task<string> CreateTimeline(string pid, DateTime tdate, string header, string note)
        {
            string sdate;
            sdate = tdate.ToString("dd-MM-yyyy");
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("pid", pid));
                postData.Add(new KeyValuePair<string,string>("tdate", sdate));
                postData.Add(new KeyValuePair<string, string>("header", header));
                postData.Add(new KeyValuePair<string, string>("note", note));
     

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/Addtimeline", content))
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