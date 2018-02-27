﻿using System;
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
using System.IO;

namespace SmartPM.Views.Team
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectList : ContentPage
    {

        private AuthenModel userAccount = new AuthenModel();

        public   List<AProjectList> pdata =  new List<AProjectList>();
        public string uid { get; set; }
        public string gid { get; set; }
        public string picture { get; set; }
        public string backclr { get; set; }

        List<AProjectList> list = new List<AProjectList>();
        public ProjectList(string id ,string groupid)
        {
            InitializeComponent();

            uid = id;
            gid = groupid;
            RenderProject(uid , gid);

           /*List<AProjectList> list = new List<AProjectList>
            {
                new AProjectList
                {
                    projectNumber = "P100001",
                    projectName = "โปรเจค SpacXcross",
                    projectManager = "Elon Maskie",
                    projectStart = "32 มกราคม 2561 - 32 มกราคม 2580",
                    projectEnd = "0 Days",
                    projectCost = "10,000,000,000 Baht",
                    customerName = "Adobe",
                   backclr = "#4CAF50",
                     picture = "thumTime"
                },

                                    new AProjectList
                {
                    projectNumber = "P100003",
                    projectManager = "Cloee Aisas",
                    projectStart = "25 ตุลาคม 2561 - 32 มกราคม 2580",
                    projectEnd = "0 Days",
                    projectCost = "100,000,000,000,000,000 Baht",
                    customerName = "GooGle",
                    backclr = "#e83030",
                     picture = "thumTime"
                }
            };
            projectlist.ItemsSource = list;*/
        }

        /*private void MainSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = MainSearch.Text;
            projectlist.ItemsSource =
                list.Where(projectNumber.Contains());
        }*/


        public async void RenderProject(string uid , string gid)
        {

            try
            {
                var jsonResult = await FilterProject(uid, gid);
                list = JsonConvert.DeserializeObject<List<AProjectList>>(jsonResult);
                projectlist.ItemsSource = list;
                this.IsBusy = false;
            }
            catch { }



        }
        public async Task<string> FilterProject(string uid ,string  gid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                postData.Add(new KeyValuePair<string, string>("gid", gid));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                   // client.Timeout = new TimeSpan(0, 0, 15);
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

        private async void projectlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Projectlists = e.Item as AProjectList;
            string id = Projectlists.projectNumber;

            


            var page = new ProjectDashboardScreen(uid,gid,id);
            //App.Current.MainPage = new NavigationPage(page);
            await Navigation.PushAsync(page);
        }

        private async void logout(object sender, EventArgs e)
        {

            userAccount = null;
            App.Current.MainPage = new LoginScreen();
        }

    }
}