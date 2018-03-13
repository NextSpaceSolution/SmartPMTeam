using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartPM.Models.Timesheet;
using SmartPM.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmTimesheet : ContentPage
	{
        ObservableCollection<ConfirmTimesheetModel> tmp = new ObservableCollection<ConfirmTimesheetModel>();
        ObservableCollection<ConfirmTimesheetModel> approve { get; set; }
        ConfirmTimesheetModel approve2 = new ConfirmTimesheetModel();
        string userId;
        public ConfirmTimesheet (string uid)
		{
			InitializeComponent ();
            userId = uid;
            RenderList(uid);
        }

        protected void RefreshPage(object sender, EventArgs e)
        {

            RenderList(userId);
            NewsList.EndRefresh();

        }
        /*
        public async void OnMore(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                var models = (ConfirmTimesheetModel)mi.CommandParameter;
                var temp = await DisplayAlert("Approve  Action", " Confirm to Approve this", "Ok", "Cancle");
                if (temp)
                {
                    string result = await TimesheetService.actionApprove(models.projectId, models.taskId, models.functionId, models.actionId, userId);
                    JObject data = JObject.Parse(result);
                    string context = (string)data["msg"];
                    if (context == "Success")
                    {
                        await DisplayAlert("Notice", "Approve Successfully", "Ok");
                        approve.Remove(models);
                    }
                    else
                    {
                        await DisplayAlert("Notice", "Approve Failer", "Ok");
                    }
                }
            }
            catch { }

        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var models = (ConfirmTimesheetModel)mi.CommandParameter;
            var temp = await DisplayAlert("Reject Action", " Confirm to Reject this", "Ok", "Cancle");
            if (temp)
                approve.Remove(models);

        }

        */


        private async void confirm_action(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as ConfirmTimesheetModel;          
            var page = new SelectActionTimesheet(item);
            await Navigation.PushAsync(page);
        }

        public async void RenderList(string uid)
        {
            string token = "";
            try
            {
                var jsonResult = await TimesheetService.ListConfirm(token, uid);
                approve = JsonConvert.DeserializeObject<ObservableCollection<ConfirmTimesheetModel>>(jsonResult);
                NewsList.ItemsSource = approve;

            }
            catch { }
            
        }
        /*
        public async void RenderGetActionName()
        {
            var actResult = new ObservableCollection<ActionModel>();
            try
            {
                string tempData = await FilterTimesheetService.reqGetAction();
                actResult = JsonConvert.DeserializeObject<ObservableCollection<ActionModel>>(tempData);
                PickerActionNames.ItemsSource = actResult;

            }
            catch
            {
                //await DisplayAlert("Notice", "Fail to load content", "Cancle");
            }
        }*/
    }
}
