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
    [Models.AuthorizeUser("Admin,Director,SystemDirector,SimpleUser")]
    public class ArchivesController : Controller
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
        public Core.Interfaces.IExternalServices.IRoomRepository _roomAgendaRepository { get; set; }
        // GET: Archives
        public async Task<ActionResult> Index()
        {
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();

            var Meetings = (await _meetingRepository.GetByUserId(userid)).ToList();
            return View(Meetings);
        }
        public async Task<ActionResult> Delete(int Id)
        {
            var model = await _meetingRepository.Delete(Id);
            if (model)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }
        public async Task<ActionResult> Report(int Id)
        {
            var userid = JsonConvert.DeserializeObject<Core.Models.TokenViewModel>(Session[WebApplication5.Models.UserToken.USER_SESSION_VALUE].ToString()).authData.userId.ToString();

            var model = (await _meetingAttendeeRepository.GetByMeetingId(Id.ToString())).Where(x => x.userId == userid).FirstOrDefault();
            var attendeemodel = new MeetingAttendeeViewModel();
            attendeemodel.boardname = (await _boardRepository.GetById((await _meetingRepository.GetById(Id)).boardId)).boardName;
            attendeemodel.Attendee = model;
            //attendeemodel.boardname = "";
            attendeemodel.meeting = await _meetingRepository.GetById(attendeemodel.Attendee.meetingId);
            attendeemodel.Attendees = await _meetingAttendeeRepository.GetByMeetingId(model.meetingId.ToString());
            attendeemodel.roomname = (await _roomAgendaRepository.GetById((int)attendeemodel.meeting.roomId)).roomName;
            var meetingAgendas = (await _meetingAgendaRepository.GetAll()).Where(x => x.meetingId == model.meetingId).ToList();
            ViewBag.Amenities = (await _amenityRepository.GetAll()).Where(x => x.amenityIcon == Id.ToString()).ToList();
            ViewBag.Decitions = await _decisionRepository.GetByMeetingId(Id);
            ViewBag.Tasks = await _taskRepository.GetByMeetingId(Id);
            ViewBag.Users = await _userRepository.GetAll();
            attendeemodel.Speakers = (await _meetingAgendaSpeakerRepository.GetAll()).Where(x => meetingAgendas.Select(w => w.meetingAgendaId).ToList().Contains(x.meetingAgendaId)).ToList();
            ViewBag.Success = TempData["success"];
            var pollvotting = new List<MeetingPollVottingViewwModel>();
            var pollids = (await _meetingAgendaPollRepository.GetByMeetingId(Id));
            var votearray = (await _pollVottingRepository.GetAll()).Where(x => pollids.Select(w => w.pollId).ToList().Contains(x.pollId)).ToList();
            var polls = (await _meetingAgendaPollRepository.GetAll()).Where(x => x.meetingId == Id).ToList();
            var PollOptions = (await _meetingAgendaPollOptionRepository.GetAll()).Where(x => polls.Select(w => w.pollId).ToList().Contains(x.pollId)).ToList();
            var votting = (await _pollVottingRepository.GetAll()).Where(x => polls.Select(w => w.pollId).ToList().Contains(x.pollId)).ToList();
            List<VottingArrchiveViewModle> listOfVotes = new List<VottingArrchiveViewModle>();
            foreach(var vote in votting)
            {
                VottingArrchiveViewModle votemodel = new VottingArrchiveViewModle();
                votemodel.username = (await _userRepository.GetById(vote.userId)).fullname;
                votemodel.pollQuestion = polls.Where(x => x.pollId == vote.pollId).FirstOrDefault().pollQuestion;
                votemodel.pollOption = PollOptions.Where(x => x.pollOptionId == vote.pollOptionId).FirstOrDefault().optionValue;
                listOfVotes.Add(votemodel);
            }
            ViewBag.Votes = listOfVotes.GroupBy(x=>x.pollQuestion).Select(x=> new VottingArrchiveViewModleGroupBy {
                Key = x.Key,
                Element = x.ToList()
            }).ToList();
            //        var consolidatedChildren =
            //from c in pollvotting
            //group c by new
            //{
            //    c.pollId,
            //    c.pollOptionId,

            //} into gcs
            //select new MeetingResultViewModle()
            //{
            //    PollId = gcs.Key.pollId,
            //    PollOption = gcs.Key.pollOptionId,
            //    Count = gcs.ToList().Count,
            //    PollTitle = polls.Where(x => x.pollId == gcs.Key.pollId).Select(x => x.pollQuestion).FirstOrDefault(),
            //    Polloptionvalue = PollOptions.Where(x => x.pollOptionId == gcs.Key.pollOptionId).Select(x => x.optionValue).FirstOrDefault(),
            //};
            //        ViewBag.Votes = consolidatedChildren;
            return View(attendeemodel);
        }
    }
}