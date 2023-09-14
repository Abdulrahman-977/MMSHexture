using Core.Interfaces.IExternalServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RestOperation : IRestOperation
    {

        public async Task<HttpResponseMessage> Get(string Url, string accessToken)//
        {
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, Url);
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);//
                var response = await httpClient.SendAsync(httpRequestMessage);
                return response;
            }
            catch
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }

        public async Task<HttpResponseMessage> Post<T>(string Url, T data, string accessToken)//, string accessToken
        {
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Url);
                string Json = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                {
                PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None,
                    Formatting = Formatting.Indented
                });
                HttpContent httpContent = new StringContent(Json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpRequestMessage.Content = httpContent;
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);//
                var result = await httpClient.SendAsync(httpRequestMessage);
                return result;
            }
            catch
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }

        }

        public async Task<HttpResponseMessage> Put<T>(string Url, T data, string accessToken)
        {
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, Url);
                string Json = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });
                HttpContent httpContent = new StringContent(Json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpRequestMessage.Content = httpContent;
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var result = await httpClient.SendAsync(httpRequestMessage);
                return result;
            }
            catch
            {
                return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }
        public async Task<HttpResponseMessage> Delete(string Url, string accessToken)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, Url);
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.SendAsync(httpRequestMessage);
            return response;
        }
    }
}
