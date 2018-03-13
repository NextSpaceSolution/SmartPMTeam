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
	public partial class ApproveTimesheet : ContentPage
	{
        ObservableCollection<ApproveTimesheetModels> tmp = new ObservableCollection<ApproveTimesheetModels>();
        ObservableCollection<ApproveTimesheetModels> approve { get; set; }
        ApproveTimesheetModels approve2 = new ApproveTimesheetModels();
        string userId ;
        public ApproveTimesheet (string uid)
		{
			InitializeComponent ();
            userId = uid;
            RenderApproveTimeesheet(uid);
            
        }

        protected void RefreshPage(object sender, EventArgs e)
        {
         
                RenderApproveTimeesheet(userId);
                NewsList.EndRefresh();
         
        }
        public async void OnMore(object sender, EventArgs e)
        {
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
                    if (context == "true")
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
            var models = (ApproveTimesheetModels)mi.CommandParameter;
            var temp = await DisplayAlert("Reject Action", " Confirm to Reject this", "Ok", "Cancle");
            if(temp)
                approve.Remove(models);

        }

    


        private async void approve_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {



                if (e.SelectedItem == null)
                {
                    return;
                }
                var lister = e.SelectedItem as ApproveTimesheetModels;
                approve2.projectId = lister.projectId;
                approve2.taskId = lister.taskId;
                approve2.functionId = lister.functionId;
                approve2.actionId = lister.actionId;

                var temp = await DisplayAlert("Approve  Action", " Confirm to Approve this", "Ok", "Cancle");
                if (temp)
                {

                    string result = await TimesheetService.actionApprove(approve2.projectId, approve2.taskId, approve2.functionId, approve2.actionId, userId);
                    JObject data = JObject.Parse(result);
                    string context = (string)data["IsComplete"];
                    if (context == "true")
                    {
                        await DisplayAlert("Notice", "Approve Successfully", "Ok");
                        approve.Remove(lister);
                    }
                    else
                    {
                        await DisplayAlert("Notice", "Approve Failer", "Ok");
                    }
                }
            }
            catch { }

           // DisplayAlert("Item Selected", "Confirm to Approve " + approve2.functionId.ToString(), "Ok", "Cancle");
           // DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");

        }

        public async void RenderApproveTimeesheet(string uid)
        {
            try
            {
                var jsonResult = await TimesheetService.ListApprove(uid);
                approve = JsonConvert.DeserializeObject<ObservableCollection<ApproveTimesheetModels>>(jsonResult);
                foreach (var item in approve)
                {
                    tmp.Add(new ApproveTimesheetModels()
                    {
                        submitDate = item.submitDate,
                        projectId = item.projectId,
                        taskId = item.taskId,
                        functionId = item.functionId,
                        actionId = item.actionId,
                        userId = item.userId,
                        // timeStart = item.timeStart.ToString(),
                        // timeEnd = item.timeEnd.ToString()
                    });
                }
            }
            catch {
                //await DisplayAlert("Notice","Fail","Cancle");
            }
            NewsList.ItemsSource = approve;
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