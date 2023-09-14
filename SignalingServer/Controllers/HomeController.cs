using Core.Helper;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unity;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
    public class HomeController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingRepository _meetingRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.INotificationRepository _notiRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAttendeeRepository _attendeeRepository { get; set; }
        int attended = 0;
        public async Task<ActionResult> Index()
        {
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();
            ViewBag.Notis = (await _notiRepository.GetAllByUserId(userid)).Take(10).ToList();
            var meeting = (await _meetingRepository.GetByUserId(userid)).Where(x=>x.meetingDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            var list = new List<MeetingHomePageViewModel>();
            foreach(var m in meeting)
            {
                try{
                var model = new MeetingHomePageViewModel();
                    model.meeting = m;
                model.meetingId = m.meetingId;
                    model.InvitationStatus = (await _attendeeRepository.GetByMeetingId(m.meetingId.ToString())).Where(x => x.userId == userid).Select(x => x.inviteStatus).FirstOrDefault();
                    var date = m.meetingDate.ToShortDateString() + " " + m.meetingStartTime;
                    var meetingdate = Convert.ToDateTime(date);
                TimeSpan duration = meetingdate - DateTime.Now;
                
                if(duration.TotalHours > 0)
                {
                        var hours = ((int)duration.TotalHours);

                        var minutes = (int)duration.TotalMinutes - (hours * 60);

                    model.time = hours + " س " + minutes + " د ";
                }
                //if(duration.TotalMinutes > 0)
                //{
                //        var hours = duration.TotalMinutes / 60;
                //    model.time += ((int)duration.TotalMinutes).ToString() + " د ";
                //}
                //if (duration.TotalSeconds > 0)
                //{
                //    model.time += ((int)duration.TotalSeconds).ToString() + " ث ";
                //}
                model.userId = userid; model.title = m.meetingSubject;
                model.UserName = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.fullname.ToString();
                list.Add(model);
                }
                catch(Exception ex)  { }
            }
            return View(list);
        }
        public void AddAttended()
        {
            attended = attended + 1;
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RoomHub>();
            hubContext.Clients.Client("1").Invoke("ItemsUpdated", attended); ; 
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public async Task<ActionResult> GetNotifications(string userId)
        {
            var list = (await _notiRepository.GetAllByUserId(userId)).Take(10).ToList();
            return PartialView("_NotiDiv", list);
        }
        public  async Task<ActionResult> ShowAllNotification()
        {
            ViewBag.Success = TempData["success"];
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();
            var list = await _notiRepository.GetAllByUserId(userid);
            return View(list);
        }
        public async Task<ActionResult> DeleteNotification(int Id)
        {
            var response = await _notiRepository.Delete(Id);
            if (response)
            {
                TempData["success"] = true;
                return RedirectToAction("ShowAllNotification");
            }
            TempData["success"] = false;
            return RedirectToAction("ShowAllNotification");
        }
    }
}