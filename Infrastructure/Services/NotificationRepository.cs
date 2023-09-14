using Core.Helper;
using Core.Interfaces.IExternalServices;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public NotificationRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }
        public Task<ResponseViewModel> Add(NotificationViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}UserNotification/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public Task<List<NotificationViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<NotificationViewModel>> GetAllByUserId(string userId)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}UserNotification/" + userId, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<NotificationViewModel>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<NotificationViewModel>();
            }
            return new List<NotificationViewModel>();
        }

        public Task<NotificationViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel> Update(NotificationViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
