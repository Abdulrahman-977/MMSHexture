using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IDeviceRepository
    {
         Task<List<Device>> GetAll();
         Task<ResponseViewModel> Add(Device model);
         Task<ResponseViewModel> Update(Device model);
         Task<Device> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
