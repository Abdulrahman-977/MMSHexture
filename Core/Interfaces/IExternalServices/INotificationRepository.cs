using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface INotificationRepository
    {
        Task<List<NotificationViewModel>> GetAll();
        Task<List<NotificationViewModel>> GetAllByUserId(string userId);
        Task<ResponseViewModel> Add(NotificationViewModel model);
        Task<ResponseViewModel> Update(NotificationViewModel model);
        Task<NotificationViewModel> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
