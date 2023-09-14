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
    public class ReportController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingRepository _meetingRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.ITaskRepository _taskRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IDecisionRepository _decisionRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IAmenityRepository _amenityRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IBoardRepository _boardRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IRoomRepository _ROOMRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAttendeeRepository _attendeeRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IUserRepository _userRepository { get; set; }
        // GET: Report
        public async Task<ActionResult> Index()
        {
            var listOfMeeting = await _meetingRepository.GetAll();
            var listMeeting = new List<MeetingViewModel>();
            foreach(var metting in listOfMeeting)
            {
                try { 
                var modelmeeting = new MeetingViewModel();
                modelmeeting.meetingId = metting.meetingId;
                
                modelmeeting.meetingNumber = metting.meetingNumber;
                modelmeeting.meetingSubject = metting.meetingSubject;
                modelmeeting.meetingDate = metting.meetingDate;
                modelmeeting.meetingStartTime = metting.meetingStartTime;
                modelmeeting.meetingEndTime = metting.meetingEndTime;
                modelmeeting.meetingStatus = metting.meetingStatus;
                modelmeeting.degreeOfImportance = metting.degreeOfImportance;
                modelmeeting.meetingType = metting.meetingType;
                modelmeeting.boardId = metting.boardId;
                modelmeeting.boardName = (await _boardRepository.GetById(metting.boardId)).boardName;
                modelmeeting.roomName = await _ROOMRepository.GetNameById((int)metting.roomId);
                modelmeeting.Attendees = await _attendeeRepository.GetByMeetingId(metting.meetingId.ToString());
                listMeeting.Add(modelmeeting);
                }
                catch { }

            }
            ViewBag.Meetings = listMeeting;
            ViewBag.Tasks = await _taskRepository.GetAll();
            ViewBag.Decisions = await _decisionRepository.GetAll();
            ViewBag.Amenity = await _amenityRepository.GetAll();
            ViewBag.Users = await _userRepository.GetAll();
            return View();
        }
    }
}