using Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace WebApplication5.Controllers
{
    [Models.AuthorizeUser("Admin,Director,SystemDirector")]
    public class RoomController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IRoomRepository _roomRepository { get; set; }
        // GET: Room
        public async Task<ActionResult> Index()
        {
            ViewBag.Success = TempData["success"];
            var list = await _roomRepository.GetAll();
            return View(list);
        }
        [Models.AuthorizeUser("Admin,SystemDirector")]
        public async Task<ActionResult> Add()
        { 
            ViewBag.Success = TempData["success"];
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(RoomViewModel model, FormCollection formdata, HttpPostedFileBase files)
        {
            if(files != null) { 
            files.SaveAs(Server.MapPath("~/Content/Uploads/" + files.FileName));
            model.image = files.FileName;
            }
            model.roomIsClosedReason = "";
            model.devices = new List<Device>();
            model.roomAmenities = new List<RoomAmenity>();
            var response = await _roomRepository.Add(model);
            if (response.isSuccess == true)
            {
                TempData["success"] = true;
                return RedirectToAction("Index");
            }
            TempData["success"] = false;
            return View();
        }
        [Models.AuthorizeUser("Admin,SystemDirector")]
        public async Task<ActionResult> Edit(int Id)
        {
            if (Id != 0)
            {
                ViewBag.Success = TempData["success"];
                var model = await _roomRepository.GetById(Id);
                return View(model);
            }
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int RoomId, RoomViewModel model, FormCollection formdata, HttpPostedFileBase files)
        {
            var oldmodel = await _roomRepository.GetById(RoomId);
            if(files != null)
            {
                files.SaveAs(Server.MapPath("~/Content/Uploads/" + files.FileName));
                model.image = files.FileName;
            }
            if (model.image == null)
            {
                model.image = oldmodel.image;
            }
            model.roomIsClosedReason = "";
            model.devices = oldmodel.devices;
            model.roomAmenities = oldmodel.roomAmenities;
            var response = await _roomRepository.Update(model);
            if (response.isSuccess == true)
            {
                TempData["success"] = true;
                return RedirectToAction("Index");
            }
            TempData["success"] = false;
            return View();
        }

        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _roomRepository.Delete(Id);
            if (response)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }
    }
}