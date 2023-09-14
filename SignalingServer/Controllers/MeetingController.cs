using Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unity;
using WebApplication5.Models;
using System.Linq;
using WebApplication5.LocalDataModels;
using Newtonsoft.Json;

namespace WebApplication5.Controllers
{
    [Models.AuthorizeUser("Admin,Director,SystemDirector")]
    public class MeetingController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingRepository _meetingRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaRepository _meetingAgendaRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaSpeakerRepository _meetingAgendaSpeakerRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IBoardRepository _boardRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IUserRepository _userRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAttendeeRepository _meetingAttendeeRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaPollOptionRepository _meetingAgendaPollOptionRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaPollRepository _meetingAgendaPollRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IRoomRepository _roomRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.ITaskRepository _taskRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IDecisionRepository _decisionRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingPollVottingRepository _pollVottingRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IAmenityRepository _amenityRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IDeviceRepository _deviceRepository { get; set; }
        // GET: Meeting
        public ActionResult Meeting(string mid)
        {
            return View();
        }
        [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
        public async Task<ActionResult> StartMeeting(string mid, string userId, string uid)
        {
            
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();
            int meetingId = Convert.ToInt32(mid);
            var meeting = (await _meetingRepository.GetById(Convert.ToInt32(mid)));
            var date = meeting.meetingDate.ToShortDateString() + " " + meeting.meetingEndTime;
            var startdate = meeting.meetingDate.ToShortDateString() + " " + meeting.meetingStartTime;
            var meetingdate = Convert.ToDateTime(date);
            var meetingstarttime = Convert.ToDateTime(startdate);
            TimeSpan duration = meetingdate - DateTime.Now;
            var endtime = (duration.TotalSeconds == 0 || duration.TotalSeconds < 0);
            if(endtime || meeting.meetingStatus == 3 || meetingstarttime > DateTime.Now)
            {
                return View("ErrorMeeting");
            }
            ViewBag.Boards = await _boardRepository.GetAll();
            var listofattendee = await _meetingAttendeeRepository.GetByMeetingId(mid);
            if(listofattendee.Where(x=>x.userId == userId && x.inviteStatus != 2).FirstOrDefault() == null)
            {
                return View("ErrorMeeting");
            }
            ViewBag.Attedees = await _meetingAttendeeRepository.GetByMeetingId(mid);
           
            var meetingAgendas = (await _meetingAgendaRepository.GetAll()).Where(x => x.meetingId == meetingId).ToList();
            ViewBag.Users = await _userRepository.GetAll();
            ViewBag.MeetingTitle = (await _meetingRepository.GetById(Convert.ToInt32(mid))).meetingSubject;
            ViewBag.Speakers = (await _meetingAgendaSpeakerRepository.GetAll()).Where(x=> meetingAgendas.Select(w=>w.meetingAgendaId).ToList().Contains(x.meetingAgendaId)).ToList();
            ViewBag.Tasks = await _taskRepository.GetByMeetingId(meetingId);
            ViewBag.Decisions = await _decisionRepository.GetByMeetingId(meetingId);
            var polls = (await _meetingAgendaPollRepository.GetAll()).Where(x=>x.meetingId == meetingId).ToList();
            ViewBag.Poll = polls;
            ViewBag.PollOptions = (await _meetingAgendaPollOptionRepository.GetAll()).Where(x => polls.Select(w => w.pollId).ToList().Contains(x.pollId)).ToList();
            ViewBag.AttendeeRole = (await _meetingAttendeeRepository.GetByMeetingId(mid)).Where(x => x.userId == userid).FirstOrDefault().attendeeRole;
            
            meeting.meetingStatus = 2;
            ViewBag.MeetingType = meeting.meetingType;
            var updatemeeting = _meetingRepository.Update(meeting);
            return View();
        }
        public async Task<ActionResult> Index()
        {
            var list = await _meetingRepository.GetAll();
            var meetings = list.Select(x => new MeetingViewModel
            {
                meetingId = x.meetingId,
                meetingDate = x.meetingDate,
                meetingEndTime = x.meetingEndTime,
                meetingNumber = x.meetingNumber,
                meetingStartTime = x.meetingStartTime,
                meetingStatus = x.meetingStatus,
                meetingSubject = x.meetingSubject,
                meetingType = x.meetingType,
                boardName = _boardRepository.GetById(x.boardId).Result.boardName
            }).ToList();
            return View();
        }
        public async Task<ActionResult> AddMeeting()
        {
            ViewBag.Rooms = await _roomRepository.GetAll();
            ViewBag.Success = TempData["success"];
            ViewBag.Users = await _userRepository.GetAll();
            ViewBag.Boards = await _boardRepository.GetAll();

            ViewBag.MeetingNum = (await _meetingRepository.GetAll()).Count() + 1 ;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddMeeting(FormCollection formData, HttpPostedFileBase aksfileupload)
        {
            
            Meeting modelmeeting = new Meeting();
            //var filess = formdata["UploadedFileName"].ToList();
            //modelmeeting.meetingId =Convert.ToInt32(formdata["meetingId"].ToString());
            //var d = formdata["UploadedFileName"];
            modelmeeting.meetingIsCancel = false;
            modelmeeting.meetingNumber = formData["meetingNumber"].ToString();
            modelmeeting.meetingSubject = formData["meetingSubject"].ToString();
            modelmeeting.meetingDate = Convert.ToDateTime(formData["meetingDate"].ToString()); 
            modelmeeting.meetingStartTime = formData["meetingStartTime"].ToString(); 
            modelmeeting.meetingEndTime = formData["meetingEndTime"].ToString();
            modelmeeting.meetingComment = formData["meetingComment"].ToString();
            modelmeeting.meetingStatus = 1;
            modelmeeting.degreeOfImportance = 0;
            modelmeeting.roomId = Convert.ToInt32(formData["roomId"].ToString());
            //modelmeeting.meetingType = formData["meetingType"].ToString();
            modelmeeting.boardId = Convert.ToInt32(formData["boardId"].ToString()); 
            modelmeeting.meetingCancelReason = "";
            modelmeeting.meetingStatus = 1;
            modelmeeting.meetingType = "2";
            var response = await _meetingRepository.Add(modelmeeting);
            var Attendees = formData["Attendees"].ToString().Split(new char[] { ',' }).ToList();
            var userIds = formData["userId"].Split(new char[] { ',' }).ToList();
            var AttendeeNames = formData["attendeeName"].Split(new char[] { ',' }).ToList();
            var AttendeeOrginations = formData["attendeeOrganization"].Split(new char[] { ',' }).ToList();
            var AttendeeRoles = formData["attendeeRole"].Split(new char[] { ',' }).ToList();
           


            if (response.isSuccess == true)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("~/Content/Uploads/") + fileName); //File will be saved in application root
                    var amenitymodel = new AmenityViewModel();
                    amenitymodel.amenityName = file.FileName;
                    amenitymodel.meetingId = ((Meeting)response.data).meetingId;
                    amenitymodel.amenityIcon = ((Meeting)response.data).meetingId.ToString();
                    var amenityresponse = await _amenityRepository.AddAsync(amenitymodel);
                    if (amenityresponse.isSuccess)
                    {
                        file.SaveAs(Server.MapPath("~/Content/Uploads/") + fileName);
                    }
                //To save file, use SaveAs method
                //file.SaveAs(Server.MapPath("~/") + fileName); //File will be saved in application root
                }
                List<MeetingAttendee> listOfAttendees = new List<MeetingAttendee>();
                for (int i = 0; i < formData["attendeeName"].Split(new char[] { ',' }).ToList().Count(); i++)
                {
                    var user = await _userRepository.GetById(userIds[i]);

                    var MeetingAttendee = new MeetingAttendee();
                    MeetingAttendee.attendeeMobile = user.mobile;

                    MeetingAttendee.userId = userIds[i];
                    MeetingAttendee.attendeeName = formData["attendeeName"].Split(new char[] { ',' }).ToList()[i];
                    MeetingAttendee.attendeeOrganization = AttendeeOrginations[i];
                    MeetingAttendee.attendeeRole = Convert.ToInt32(AttendeeRoles[i]);
                    MeetingAttendee.meetingId = ((Meeting)response.data).meetingId;
                    MeetingAttendee.attendeeDesignation = "";
                    var index = formData["deviceuserId"].Split(new char[] { ',' }).ToList().FindIndex(x => x == userIds[i]);
                    MeetingAttendee.attendeeEmail = formData["deviceId"].Split(new char[] { ',' }).ToList()[index];
                        listOfAttendees.Add(MeetingAttendee);
                }
                var sendAttendeerequest = await _meetingAttendeeRepository.BulkAdd(listOfAttendees);
                for (int i = 0; i < formData["deviceId"].Split(new char[] { ',' }).ToList().Count(); i++)
                {
                    var device = await _deviceRepository.GetById(Convert.ToInt32(formData["deviceId"].Split(new char[] { ',' }).ToList()[i]));
                    if (!String.IsNullOrEmpty(formData["deviceuserId"].Split(new char[] { ',' }).ToList()[i]))
                    {
                        device.userId = formData["deviceuserId"].Split(new char[] { ',' }).ToList()[i];
                        var updatedevice = await _deviceRepository.Update(device);
                    }

                }
                if (sendAttendeerequest.isSuccess)
                {
                    for (int i = 0; i < formData["attendeeName"].Split(new char[] { ',' }).ToList().Count(); i++)
                    {
                        NotificationHub objNotifHub = new NotificationHub();
                        //await objNotifHub.SendNotification(userIds[i]);
                    }
                    if(formData["speakerTitle"] != null) { 
                    for (int i = 0; i < formData["speakerTitle"].Split(new char[] { ',' }).ToList().Count(); i++)
                    {
                        var MeetingAgenda = new MeetingAgenda();
                        MeetingAgenda.agendaTitle = formData["speakerTitle"].Split(new char[] { ',' }).ToList()[i];
                        MeetingAgenda.meetingAgendaSpeakerId = 0;
                        MeetingAgenda.agendaDescription = "";
                        MeetingAgenda.meetingId = ((Meeting)response.data).meetingId;
                        MeetingAgenda.meetingAgendaAttachments = new List<MeetingAgendaAttachment>();
                        var agendarequestresponse = await _meetingAgendaRepository.Add(MeetingAgenda);
                        if (agendarequestresponse.isSuccess)
                        {
                            var Mettingspeaker = new MeetingAgendaSpeaker();
                            Mettingspeaker.meetingAgendaId = ((MeetingAgenda)agendarequestresponse.data).meetingAgendaId;
                            Mettingspeaker.meetingAttendeeId = ((List<MeetingAttendee>)sendAttendeerequest.data).Where(x=>x.userId == formData["speakeruserId"].Split(new char[] { ',' }).ToList()[i]).FirstOrDefault().meetingAttendeeId;
                            Mettingspeaker.subject = ((MeetingAgenda)agendarequestresponse.data).agendaTitle;
                            Mettingspeaker.time = formData["speakerTime"].Split(new char[] { ',' }).ToList()[i];
                            var agendaspeakerrequest = await _meetingAgendaSpeakerRepository.Add(Mettingspeaker);
                        }
                    }
                    }
                    if (formData["QuestionTitle"] != null)
                    {
                        for (int i = 0; i < formData["QuestionTitle"].Split(new char[] { ',' }).ToList().Count(); i++)
                        {
                            //var meetingagendaPoll = new LocalMeetingPoll();
                            //meetingagendaPoll.MeetingId = ((Meeting)response.data).meetingId;
                            //meetingagendaPoll.PollQuestion = formdata["QuestionTitle"].Split(new char[] { ',' }).ToList()[i];
                            //meetingagendaPoll.status = 0;
                            //mmsDBContext dbcontect = new mmsDBContext();
                            //dbcontect.LocalMeetingPolls.Add(meetingagendaPoll);
                            //dbcontect.SaveChanges();
                            //var options = formdata["answers"].Split(new char[] { ',' }).ToList()[i].Split(new char[] { ';' }).ToList();
                            //foreach (var option in options)
                            //{
                            //    var meetingAgendaPollOption = new LocalMeetingPollOptions();
                            //    meetingAgendaPollOption.OptionValue = option;
                            //    var maxid = dbcontect.LocalMeetingPolls.Max(x => x.Id);
                            //    meetingAgendaPollOption.PollId = dbcontect.LocalMeetingPolls.Find(maxid).Id;
                            //    dbcontect.LocalMeetingPollOptions.Add(meetingAgendaPollOption);
                            //    dbcontect.SaveChanges();
                            //}
                            var meetingagendaPoll = new MeetingAgendaPoll();
                            meetingagendaPoll.pollQuestion = formData["QuestionTitle"].Split(new char[] { ',' }).ToList()[i];
                            meetingagendaPoll.meetingId = ((Meeting)response.data).meetingId;
                            meetingagendaPoll.pollStatus = 0;
                            var meetingagendaPollRequest = await _meetingAgendaPollRepository.Add(meetingagendaPoll);
                            if (meetingagendaPollRequest.isSuccess)
                            {
                                var options = formData["answers"].Split(new char[] { ',' }).ToList()[i].Split(new char[] { ';' }).ToList();
                                foreach (var option in options)
                                {
                                    var meetingAgendaPollOption = new MeetingAgendaPollOption();
                                    meetingAgendaPollOption.optionValue = option;
                                    meetingAgendaPollOption.pollId = (await _meetingAgendaPollRepository.GetAll()).LastOrDefault().pollId;
                                    var meetingAgendaPollOptionrequest = await _meetingAgendaPollOptionRepository.Add(meetingAgendaPollOption);
                                }
                            }
                        }
                    }
                }
                TempData["success"] = true;
                
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(MeetingViewModel model, FormCollection formdata)
        {
            Meeting modelmeeting = new Meeting();
            modelmeeting.meetingId = model.meetingId;
            modelmeeting.meetingIsCancel = false;
            modelmeeting.meetingNumber = model.meetingNumber;
            modelmeeting.meetingSubject = model.meetingSubject;
            modelmeeting.meetingDate = model.meetingDate;
            modelmeeting.meetingStartTime = model.meetingStartTime;
            modelmeeting.meetingEndTime = model.meetingEndTime;
            modelmeeting.meetingStatus = model.meetingStatus;
            modelmeeting.degreeOfImportance = model.degreeOfImportance;
            modelmeeting.meetingType = model.meetingType;
            modelmeeting.boardId = model.boardId;
            modelmeeting.meetingCancelReason = "";
            modelmeeting.roomId = Convert.ToInt32(formdata["roomId"].ToString());
            modelmeeting.meetingStatus = 1;
            modelmeeting.meetingType = "1";
            modelmeeting.meetingComment = formdata["meetingComment"].ToString();
            var response = await _meetingRepository.Add(modelmeeting);
            var Attendees = formdata["Attendees"].ToString().Split(new char[] { ',' }).ToList();
            var userIds = formdata["userId"].Split(new char[] { ',' }).ToList();
            var AttendeeNames = formdata["attendeeName"].Split(new char[] { ',' }).ToList();
            var AttendeeOrginations = formdata["attendeeOrganization"].Split(new char[] { ',' }).ToList();
            var AttendeeRoles = formdata["attendeeRole"].Split(new char[] { ',' }).ToList();
            if (response.isSuccess == true)
            {
                var MeetingAgenda = new MeetingAgenda();
                MeetingAgenda.agendaTitle = "";
                MeetingAgenda.meetingAgendaSpeakerId = 0;
                MeetingAgenda.agendaDescription = "";
                MeetingAgenda.meetingId = ((Meeting)response.data).meetingId;
                MeetingAgenda.meetingAgendaAttachments = new List<MeetingAgendaAttachment>();
                var agendarequestresponse = await _meetingAgendaRepository.Add(MeetingAgenda);
                List<MeetingAttendee> listOfAttendees = new List<MeetingAttendee>();
                for (int i = 0; i < formdata["attendeeName"].Split(new char[] { ',' }).ToList().Count(); i++)
                {
                    var user = await _userRepository.GetById(userIds[i]);

                    var MeetingAttendee = new MeetingAttendee();
                    MeetingAttendee.attendeeMobile = user.mobile;

                    MeetingAttendee.userId = userIds[i];
                    MeetingAttendee.attendeeName = formdata["attendeeName"].Split(new char[] { ',' }).ToList()[i];
                    MeetingAttendee.attendeeOrganization = AttendeeOrginations[i];
                    MeetingAttendee.meetingId = ((Meeting)response.data).meetingId;
                    MeetingAttendee.attendeeDesignation = "";
                    MeetingAttendee.attendeeRole = Convert.ToInt32(AttendeeRoles[i]);
                    var index = formdata["deviceuserId"].Split(new char[] { ',' }).ToList().FindIndex(x => x == userIds[i]);
                    MeetingAttendee.attendeeEmail = formdata["deviceId"].Split(new char[] { ',' }).ToList()[index];
                    listOfAttendees.Add(MeetingAttendee);
                }
                var sendAttendeerequest = await _meetingAttendeeRepository.BulkAdd(listOfAttendees);
                
                for (int i = 0; i < formdata["deviceId"].Split(new char[] { ',' }).ToList().Count(); i++)
                {
                    var device = await _deviceRepository.GetById(Convert.ToInt32(formdata["deviceId"].Split(new char[] { ',' }).ToList()[i]));
                    if(!String.IsNullOrEmpty(formdata["deviceuserId"].Split(new char[] { ',' }).ToList()[i]) )
                    {
                        device.userId = formdata["deviceuserId"].Split(new char[] { ',' }).ToList()[i];
                            var updatedevice = await _deviceRepository.Update(device);
                    }
                 
                }
                var s = formdata["Attendees"].Split(new char[] { ',' }).ToList();
                    TempData["success"] = true;
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            TempData["success"] = false;
            return Json(false, JsonRequestBehavior.AllowGet);
            }
        public async Task<ActionResult> Add()
        {
            ViewBag.MeetingNum = (await _meetingRepository.GetAll()).Count() + 1;
            ViewBag.Rooms = await _roomRepository.GetAll();
            ViewBag.Success = TempData["success"];
            ViewBag.Users = await _userRepository.GetAll();
            ViewBag.Boards = await _boardRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddLongMeeting(MeetingViewModel model, FormCollection formdata)
        {
            Meeting modelmeeting = new Meeting();
            modelmeeting.meetingId = model.meetingId;
            modelmeeting.meetingIsCancel = false;
            modelmeeting.meetingNumber = model.meetingNumber;
            modelmeeting.meetingSubject = model.meetingSubject;
            modelmeeting.meetingDate = model.meetingDate;
            modelmeeting.meetingStartTime = model.meetingStartTime;
            modelmeeting.meetingEndTime = model.meetingEndTime;
            modelmeeting.meetingStatus = model.meetingStatus;
            modelmeeting.degreeOfImportance = model.degreeOfImportance;
            modelmeeting.meetingType = model.meetingType;
            modelmeeting.boardId = model.boardId;
            modelmeeting.meetingCancelReason = "";
            
            var response = await _meetingRepository.Add(modelmeeting);
            var Attendees = formdata["Attendees"].ToString().Split(new char[] { ',' }).ToList();

            for (int i = 0; i < formdata["Attendees"].Count(); i++)
            {

            }
            if (response.isSuccess == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
        public async Task<ActionResult> View(int Id)
        {
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();

            var model = (await _meetingAttendeeRepository.GetByMeetingId(Id.ToString())).Where(x=>x.userId == userid).FirstOrDefault();
            var attendeemodel = new MeetingAttendeeViewModel();
            attendeemodel.boardname = (await _boardRepository.GetById((await _meetingRepository.GetById(Id)).boardId)).boardName;
            attendeemodel.Attendee = model;
            //attendeemodel.boardname = "";
            attendeemodel.meeting = await _meetingRepository.GetById(attendeemodel.Attendee.meetingId);
            attendeemodel.Attendees = await _meetingAttendeeRepository.GetByMeetingId(model.meetingId.ToString());
            attendeemodel.roomname = (await _roomRepository.GetById((int)attendeemodel.meeting.roomId)).roomName;
            var meetingAgendas = (await _meetingAgendaRepository.GetAll()).Where(x => x.meetingId == model.meetingId).ToList();
            ViewBag.Amenities = (await _amenityRepository.GetAll()).Where(x => x.amenityIcon == Id.ToString()).ToList();
            ViewBag.Devices = (await _deviceRepository.GetAll()).Where(x => x.roomId == attendeemodel.meeting.roomId).ToList();
            attendeemodel.Speakers = (await _meetingAgendaSpeakerRepository.GetAll()).Where(x => meetingAgendas.Select(w => w.meetingAgendaId).ToList().Contains(x.meetingAgendaId)).ToList();
            ViewBag.Success = TempData["success"];
            return View(attendeemodel);
        }
        public async Task<ActionResult> GetApprovedAttendee(int meetingId)
        {
            var attendee = (await _meetingAttendeeRepository.GetByMeetingId(meetingId.ToString())).Where(x => x.inviteStatus == 1).ToList();
            return Json(attendee, JsonRequestBehavior.AllowGet);
        }
        [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
        public async Task<ActionResult> ApproveInvitation(int id)
        {
            var model = await _meetingAttendeeRepository.GetById(id);
            model.inviteStatus = 1;
            var response = await _meetingAttendeeRepository.Update(model);
            if (response.isSuccess)
            {
                TempData["success"] = true;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            TempData["success"] = false;
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
        public async Task<ActionResult> RejectInvitation(int id, string inviteStatusReason)
        {
            var model = await _meetingAttendeeRepository.GetById(id);
            model.inviteStatus = 2;
            model.inviteStatusReason = inviteStatusReason;
            var response = await _meetingAttendeeRepository.Update(model);
            if (response.isSuccess)
            {
                TempData["success"] = true;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            TempData["success"] = false;
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public class VottingConnectionData
        {
            public String connectionId { get; set; }
            public string meeting_id { get; set; }
            public string userId { get; set; }
            public List<string> value { get; set; }
        }
        [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
        public async Task<ActionResult> AddVote(List<string> votearray, string userid)
        {
            foreach(var value in votearray)
            {
                var votemodel = new MeetingPollVottingViewwModel();
                votemodel.userId = userid;
                votemodel.pollId =Convert.ToInt32(value.Split(',').ToList()[0]);
                votemodel.pollOptionId = Convert.ToInt32(value.Split(',').ToList()[1]);
                var voteresponse = await _pollVottingRepository.Add(votemodel);
                
            }
            return Json(true, JsonRequestBehavior.AllowGet);
            //var votemodel = 
            //var model = await _pollVottingRepository.Ad(id);
            //model.inviteStatus = 2;
            //model.inviteStatusReason = inviteStatusReason;
            //var response = await _meetingAttendeeRepository.Update(model);
            //if (response.isSuccess)
            //{
            //    TempData["success"] = true;
            //    return Json(true, JsonRequestBehavior.AllowGet);
            //}
            //TempData["success"] = false;
            
        }
        [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
        public async Task<ActionResult> GetVottingResult(List<VottingConnectionData> votearray,string mid)
        {
            var meetingId = Convert.ToInt32(mid);
            var pollvotting = new List<MeetingPollVottingViewwModel>();
            foreach (var obj in votearray)
            {
                foreach(var option in obj.value) { 
                var votemodel = new MeetingPollVottingViewwModel();
                votemodel.userId = obj.userId;
                votemodel.pollId = Convert.ToInt32(option.Split(',').ToList()[0]);
                votemodel.pollOptionId = Convert.ToInt32(option.Split(',').ToList()[1]);
                pollvotting.Add(votemodel);
                }
            }
            var polls = (await _meetingAgendaPollRepository.GetAll()).Where(x => x.meetingId == meetingId).ToList();
            
            var PollOptions = (await _meetingAgendaPollOptionRepository.GetAll()).Where(x => polls.Select(w => w.pollId).ToList().Contains(x.pollId)).ToList();

            var consolidatedChildren =
    from c in pollvotting
    group c by new
    {
        c.pollId,
        c.pollOptionId,

    } into gcs
    select new MeetingResultViewModle()
    {
        PollId = gcs.Key.pollId,
        PollOption = gcs.Key.pollOptionId,
        Count = gcs.ToList().Count,
        PollTitle = polls.Where(x => x.pollId == gcs.Key.pollId).Select(x => x.pollQuestion).FirstOrDefault(),
        Polloptionvalue = PollOptions.Where(x => x.pollOptionId == gcs.Key.pollOptionId).Select(x => x.optionValue).FirstOrDefault(),
    };
            //var pollId = (await _meetingAgendaPollRepository.GetByMeetingId(meetingId)).Select(x => x.pollId).ToList();
            //var pollvotting = (await _pollVottingRepository.GetAll()).Where(x => pollId.Contains(x.pollId)).GroupBy(x=>x.pollId).ToList();
            return Json(new Tuple<List<MeetingResultViewModle>,List<int>>(consolidatedChildren.ToList(),polls.Select(x=>x.pollId).ToList()), JsonRequestBehavior.AllowGet);
        }
       
    }
}