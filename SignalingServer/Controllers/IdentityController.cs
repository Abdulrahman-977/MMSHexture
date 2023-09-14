using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unity;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class IdentityController : Controller
    {
        [Dependency]
		public Core.Interfaces.IExternalServices.IIdentityRepository _identityRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IUserRepository _userRepository { get; set; }
        // GET: Identity
        public async Task<ActionResult> Login()
        {
            ViewBag.Success = TempData["success"];
            if (Session[UserToken.USER_SESSION_VALUE]  != null && UserToken.Token !=null && UserToken.Token.authData != null)
            {

                Session[UserToken.USER_SESSION_VALUE] = JsonConvert.SerializeObject(UserToken.Token);
                Session["role"] = (await _userRepository.GetById(UserToken.Token.authData.userId.ToString())).roles.Count != 0 ? (await _userRepository.GetById(UserToken.Token.authData.userId.ToString())).roles[0] : "";
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        [HttpPost]
        public async Task<ActionResult> Login(Core.Models.LoginViewModel login)
        {

            var res = await _identityRepository.Login(login);

            if (res.authData == null || string.IsNullOrEmpty(res.authData.tokenInfo.token))
            {

                ViewBag.login = false;
                return View(login);
            }
            else
            {
                
                UserToken.Token = res;
                Session[UserToken.USER_SESSION_VALUE] = JsonConvert.SerializeObject(UserToken.Token);
                var role = (await _userRepository.GetById(UserToken.Token.authData.userId.ToString())).roles.Count != 0 ? (await _userRepository.GetById(UserToken.Token.authData.userId.ToString())).roles[0] : ""; 
                Session["role"] = (await _userRepository.GetById(UserToken.Token.authData.userId.ToString())).roles.Count != 0 ? (await _userRepository.GetById(UserToken.Token.authData.userId.ToString())).roles[0] : "";
                Session["img"] = (await _userRepository.GetById(UserToken.Token.authData.userId.ToString())).profileImage != "" ? "../../Content/Uploads/" + (await _userRepository.GetById(UserToken.Token.authData.userId.ToString())).profileImage : "../../Content/img/user.png";
                Session["title"] = UserToken.Token.authData.title;
                Session["name"] = UserToken.Token.authData.fullname;
                return RedirectToAction("Index", "Home");
            }
            //      _userToken.Token = new TokenViewModel() {  username= "Admin", token = "kvwoekvpowekovpowekpvokepovke", profile= new ProfileViewModel {  photoUrl = "https://cdn.icon-icons.com/icons2/1378/PNG/512/avatardefault_92824.png" } };
            //return RedirectToAction("Index", "Home");
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            UserToken.Token = new TokenViewModel();
            return RedirectToAction("Login", "Identity");
        }
        public  ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            var response = _identityRepository.ForgetPasswordAsync(model);
            return RedirectToAction("ForgetPasswordResult");
        }
        public ActionResult ForgetPasswordResult()
        {
            return View();
        }
        [Route("ResetPassword")]
        public ActionResult ResetPassword(string userId, string code)
        {
            ViewBag.UserId = userId;
            ViewBag.code = code;
            return View();
        }
        [HttpPost]
        [Route("ResetPassword")]

        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var response = await _identityRepository.ResetPassword(model);
            if (response)
            {
                TempData["success"] = true;
                return RedirectToAction("Login");
            }
            ViewBag.Success  = false;
            ViewBag.UserId = model.userId;
            ViewBag.code = model.code;
            return View();
        }
    }
}