using Core.Models;
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
    [Models.AuthorizeUser("Admin,Director,SystemDirector")]
    public class AgendaController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaRepository _agendaRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaSpeakerRepository _speakersRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAttendeeRepository _userRepository { get; set; }
        // GET: Agenda
        public async Task<ActionResult> Detail(int meetingId)
        {
            var list = (await _agendaRepository.GetAll()).Where(x => x.meetingId == meetingId).ToList();
            var speakers = (await _speakersRepository.GetAll()).Where(x => list.Select(w => w.meetingAgendaId).ToList().Contains(x.meetingAgendaId)).ToList();
            var result = new List<SpeakerViewModel>();
            foreach (var sp in speakers)
            {
                var model = new SpeakerViewModel();
                model.subject = sp.subject;
                model.time = sp.time;
                model.name = (await _userRepository.GetById(sp.meetingAttendeeId)).attendeeName;
                result.Add(model);
            }
            return View(result);
        }
    }
}