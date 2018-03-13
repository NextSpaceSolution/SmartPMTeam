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
	public partial class SelectActionTimesheet : ContentPage
	{
        ObservableCollection<ActionModel> action = new ObservableCollection<ActionModel>();
        ActionModel actionTemp = new ActionModel();
        ConfirmTimesheetModel conf = new ConfirmTimesheetModel();
		public SelectActionTimesheet (ConfirmTimesheetModel model)
		{
			InitializeComponent ();

            conf.projectId = model.projectId;
            conf.taskId = model.taskId;
            conf.functionId = model.functionId;
            conf.timeNumber = model.timeNumber;
            conf.userId = model.userId;
            RenderGetAction();
		}

        protected void RefreshPage(object sender, EventArgs e)
        {

            RenderGetAction();
            NewsList.EndRefresh();

        }
        public async void RenderGetAction()
        {
            var result = await FilterTimesheetService.reqGetAction();
            action = JsonConvert.DeserializeObject<ObservableCollection<ActionModel>>(result);
            NewsList.ItemsSource = action;
        }

        public async void OnMore(object sender, EventArgs e)
        {/*
            try
            {
                var mi = ((MenuItem)sender);
                var models = (ApproveTimesheetModels)mi.CommandParameter;
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
            */
        }
        public async void OnDelete(object sender, EventArgs e)
        {/*
            var mi = ((MenuItem)sender);
            var models = (ApproveTimesheetModels)mi.CommandParameter;
            var temp = await DisplayAlert("Reject Action", " Confirm to Reject this", "Ok", "Cancle");
            if (temp)
                approve.Remove(models);
*/
        }

        private async void action_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                var lister = e.SelectedItem as ActionModel;
                conf.actionId = lister.actionId;
                var temp = await DisplayAlert("Confirm  Action", "Confirm Action " + lister.actionName, "Ok", "Cancle");
                if (temp)
                {

                    string result = await TimesheetService.actionConfirm(conf.timeNumber, conf.projectId, conf.taskId, conf.functionId, conf.actionId, conf.userId);
                    JObject data = JObject.Parse(result);
                    string context = (string)data["msg"];
                    if (context == "true")
                    {
                        await DisplayAlert("Notice", "Confirm Action Successfully", "Ok");
                        action.Remove(lister);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Notice", "Confirm Action Failer", "Ok");
                    }
                }
            }
            catch { }

            // DisplayAlert("Item Selected", "Confirm to Approve " + approve2.functionId.ToString(), "Ok", "Cancle");
            // DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");

        }
    }
}