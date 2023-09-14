using Core.Helper;
using Core.Interfaces.IExternalServices;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IRestOperation _restOperation;
        public IdentityRepository(IRestOperation restOperation)
        {
            _restOperation = restOperation;
        }

        public async Task<bool> ForgetPasswordAsync(ForgetPasswordViewModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{Constatnts.APIUrl}Account/ForgetPassword");
            var client = new HttpClient();
            string Json = JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
            HttpContent httpContent = new StringContent(Json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = httpContent;
            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
            
        }
        public async Task<TokenViewModel> Login(LoginViewModel login)
        {
            try
            {
                TokenViewModel res = new TokenViewModel();
                //HttpClientHandler clientHandler = new HttpClientHandler();
                //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                var request = new HttpRequestMessage(HttpMethod.Post, $"{Constatnts.APIUrl}Account/Login");
                var client = new HttpClient();
                string Json = JsonConvert.SerializeObject(login, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });
                HttpContent httpContent = new StringContent(Json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = httpContent;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<TokenViewModel>(content);
                }
                return res;
            }
            catch
            {
                return new TokenViewModel();
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordViewModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{Constatnts.APIUrl}Account/ResetPassword");
            var client = new HttpClient();
            string Json = JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
            HttpContent httpContent = new StringContent(Json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = httpContent;
            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
