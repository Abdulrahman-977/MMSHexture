using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IRoomRepository
    {
         Task<List<RoomViewModel>> GetAll();
         Task<ResponseViewModel> Add(RoomViewModel model);
         Task<ResponseViewModel> Update(RoomViewModel model);
         Task<RoomViewModel> GetById(int Id);
         Task<bool> Delete(int  Id);
         Task<string> GetNameById(int Id);
    }
}
