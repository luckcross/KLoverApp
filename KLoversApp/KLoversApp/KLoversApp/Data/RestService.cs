using KLoversApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KLoversApp.Data
{
    public class RestService
    {
        private HttpClient client;
        private string grantType = "password";

        public RestService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.HttpClientBaseAddress);
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded' "));
        }

        public async Task<Token> Login(User user)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("grantType", grantType));
            postData.Add(new KeyValuePair<string, string>("username", user.Username));
            postData.Add(new KeyValuePair<string, string>("password", user.Password));

            FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
            Token response = await PostResponseLogin<Token>(Constants.LoginUrl, content);

            DateTime dt = DateTime.Today;
            response.ExpireDate = dt.AddSeconds(response.ExpireIn);

            return response;
        }

        public async Task<Token> LoginProvisorio(string token)
        {
            object request = new
            {
                Token = token,
            };

            string jsonRequest = JsonConvert.SerializeObject(token);

            string contentType = "application/json";
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, contentType);

            string url = Constants.LoginUrl + token;

            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri(Constants.HttpClientBaseAddress);

            var result = await cliente.PutAsync(url, stringContent);
            string x = "blabla";

            if (!result.IsSuccessStatusCode)
            {
                return null; // <----- Mudar logica para evitar isso, pelo amor de deus
            }
            return null;
        }

        public async Task<T> PostResponseLogin<T>(string webUrl, FormUrlEncodedContent content) where T : class
        {
            HttpResponseMessage response = await client.PostAsync(webUrl, content);
            string jsonResult = response.Content.ReadAsStringAsync().Result;
            T responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
            return responseObject;
        }

        public async Task<T> PostResponse<T>(string webUrl, string jsonString) where T : class
        {
            Token token = App.TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AcessToken);

            string contentType = "application/json";
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, contentType);

            try
            {
                HttpResponseMessage response = await client.PostAsync(webUrl, stringContent);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        T responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
                        return responseObject;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public async Task<T> GetResponse<T>(string webUrl) where T : class
        {
            Token token = App.TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AcessToken);
            try
            {
                HttpResponseMessage response = await client.GetAsync(webUrl);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        T responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
                        return responseObject;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            
            return null;
        }
    }
}
