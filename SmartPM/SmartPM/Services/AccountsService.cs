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
    public static class AccountsService
    {
        public static async Task<string> AddRequest(string Vusername, string Vpassword, string VFirstname, string VLastname, string Jobresp, string userLogged, string gid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("Vusername", Vusername));
                postData.Add(new KeyValuePair<string, string>("Vpassword", Vpassword));
                postData.Add(new KeyValuePair<string, string>("VFirstname", VFirstname));
                postData.Add(new KeyValuePair<string, string>("VLastname", VLastname));
                postData.Add(new KeyValuePair<string, string>("Jobresp", Jobresp));
                postData.Add(new KeyValuePair<string, string>("userLogged", userLogged));
                postData.Add(new KeyValuePair<string, string>("gid", gid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    // client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/UserManagement/AddUserService", content))
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

        public static async Task<string> EditService(string id, string Vusername, string Vpassword, string VFirstname, string VLastname, string Jobresp, string userLogged, string stat, string gid)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", id));
                postData.Add(new KeyValuePair<string, string>("Vusername", Vusername));
                postData.Add(new KeyValuePair<string, string>("Vpassword", Vpassword));
                postData.Add(new KeyValuePair<string, string>("VFirstname", VFirstname));
                postData.Add(new KeyValuePair<string, string>("VLastname", VLastname));
                postData.Add(new KeyValuePair<string, string>("Jobresp", Jobresp));
                postData.Add(new KeyValuePair<string, string>("userLogged", userLogged));
                postData.Add(new KeyValuePair<string, string>("stat", stat));
                postData.Add(new KeyValuePair<string, string>("gid", gid));

                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    // client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/UserManagement/EditService", content))
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

        public static async Task<string> DetailsServices(string id)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", id));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/UserManagement/DetailsService", content))
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



        public static async Task<string> DetailsM(string id)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", id));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/UserManagement/DetailsM", content))
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

        public static async Task<string> Edit(string id)
        {
            try
            {
                // This is the postdata
                var postData = new List<KeyValuePair<string, string>>(2);
                postData.Add(new KeyValuePair<string, string>("id", id));
                HttpContent content = new FormUrlEncodedContent(postData);

                using (var client = new HttpClient())
                {
                    //client.Timeout = new TimeSpan(0, 0, 15);
                    using (var response = await client.PostAsync("http://192.168.88.107:56086/UserManagement/Edit", content))
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
