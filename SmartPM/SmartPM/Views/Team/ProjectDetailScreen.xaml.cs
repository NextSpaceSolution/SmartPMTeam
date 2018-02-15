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
using System.Globalization;

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
                    /*if (pdata.actualStart == null)
                    {

                        DateTime? temp = DateTime.ParseExact("0000-00-00T00:00:00", "yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture);
                        pdata.actualStart = temp;
                    }*/


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
                    pdata.variant = item.variant.ToString();
                    pdata.projectStatus = item.projectStatus;
                    pdata.note = item.note;


                }
            }

            if (pdata.projectStatus == null)
            {
                pdata.projectStatus = "N/A";
            }
           /*if (pdata.projectStart == null)
            {
                
                //string temp = "0000-00-00T00:00:00.00";
                DateTime strDate = DateTime.ParseExact("0000-00-00T00:00:00.00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                pdata.projectStart = strDate;
            }
            if (pdata.projectEnd == null)
            {
                string temp = "0000 - 00 - 00T00: 00:00.00";
                DateTime strDate = DateTime.ParseExact(temp, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                pdata.projectEnd = strDate;
            }

            if (pdata.projectCreateDate == null)
            {
                string temp = "0000 - 00 - 00T00: 00:00.00";
                DateTime strDate = DateTime.ParseExact(temp, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                pdata.projectCreateDate = strDate;
            }
            if (pdata.projectEditDate == null)
            {
                string temp = "0000 - 00 - 00T00: 00:00.00";
                DateTime strDate = DateTime.ParseExact(temp, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                pdata.projectEditDate = strDate;
            }
            if ( pdata.actualStart == null)
            {
                
                DateTime temp = DateTime.Parse("0000-00-00T00:00:00.00");
                pdata.actualStart = temp;
            }
            if (pdata.actualEnd == null)
            {
                DateTime temp = DateTime.Parse("0000-00-00T00:00:00.00");
                
                pdata.actualEnd = temp;
            }
            if(pdata.note == null)
            {
                pdata.note = "None Description";
            }
            if(pdata.projectEditBy == null)
            {
                pdata.projectEditBy = "N/a";
            }*/
           



            projectname.Text = pdata.projectName;
            projecmanager.Text = pdata.projectManagerfName;
            projectcreateby.Text = pdata.projectCreateBy;
            projectstart.Text = pdata.projectStart.ToString();
            projectend.Text = pdata.projectEnd.ToString();
            note.Text = pdata.note;
            variant.Text = pdata.variant.ToString();

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