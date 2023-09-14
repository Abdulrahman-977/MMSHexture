using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IMeetingAgendaPollRepository
    {
        Task<List<MeetingAgendaPoll>> GetAll();
        Task<ResponseViewModel> Add(MeetingAgendaPoll model);
        Task<ResponseViewModel> Update(MeetingAgendaPoll model);
        Task<MeetingAgendaPoll> GetById(int Id);
        Task<List<MeetingAgendaPoll>> GetByMeetingId(int Id);
        Task<bool> Delete(int Id);
    }
}
