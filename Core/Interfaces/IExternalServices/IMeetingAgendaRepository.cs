using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IMeetingAgendaRepository
    {
        Task<List<MeetingAgenda>> GetAll();
        Task<ResponseViewModel> Add(MeetingAgenda model);
        Task<ResponseViewModel> Update(MeetingAgenda model);
        Task<MeetingAgenda> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
