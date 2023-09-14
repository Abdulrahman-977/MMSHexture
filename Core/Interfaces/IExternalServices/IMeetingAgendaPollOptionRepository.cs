using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IMeetingAgendaPollOptionRepository
    {
        Task<List<MeetingAgendaPollOption>> GetAll();
        Task<ResponseViewModel> Add(MeetingAgendaPollOption model);
        Task<MeetingAgendaPollOption> GetByPollId(int Id);
        Task<ResponseViewModel> Update(MeetingAgendaPollOption model);
        Task<MeetingAgendaPollOption> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
