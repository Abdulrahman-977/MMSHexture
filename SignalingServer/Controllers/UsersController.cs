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
    //[Models.AuthorizeUser("Admin,SystemDirector")]
    public class UsersController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IUserRepository _userRepository { get; set; }
        // GET: Users
        public async Task<ActionResult> Index()
        {
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();
            ViewBag.Success = TempData["success"];
            ViewBag.Denied = TempData["denied"];
            var list = (await _userRepository.GetAll()).Where(x=>x.id != userid).ToList();
            return View(list);
        }
        [AllowAnonymous]
        public async Task<ActionResult> Add()
        {
            ViewBag.Success = TempData["success"];
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(AddUserViewModel model)
        {
            model.designation = model.role;
            var response = await _userRepository.Add(model);
            if (response.isSuccess)
            {
                TempData["success"] = true;
                return RedirectToAction("Index");
            }
            TempData["success"] = false;
            return RedirectToAction("Add");
        }
        public async Task<ActionResult> UpdateProfile()
        {
            ViewBag.Success = TempData["success"];
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();
            var user = await _userRepository.GetById(userid);
            var model = new UpdateUserProfileViewModel();
            model.userId = UserToken.Token.authData.userId.ToString();
            model.designation = user.designation;
            model.gender = user.gender;
            model.fullname = user.fullname;
            model.mobile = user.mobile;
            model.profileImage = user.profileImage;
            model.title = user.title;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateProfile(UpdateUserProfileViewModel model, FormCollection formdata, HttpPostedFileBase files)
        {
            ResponseViewModel responsepss = null;
            if (!String.IsNullOrEmpty(model.oldPassword))
            {
                ChangePasswordViewModel pssmodel = new ChangePasswordViewModel();
                pssmodel.userId = model.userId;
                pssmodel.oldPassword = model.oldPassword;
                pssmodel.newPassword = model.newPassword;
                pssmodel.confirmPassword = model.confirmPassword;
                responsepss = await _userRepository.ChangePassword(pssmodel);
            }
            if (responsepss == null || responsepss.isSuccess)
            {
                if (files != null)
                {
                    files.SaveAs(Server.MapPath("~/Content/Uploads/" + files.FileName));
                    model.profileImage = files.FileName;
                    Session["img"] = "../../Content/Uploads/" + files.FileName;
                }
                else
                {
                    var user = await _userRepository.GetById(model.userId);
                    model.profileImage = user.profileImage;
                }
                model.role = Session["role"].ToString();
                model.designation = "";
                var response = await _userRepository.UpdateProfile(model);
                if (response.isSuccess)
                {
                    Session["title"] = model.title;
                    Session["name"] = model.fullname;
                    TempData["success"] = true;
                    return RedirectToAction("UpdateProfile");
                }
            }
            TempData["success"] = false;
            return RedirectToAction("UpdateProfile");

        }
        public async Task<ActionResult> CheckEmail(string email)
        {
            var check = await _userRepository.GetByEmailAsync(email) == null;
            return Json(check, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Edit(string userId)
        {
            ViewBag.Success = TempData["success"];
            var user = await _userRepository.GetById(userId);
            var model = new UpdateUserProfileViewModel();
            model.userId = userId;
            model.designation = user.designation;
            model.gender = user.gender;
            model.fullname = user.fullname;
            model.mobile = user.mobile;
            model.profileImage = user.profileImage;
            model.title = user.title;
            model.designation = "";
            model.role = user.roles.Count != 0 ? user.roles.FirstOrDefault() : "";
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(UpdateUserProfileViewModel model, FormCollection formdata, HttpPostedFileBase files)
        {
            ResponseViewModel responsepss = null;
            if (!String.IsNullOrEmpty(model.oldPassword))
            {
                ChangePasswordViewModel pssmodel = new ChangePasswordViewModel();
                pssmodel.userId = model.userId;
                pssmodel.oldPassword = model.oldPassword;
                pssmodel.newPassword = model.newPassword;
                pssmodel.confirmPassword = model.confirmPassword;
                 responsepss = await _userRepository.ChangePassword(pssmodel);
            }
            if(responsepss == null || responsepss.isSuccess) { 
                if (files != null)
                {
                    files.SaveAs(Server.MapPath("~/Content/Uploads/" + files.FileName));
                    model.profileImage = files.FileName;
                }
                else
                {
                    var user = await _userRepository.GetById(model.userId);
                    model.profileImage = user.profileImage;
                }
                model.designation = "";
                var response = await _userRepository.UpdateProfile(model);
                if (response.isSuccess)
                {
                
                    TempData["success"] = true;
                    return RedirectToAction("Edit", new { userId = model.userId });
                }
            }
            TempData["success"] = false;
            return RedirectToAction("Edit",new { userId  = model.userId});

        }
        public async Task<ActionResult> Delete(string Id)
        {
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();
            if(Id != userid)
            {
                var response = await _userRepository.Delete(Id);
                if (response)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            TempData["denied"] = true;
            return RedirectToAction("Index");

        }
    }
    
}