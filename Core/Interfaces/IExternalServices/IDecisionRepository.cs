using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IDecisionRepository
    {
        Task<ResponseViewModel> Add(DecisionViewModel model);
        Task<bool> Delete(int Id);
        Task<List<DecisionViewModel>> GetByMeetingId(int mid);
        Task<List<DecisionViewModel>> GetAll();
    }
}
