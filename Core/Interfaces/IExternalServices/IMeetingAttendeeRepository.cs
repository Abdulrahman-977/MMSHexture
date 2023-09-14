using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IMeetingAttendeeRepository
    {
         Task<List<MeetingAttendee>> GetAll();
         Task<ResponseViewModel> Add(MeetingAttendee model);
         Task<ResponseViewModel> Update(MeetingAttendee model);
         Task<MeetingAttendee> GetById(int Id);
        Task<List<MeetingAttendee>> GetByUserId(string userId);
        Task<List<MeetingAttendee>> GetByMeetingId(string mid);
        Task<ResponseViewModel> BulkAdd(List<MeetingAttendee> list);
        Task<bool> Delete(int Id);
    }
}
