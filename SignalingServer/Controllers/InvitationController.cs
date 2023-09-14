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
    [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
    public class InvitationController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IRoomRepository _roomAgendaRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaRepository _meetingAgendaRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAttendeeRepository _AttendeeRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IBoardRepository _BoardRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingRepository _MeetingRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaSpeakerRepository _MeetingAgendaSpeakerRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IAmenityRepository _amenityRepository { get; set; }
        // GET: Board
        public async Task<ActionResult> Index()
        {
            var list = (await _AttendeeRepository.GetByUserId(UserToken.Token.authData.userId.ToString()));
            var result = new List<MeetingAttendeeViewModel>();
            foreach(var att in list)
            {
                var model = new MeetingAttendeeViewModel();
                model.meeting = await _MeetingRepository.GetById(att.meetingId);
                model.Id = att.meetingAttendeeId;
                model.MeetingName = (await _MeetingRepository.GetById(att.meetingId)).meetingSubject;
                model.Date = (await _MeetingRepository.GetById(att.meetingId)).meetingDate.ToShortDateString() + " " + (await _MeetingRepository.GetById(att.meetingId)).meetingStartTime + " " + (await _MeetingRepository.GetById(att.meetingId)).meetingEndTime;
                model.status = att.inviteStatus;
                if(model.meeting.boardId != 0)
                {
                    model.boardname = (await _BoardRepository.GetById(model.meeting.boardId)).boardName;

                }
                else
                {
                    model.boardname = "";
                }
                result.Add(model);
            }
            
            return View(result);
        }
        public async Task<ActionResult> View(int Id)
        {
            var model = (await _AttendeeRepository.GetById(Id));
            var attendeemodel = new MeetingAttendeeViewModel();
            attendeemodel.boardname = (await _BoardRepository.GetById((await _MeetingRepository.GetById(model.meetingId)).boardId)).boardName;
           
            attendeemodel.Attendee = model;
            attendeemodel.meeting = await _MeetingRepository.GetById(attendeemodel.Attendee.meetingId);
            attendeemodel.roomname = (await _roomAgendaRepository.GetById((int)attendeemodel.meeting.roomId)).roomName;
            attendeemodel.Attendees = await _AttendeeRepository.GetByMeetingId(model.meetingId.ToString());
            var meetingAgendas = (await _meetingAgendaRepository.GetAll()).Where(x => x.meetingId == model.meetingId).ToList();
            attendeemodel.Speakers = (await _MeetingAgendaSpeakerRepository.GetAll()).Where(x => meetingAgendas.Select(w => w.meetingAgendaId).ToList().Contains(x.meetingAgendaId)).ToList();
            ViewBag.Amenities = (await _amenityRepository.GetAll()).Where(x => x.amenityIcon == attendeemodel.meeting.meetingId.ToString()).ToList();
            ViewBag.Success = TempData["success"];
            return View(attendeemodel);
        }

        public async Task<ActionResult> ApproveInvitation(int id)
        {
            var model = await _AttendeeRepository.GetById(id);
            model.inviteStatus = 1;
            var response = await _AttendeeRepository.Update(model);
            if (response.isSuccess)
            {
                TempData["success"] = true;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            TempData["success"] = false;
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> RejectInvitation(int id, string inviteStatusReason)
        {
            var model = await _AttendeeRepository.GetById(id);
            model.inviteStatus = 2;
            model.inviteStatusReason = inviteStatusReason;
            var response = await _AttendeeRepository.Update(model);
            if (response.isSuccess)
            {
                TempData["success"] = true;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            TempData["success"] = false;
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        // GET: Invitation

    }
}