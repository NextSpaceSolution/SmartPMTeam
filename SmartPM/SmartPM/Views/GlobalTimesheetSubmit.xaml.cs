using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using SmartPM.Models;
using SmartPM.Models.Timesheet;
using Xamarin.Forms.Xaml;
using System;
using Plugin.Connectivity;
using SmartPM.Services;
using System.Collections.ObjectModel;

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GlobalTimesheetSubmit : ContentPage
	{

        TimesheetOnSubmitModel timesheetData = new TimesheetOnSubmitModel();
        TempTimesheetModel tempObj = new TempTimesheetModel();
        TimeSpan myDateResult = DateTime.Now.TimeOfDay;
        TimeSpan tempCurrentTime = TimeSpan.Parse("14:00:00");
        TimeSpan hasCheckMorning = TimeSpan.Parse("13:00:00");
        TimeSpan defaultMorningStarttime = TimeSpan.Parse("09:00:00");
        TimeSpan defaultMorningEndtime = TimeSpan.Parse("13:00:00");
        TimeSpan defaultNoonStarttime = TimeSpan.Parse("14:00:00");
        TimeSpan defaultNoonEndtime = TimeSpan.Parse("18:00:00");
        TimeSpan defaultOneHrs = TimeSpan.Parse("01:00:00");
        TimeSpan defaultEdHrs = TimeSpan.Parse("09:00:00");



        public GlobalTimesheetSubmit (TimesheetOneModel model)
		{
			InitializeComponent ();
            Labelfullname.Text = model.fullName;
            Labeljob.Text = model.jobResp;
            LabelProname.Text = model.projectName;
            Taskname.Text = model.TaskName;
            LableFunction.Text = model.functionName;

            
            SetDefaultTime();

            if (checkConnect() == true)

                RenderGetActionName();              
            else
                Title = "Internet not connect";


            timesheetData.UserId = model.userId;         
            timesheetData.ProjectNumber = model.projectId;
            timesheetData.TaskId = model.taskId;
            timesheetData.FunctionId = model.functionId;


        }

        public bool checkConnect()
        {
            var isConnected = CrossConnectivity.Current.IsConnected;
            if (isConnected == true)
            {
                return true;
            }
            else
                return false;
        }

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
        }


        private void OnActionSelectedIndexChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var modelPicker = (Picker)sender;
            int selectedIndex = modelPicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                var model = (ActionModel)modelPicker.SelectedItem;
                tempObj.actionName = model.actionName;
                timesheetData.ActionId = model.actionId;
            }
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public void SetDefaultTime()
        {
            if (myDateResult <= hasCheckMorning)
            {
                TimesheetStartPick.Time = defaultMorningStarttime;
                TimesheetEndPick.Time = defaultMorningEndtime;
            }
            else
            {
                TimesheetStartPick.Time = defaultNoonStarttime;
                TimesheetEndPick.Time = defaultNoonEndtime;
            }
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            TimeSpan minimunTimeLength = TimeSpan.Parse("09:00:00");
            TimeSpan maximunTimeLength = TimeSpan.Parse("21:00:00");
            string total = string.Empty;
            TimeSpan tempTotal;
            tempObj.tempDate = DateTimesheetPick.Date;
            tempObj.tempSTime = TimesheetStartPick.Time;
            tempObj.tempETime = TimesheetEndPick.Time;

            if (TimesheetStartPick.Time < minimunTimeLength || TimesheetEndPick.Time > maximunTimeLength)
            {
                await DisplayAlert("Notice", "สามารถบันทึกเวลาได้ระหว่าง 09:00 - 21:00 น.", "OK");
            }

            else
            {
                if (TimesheetStartPick.Time > TimesheetEndPick.Time)
                {
                    await DisplayAlert("Notice", "Launch End ต้องมากกว่า Launch Start", "OK");
                }
                else
                {
                    if (TimesheetEndPick.Time.Subtract(TimesheetStartPick.Time) >= defaultEdHrs)
                    {
                        tempTotal = (TimesheetEndPick.Time - TimesheetStartPick.Time) - defaultOneHrs;
                        total = tempTotal.ToString(@"hh\:mm");
                    }
                    else { 
                        tempTotal = TimesheetEndPick.Time - TimesheetStartPick.Time;
                        total = tempTotal.ToString(@"hh\:mm");
                    }


                    tempObj.Strdate = tempObj.tempDate.ToString("dd-MM-yyyy");
                    tempObj.StrdateConcate = tempObj.tempDate.ToString("yyyy-MM-dd");
                    tempObj.StrStime = tempObj.tempSTime.ToString(@"hh\:mm\:ss");
                    tempObj.StrEtime = tempObj.tempETime.ToString(@"hh\:mm\:ss");


                    tempObj.concateStime = tempObj.StrdateConcate + " " + tempObj.StrStime;
                    tempObj.concateEtime = tempObj.StrdateConcate + " " + tempObj.StrEtime;

                    timesheetData.TimeSheetStart = tempObj.concateStime;
                    timesheetData.TimeSheetEnd = tempObj.concateEtime;
                    // string proId = "100001";
                    //string taskId = "100005";
                    //  string uid = "100019";
                    // string fid = "100008";

                    if (string.IsNullOrEmpty(tempObj.actionName))
                    {
                        await DisplayAlert("Notice", "!!! กรุนาเลือก Action", "Ok");

                    }
                    else
                    {
                        try
                        {
                            var resultTotal = await DisplayAlert("Notice", "Total time " + total + " ชั่วโมง:นาที", "Ok", "Cancle");
                            if (resultTotal)
                            {

                                string result = await FilterTimesheetService.reqRecordTimesheet(timesheetData.ProjectNumber, timesheetData.ActionId,
                                                                      timesheetData.TaskId, timesheetData.FunctionId, timesheetData.UserId,
                                                                      timesheetData.TimeSheetStart, timesheetData.TimeSheetEnd);
                                JObject data = JObject.Parse(result);
                                string msg = (string)data["msg"];
                                if (msg == "success")
                                {
                                    var userAct = await DisplayAlert("Notice", "Record Successfully", "Ok", "Cancle");
                                    if (userAct)
                                    {
                                        await Navigation.PopToRootAsync();
                                    }

                                }
                                else
                                {
                                    var userAct = await DisplayAlert("Notice", "Record False TryAgain", "Ok", "Cancle");
                                    if (userAct != true)
                                    {
                                        await Navigation.PopToRootAsync();
                                    }
                                }
                            }
                        }
                        catch {
                            //await DisplayAlert("Notice", "fail to load content","Cancle");
                        }

                    }
                }
           
            }


        }
       
    }
}