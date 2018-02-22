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

            timesheetData.TaskId = model.taskId;
            SetDefaultTime();

            if (checkConnect() == true)
                {
                RenderGetActionName();
                RenderFindFunctionId(model.projectId,timesheetData.TaskId, model.functionName);
                }
            else
                Title = "Internet not connect";


            //timesheetData.TimeSheetId
            /*
            timesheetData.UserId = model.userId;
            timesheetData.ProjectNumber = model.projectId;
            timesheetData.TaskId = model.taskId;
            timesheetData.FunctionId = model.functionId;
            */
            /*
            ActionNames.Items.Add("Gettering Requirement");
            ActionNames.Items.Add("System Analysis");
            ActionNames.Items.Add("Development");
            ActionNames.Items.Add("Testing");
            */
            timesheetData.UserId = model.userId;
            timesheetData.ProjectNumber = model.projectId;
           // timesheetData.FunctionId = "100001";
           // timesheetData.ActionId = "F";

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
        private void ActionNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempObj.actionName = ActionNames.Items[ActionNames.SelectedIndex];
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

                                string actId = await FilterTimesheetService.reqFindActionId(tempObj.actionName);
                                JObject data2 = JObject.Parse(actId);
                                string aid = (string)data2["actionId"];

                                //await Navigation.PushAsync(new NextConcate(proId,uid,taskId,fid,tempObj.actionName,tempObj.Strdate, tempObj.StrStime, tempObj.StrEtime, tempObj.concateStime, tempObj.concateEtime));
                                string result = await FilterTimesheetService.reqRecordTimesheet(timesheetData.ProjectNumber, aid,
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
                            await DisplayAlert("Notice", "fail to load content","Cancle");
                        }

                    }
                }
           
            }


        }

        public async void RenderFindFunctionId(string pid,string tid, string fname)
        {
            try
            {
                string result = await FilterTimesheetService.reqFindFunctionId(pid, tid, fname);
                JObject temp = JObject.Parse(result);
                timesheetData.FunctionId = (string)temp["functionId"];
            }
            catch {
                await DisplayAlert("Notice", "Fail to load content", "Cancle");
            }

        }

        public async void RenderGetActionName()
        {
            try
            {
                var actResult = new List<ActionModel>();
                var TempactResult = new List<ActionModel>();
                string tempData = await FilterTimesheetService.reqGetAction();
                actResult = JsonConvert.DeserializeObject<List<ActionModel>>(tempData);
                foreach (var item in actResult)
                {
                    TempactResult.Add(new ActionModel
                    {
                        actionId = item.actionId,
                        actionName = item.actionName
                    });
                }

                foreach (var item in TempactResult)
                {
                    if (item != null)
                        ActionNames.Items.Add(item.actionName);
                    else
                        ActionNames.Items.Add("Non of above");


                }
            }
            catch {
                await DisplayAlert("Notice", "Fail to load content", "Cancle");
            }
        }

       
    }
}