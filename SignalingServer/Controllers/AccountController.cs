using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            //AuthenticatedUser res = new AuthenticatedUser();
            //LoginViewModel login = new LoginViewModel { username = username, password = password };
            //    //client.BaseAddress = new Uri("https://mms.compass-dx.com/api/");
            //    var request = new HttpRequestMessage(HttpMethod.Post, "https://mms.compass-dx.com/api/Account/Login");
            //    var client = new HttpClient();
            //    string Json = JsonConvert.SerializeObject(login, new JsonSerializerSettings()
            //    {
            //        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            //        Formatting = Formatting.Indented
            //    });
            //    HttpContent httpContent = new StringContent(Json);
            //    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //    request.Content = httpContent;
            //    var response = await client.SendAsync(request);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var content = await response.Content.ReadAsStringAsync();
            //    res = JsonConvert.DeserializeObject<AuthenticatedUser>(content);
            //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", res.authData.tokenInfo.token);
            //    UserToken.Token = new TokenViewModel { token = res.authData.tokenInfo.token };
            //    Session["Token"] = UserToken.Token;
            //}


            return RedirectToAction("Index", "Home");

        }
    }
}