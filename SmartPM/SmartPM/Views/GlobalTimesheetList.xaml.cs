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
using Plugin.Connectivity;
using SmartPM.Views;
using SmartPM.Services;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlobalTimesheetList : ContentPage
    {
        ObservableCollection<TimesheetList> timeList = new ObservableCollection<TimesheetList>();
     
        string uid;
		public GlobalTimesheetList (string id)
		{
            InitializeComponent();
            //RenderReqTimesheetList();
            uid = id;
            RenderReqTimesheetList(uid);
            
        }

        protected virtual void OnAppearing()
        {
            base.OnAppearing();
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
            try
            {
                var jsonResult = await TimesheetService.reqTimesheet(uid);
                timeList = JsonConvert.DeserializeObject<ObservableCollection<TimesheetList>>(jsonResult);
                TimesheetlistItem.ItemsSource = timeList;
                this.IsBusy = false;
            }
            catch
            {
                await DisplayAlert("Notice", "Fail to load Content", "Cancle");
            }

        }
        
    }
}