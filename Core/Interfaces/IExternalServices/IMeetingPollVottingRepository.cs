using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IMeetingPollVottingRepository
    {
        Task<List<MeetingPollVottingViewwModel>> GetAll();
        Task<ResponseViewModel> Add(MeetingPollVottingViewwModel model);
        Task<ResponseViewModel> Update(MeetingPollVottingViewwModel model);
        Task<MeetingPollVottingViewwModel> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
