using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IMeetingAgendaSpeakerRepository
    {
        Task<List<MeetingAgendaSpeaker>> GetAll();
        Task<ResponseViewModel> Add(MeetingAgendaSpeaker model);
        Task<ResponseViewModel> Update(MeetingAgendaSpeaker model);
        Task<MeetingAgendaSpeaker> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
