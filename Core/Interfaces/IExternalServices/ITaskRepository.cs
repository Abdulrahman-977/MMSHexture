using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface ITaskRepository
    {
        Task<ResponseViewModel> Add(TaskViewModel model);
        Task<bool> Delete(int Id);
        Task<List<TaskViewModel>> GetByMeetingId(int Id);
        Task<List<TaskViewModel>> GetAll();
    }
}
