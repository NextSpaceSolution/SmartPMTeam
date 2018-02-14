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


namespace SmartPM.Views.Team
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectDetailScreen : ContentPage
    {
        private AuthenModel userAccount = new AuthenModel();

        AProjectList pdata = new AProjectList();
        private string pid;


        public ProjectDetailScreen(string id)
        {
            InitializeComponent();

            pid = id;
            RenderAPI(pid);

            AProjectList list = new AProjectList();
            
                list.projectName = "dummyProject XXX";
                list.projectManager = "dummyManager001";
                list.projectStart = "dummyStartDate";
                list.projectEnd = "dummyEndDate";
                list.projectCost = "10,000,000 Baht";
                list.projectCreateBy = "dummyManger001";
                list.customerName = "dummyCustomer";
                list.customerTel = "01-00-0000";
                list.actualStart = "N/A";
                list.actualEnd = "N/A";
                list.note = "dummy Text";
                list.variant = "N/A";
                list.projectStatus = "N/A";

                    
            
            BindingContext = list;
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

        public async void RenderAPI(string pid)
        {
            string jsonResult = await FilterProject(pid);
            JObject datapro = JObject.Parse(jsonResult);

            pdata.projectNumber = (string)datapro["projectNumber"];
            pdata.projectManager = (string)datapro["projectManager"];
            pdata.projectStart = (string)datapro["projectStart"];
            pdata.projectEnd = (string)datapro["projectEnd"];
            pdata.projectCost = (string)datapro["projectCost"];
            pdata.projectCreateBy = (string)datapro["projectCreateBy"];
            pdata.customerName = (string)datapro["customerName"];
            pdata.customerTel = (string)datapro["customerTel"];
            pdata.actualStart = (string)datapro["actualStart"];
            pdata.actualEnd = (string)datapro["actualEnd"];
            pdata.note = (string)datapro["note"];
            pdata.variant = (string)datapro["variant"];
            pdata.projectStatus = (string)datapro["projectStatus"];

            BindingContext = pdata;

        }
        public async Task<string> FilterProject(string pid)
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
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest2/FilterProject", content))
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