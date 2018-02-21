using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace SmartPM.Services
{
    public static class TimesheetService
    {

        public static async Task<string> ListApprove(string uid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {


                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest3/ListApprovTimesheet", content))

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

        public static async Task<string> actionApprove(string pid, string tid, string fid, string actid, string uid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("pid", pid));
                postData.Add(new KeyValuePair<string, string>("tid", tid));
                postData.Add(new KeyValuePair<string, string>("fid", fid));
                postData.Add(new KeyValuePair<string, string>("actid", actid));
                postData.Add(new KeyValuePair<string, string>("uid", uid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {


                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.200:56086/APIRest3/approveAction", content))

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
