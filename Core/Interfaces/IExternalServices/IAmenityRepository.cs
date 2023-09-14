using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IAmenityRepository
    {
        Task<List<AmenityViewModel>> GetAll();
        Task<ResponseViewModel> AddAsync(AmenityViewModel model);
        Task<ResponseViewModel> Update(AmenityViewModel model);
        Task<AmenityViewModel> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
