using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IMeetingRepository
    {
         Task<List<Meeting>> GetAll();
         Task<ResponseViewModel> Add(Meeting model);
         Task<ResponseViewModel> Update(Meeting model);
         Task<Meeting> GetById(int Id);
        Task<List<Meeting>> GetByUserId(string UserId);
        Task<List<Meeting>> GetByStatus(int Status);
        Task<bool> Delete(int Id);
    }
}
