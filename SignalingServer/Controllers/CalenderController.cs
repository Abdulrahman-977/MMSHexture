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
    public class CalenderController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingRepository _meetingRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IRoomRepository _roomRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IBoardRepository _boardRepository { get; set; }
        // GET: Calender
        public async Task<ActionResult> Index()
        {
            ViewBag.Boards = await _boardRepository.GetAll();
            ViewBag.Rooms = await _roomRepository.GetAll();
            var Meetings = await _meetingRepository.GetByUserId(UserToken.Token.authData.userId.ToString());
            return View(Meetings);
        }
    }
}