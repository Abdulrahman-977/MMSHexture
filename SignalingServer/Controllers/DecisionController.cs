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
    public class DecisionController : Controller
    {
        // GET: Decision
        [Dependency]
        public Core.Interfaces.IExternalServices.IDecisionRepository _decisionRepository { get; set; }
        // GET: Task
        public async Task<ActionResult> Add(string meetingid, int boardId, string title, string startDate, string createdDate)
        {
            int id = Convert.ToInt32(meetingid);
            var model = new DecisionViewModel();
            model.title = title;
            model.meetingId = Convert.ToInt32(meetingid);
            model.startDate = Convert.ToDateTime(startDate);
            model.createdDate = Convert.ToDateTime(createdDate);
            model.boardId = boardId;
            var response = await _decisionRepository.Add(model);
            return Json(((DecisionViewModel)response.data).decisionId);
        }
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _decisionRepository.Delete(Id);
            if (response)
            {
                TempData["success"] = true;
                
            }
            TempData["success"] = false;
            return RedirectToAction("Index");
        }
    }
}