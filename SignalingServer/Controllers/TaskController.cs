using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace WebApplication5.Controllers
{
    public class TaskController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.ITaskRepository _taskRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaRepository _agendaRepository { get; set; }
        // GET: Task
        public async Task<ActionResult> Add(string meetingid, string Task, string TaskUser, string datepicker)
        {
            int id = Convert.ToInt32(meetingid);
            var model = new TaskViewModel();
            model.defermentReason = TaskUser;
            model.meetingId = Convert.ToInt32(meetingid);
            model.notes = Task;
            model.expectedCompletionDate = Convert.ToDateTime(datepicker);
            model.meetingAgendaId = (await _agendaRepository.GetAll()).Where(x => x.meetingId == id).FirstOrDefault() != null ? (await _agendaRepository.GetAll()).Where(x => x.meetingId == id).FirstOrDefault().meetingAgendaId : 0;
            var response = await _taskRepository.Add(model);
            return Json(((TaskViewModel)response.data).meetingNotesId);
        }
    }
}