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
    public class BoardController : Controller
    {
        [Dependency]
        public Core.Interfaces.IExternalServices.IBoardRepository _boardRepository { get; set; }
        [Dependency]
        public Core.Interfaces.IExternalServices.IUserRepository _userRepository { get; set; }
        // GET: Board
        public async Task<ActionResult> Index()
        {
            ViewBag.Success = TempData["success"];
            var list = await _boardRepository.GetAll();
            var result = new List<BoardAndUserViewModel>();
            foreach(var board in list)
            {
                var model = new BoardAndUserViewModel();
                model.boardId = board.boardId;
                model.boardIsActive = board.boardIsActive;
                model.boardName = board.boardName;
                model.userId = board.userId;
                var boarduser = await _boardRepository.GetBoardUserById(board.boardId);
                if (boarduser != null)
                {

                    model.userName = (await _userRepository.GetById(boarduser.userId)) != null ? (await _userRepository.GetById(boarduser.userId)).fullname : "";

                    model.usereamail = (await _userRepository.GetById(boarduser.userId)) != null ? (await _userRepository.GetById(boarduser.userId)).email : "";
                }
                else
                {
                    model.userName = "";
                    model.usereamail = "";
                }
                   
                result.Add(model);
            }
            return View(result);
        }
        public async Task<ActionResult> Add()
        {
            ViewBag.Success = TempData["success"];
            ViewBag.Users = await _userRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(BoardViewwModel model)
        {
            var response = await _boardRepository.Add(model);
            var boardusermodel = new BoardUserViewModel();
           
            if (response.isSuccess == true)
            {
                boardusermodel.boardId = ((BoardViewwModel)response.data).boardId;
                boardusermodel.userId = model.userId;
                boardusermodel.joiningDate = DateTime.Now;
                await _boardRepository.AddBoardUser(boardusermodel);
                TempData["success"] = true;
                return RedirectToAction("Index");
            }
            TempData["success"] = false;
            ViewBag.Success = TempData["success"];
            ViewBag.Users = await _userRepository.GetAll();
            return View();
        }
        public async Task<ActionResult> Edit(int Id)
        {
            ViewBag.Success = TempData["success"];
            ViewBag.Users = await _userRepository.GetAll();
            var model = await _boardRepository.GetById(Id);
            var modeluser = await _boardRepository.GetBoardUserById(Id);
            model.userId = modeluser.userId;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int BoardId, BoardViewwModel model)
        {
            
            var boardusermodel = new BoardUserViewModel();

            var response = await _boardRepository.Update(model);
            if (response.isSuccess == true)
            {
                 boardusermodel = await _boardRepository.GetBoardUserById(model.boardId);
                if(boardusermodel != null)
                {
                    boardusermodel.boardId = ((BoardViewwModel)response.data).boardId;
                    boardusermodel.userId = model.userId;
                    boardusermodel.joiningDate = DateTime.Now;
                    await _boardRepository.UpdateBoardUser(boardusermodel);
                }
                else
                {
                    boardusermodel.boardId = ((BoardViewwModel)response.data).boardId;
                    boardusermodel.userId = model.userId;
                    boardusermodel.joiningDate = DateTime.Now;
                    await _boardRepository.AddBoardUser(boardusermodel);
                }
                TempData["success"] = true;
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _boardRepository.Delete(Id);
            if (response)
            {
                TempData["success"] = true;
                return RedirectToAction("Index");
            }
            TempData["success"] = false;
            return RedirectToAction("Index");
        }
    }
    
}