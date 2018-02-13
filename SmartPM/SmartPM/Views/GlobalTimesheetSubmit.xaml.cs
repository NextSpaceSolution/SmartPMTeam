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

namespace SmartPM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GlobalTimesheetSubmit : ContentPage
	{

        TimesheetOnSubmitModel timesheetData = new TimesheetOnSubmitModel();
        TempTimesheetModel tempObj = new TempTimesheetModel();
     
        public GlobalTimesheetSubmit (TimesheetOneModel model)
		{
			InitializeComponent ();
            Labelfullname.Text = model.fullName;
            Labeljob.Text = model.jobResp;
            LabelProname.Text = model.projectName;
            Taskname.Text = model.TaskName;
            LableFunction.Text = model.functionName;

            timesheetData.TaskId = model.taskId;
            

            if (checkConnect() == true)
                {
                RenderGetActionName();
                RenderFindFunctionId(timesheetData.TaskId, model.functionName);
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
            timesheetData.UserId = "100017";
            timesheetData.ProjectNumber = model.projectId;
           // timesheetData.FunctionId = "100001";
            timesheetData.ActionId = "A";

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
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            tempObj.tempDate = DateTimesheetPick.Date;
            tempObj.tempSTime = TimesheetStartPick.Time;
            tempObj.tempETime = TimesheetEndPick.Time;


            tempObj.Strdate = tempObj.tempDate.ToString("dd-MM-yyyy");
            tempObj.StrdateConcate = tempObj.tempDate.ToString("yyyy-MM-dd");
            tempObj.StrStime = tempObj.tempSTime.ToString(@"hh\:mm\:ss");
            tempObj.StrEtime = tempObj.tempETime.ToString(@"hh\:mm\:ss");
            if (tempObj.StrStime == "00:00:00")
                tempObj.StrStime = "09:00:00";
            if (tempObj.StrEtime == "00:00:00")
                tempObj.StrEtime = "18:00:00";

            tempObj.concateStime = tempObj.StrdateConcate + " " + tempObj.StrStime;
            tempObj.concateEtime = tempObj.StrdateConcate + " " + tempObj.StrEtime;

            timesheetData.TimeSheetStart = tempObj.concateStime;
            timesheetData.TimeSheetEnd = tempObj.concateEtime;
            // string proId = "100001";
            //string taskId = "100005";
            //  string uid = "100019";
            // string fid = "100008";

            

            //await Navigation.PushAsync(new NextConcate(proId,uid,taskId,fid,tempObj.actionName,tempObj.Strdate, tempObj.StrStime, tempObj.StrEtime, tempObj.concateStime, tempObj.concateEtime));
            string result = await reqRecordTimesheet(timesheetData.ProjectNumber, timesheetData.ActionId,
                                                  timesheetData.TaskId, timesheetData.FunctionId, timesheetData.UserId,
                                                  timesheetData.TimeSheetStart, timesheetData.TimeSheetEnd);
            JObject data = JObject.Parse(result);
            string msg = (string)data["msg"];
            if (msg == "success")
                App.Current.MainPage.DisplayAlert("NOtic", "Record Successfully", "Ok");
            else
                App.Current.MainPage.DisplayAlert("Notic", "Record False", "Try again");
        }

        public async void RenderFindFunctionId(string tid, string fname)
        {
            string result = await reqFindFunctionId(tid, fname);
            JObject temp = JObject.Parse(result);
            timesheetData.FunctionId = (string)temp["functionId"];

        }

        public async void RenderRequestNewtimesheet()
        {
            // var Result = await reqRecordTimesheet(parameter);
            //JObject tempData = JObject.Parse(Result);
            //string jsonResult = (string)tempDada["msg"];
            /*
            if (jsonResult == "Success")
            {
                await DisplayAlert("Notification", "บันทึกข้อมูลเรียบร้อย", "OK");
                await Navigation.PopAsync();
            }
            else
            {

                await DisplayAlert("Notification", "ไม่สามารถบันทึกข้อมูลได้", "Cancle");
            }*/
        }

        public async void RenderGetActionName()
        {
            var actResult = new List<ActionModel>();
            var TempactResult = new List<ActionModel>();
            string tempData = await reqGetAction();
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

        public async Task<string> reqRecordTimesheet(string projectId, string actId,string taskId, string funcId, string userId, string launchStart, string launchEnd)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("proId", projectId));
                postData.Add(new KeyValuePair<string, string>("actId", actId));
                postData.Add(new KeyValuePair<string, string>("taskId", taskId));
                postData.Add(new KeyValuePair<string, string>("funcId", funcId));
                postData.Add(new KeyValuePair<string, string>("userId", userId));
                postData.Add(new KeyValuePair<string, string>("thStart", launchStart));
                postData.Add(new KeyValuePair<string, string>("thEnd", launchEnd));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/RecordTimesheet", content))
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

        public async Task<string> reqGetAction()
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
               
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/GetActionName", content))
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

        public async Task<string> reqFindFunctionId(string tid, string funcName)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("tid", tid));
                postData.Add(new KeyValuePair<string, string>("funcName", funcName));



                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest2/FindFunctionId", content))
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