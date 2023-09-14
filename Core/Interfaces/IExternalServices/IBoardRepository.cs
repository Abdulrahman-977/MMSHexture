using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IBoardRepository
    {
         Task<List<BoardViewwModel>> GetAll();
         Task<ResponseViewModel> Add(BoardViewwModel model);
         Task<ResponseViewModel> Update(BoardViewwModel model);
         Task<BoardViewwModel> GetById(int Id);
        Task<bool> Delete(int Id);
        Task<ResponseViewModel> AddBoardUser(BoardUserViewModel model);
        Task<ResponseViewModel> UpdateBoardUser(BoardUserViewModel model);
        Task<BoardUserViewModel> GetBoardUserById(int Id);
    }
}
