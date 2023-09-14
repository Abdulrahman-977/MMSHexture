using Core.Helper;
using Core.Interfaces.IExternalServices;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public RoomRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }

        public async Task<ResponseViewModel> Add(RoomViewModel model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}Room", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<RoomViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new RoomViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new RoomViewModel() };
            }
        }

        public async Task<List<RoomViewModel>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Room", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<RoomViewModel>>(content);
                    return Content;
                }
            }
            catch(Exception ex)
            {
                return new List<RoomViewModel>();
            }
            return new List<RoomViewModel>();
        }
        public async Task<RoomViewModel> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Room/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<RoomViewModel>(content);
                    return Content;
                }
            }
            catch
            {
                return new RoomViewModel();
            }
            return null;
        }
        public async Task<ResponseViewModel> Update(RoomViewModel model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}Room", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<RoomViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new RoomViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new RoomViewModel() };
            }
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}Room/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
        public async Task<string> GetNameById(int Id)
        {
            var response = await _restOperation.Get($"{Constatnts.APIUrl}Room/" + Id, _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Content = JsonConvert.DeserializeObject<RoomViewModel>(content);
                return Content.roomName;
            }
            return null;
        }
    }
}
