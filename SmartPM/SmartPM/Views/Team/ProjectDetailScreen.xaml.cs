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

        ProjectInfo pdata = new ProjectInfo();
        List<ProjectInfo> list = new List<ProjectInfo>();
        private string pid;


        public ProjectDetailScreen(string id)
        {
            InitializeComponent();
            Title = pid;

            pid = id;
            RenderAPI(pid);

           /* AProjectList list = new AProjectList();
            
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

                    
            
            BindingContext = list;*/
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

        public async void RenderAPI(string pid)
        {
            var jsonResult = await GetProInfo(pid);
            list = JsonConvert.DeserializeObject<List<ProjectInfo>>(jsonResult);
            if (list == null)
            {
                DisplayAlert("NNNN", "Novalue", "OK");
            }
            else
            {
                foreach (var item in list)
                {
                    pdata.projectName = item.projectName;
                    pdata.projectManagerfName = item.projectManagerfName + "  " + item.projectManagerlName;
                    pdata.projectStart = item.projectStart;
                    pdata.projectEnd = item.projectEnd;
                    pdata.actualStart = item.actualStart;
                    pdata.actualEnd = item.actualEnd;
                    pdata.projectCreateBy = item.projectCreateBy;
                    pdata.projectCost = item.projectCost;
                    pdata.customerName = item.customerName;
                    pdata.customerTel = item.customerTel;
                    pdata.variant = item.variant;
                    pdata.projectStatus = item.projectStatus;
                    pdata.note = item.note;


                }
            }

            /* projectname.Text = pdata.projectName;
             projecmanager.Text = pdata.projectManagerfName;
             projectcreateby.Text = pdata.projectCreateBy;
             projectstart.Text = pdata.projectStart.ToString();
             projectend.Text = pdata.projectEnd.ToString();
             note.Text = pdata.note;
             variant.Text = pdata.variant.ToString();
             projectstatus.Text = pdata.projectStatus.ToString();
             actualStart.Text = pdata.actualStart.ToString();
             actualend.Text = pdata.actualEnd.ToString();
             projectcost.Text = pdata.projectCost.ToString();
             customername.Text = pdata.customerName;
             customertel.Text = pdata.customerTel;*/

            projectname.Text = pdata.projectName;
            projecmanager.Text = pdata.projectManagerfName;
            projectcreateby.Text = pdata.projectCreateBy;
            projectstart.Text = pdata.projectStart.ToString();
            projectend.Text = pdata.projectEnd.ToString();
            note.Text = pdata.note;
            variant.Text = pdata.variant.ToString();
            if (pdata.projectStatus == null)
            {
                pdata.projectStatus = "--";
            }
            else
            {
                
            }
            projectstatus.Text = pdata.projectStatus;
            actualStart.Text = pdata.actualStart.ToString();
            actualend.Text = pdata.actualEnd.ToString();
            projectcost.Text = pdata.projectCost.ToString();
            customername.Text = pdata.customerName;
            customertel.Text = pdata.customerTel;


            this.IsBusy = false;

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