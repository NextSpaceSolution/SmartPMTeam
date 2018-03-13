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
 public static  class TaskService
    {

        public static async Task<string> FilterTask(string gid, string uid, string pid)
        {
            try
            {
                // This is the Postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("gid", gid));
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                postData.Add(new KeyValuePair<string, string>("pid", pid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    // client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest2/FilterTask", content))
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
                            return "error" + Convert.ToString(response.StatusCode);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return Convert.ToString(ex);
            }
        }


        public static async Task<string> FilterFunction(string gid, string uid, string pid, string tid)
        {
            try
            {
                // This is the Postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("gid", gid));
                postData.Add(new KeyValuePair<string, string>("uid", uid));
                postData.Add(new KeyValuePair<string, string>("pid", pid));
                postData.Add(new KeyValuePair<string, string>("tid", tid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/APIRest2/FilterFunction", content))
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
                            return "error" + Convert.ToString(response.StatusCode);
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
