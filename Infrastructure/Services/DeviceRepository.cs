using Core.Helper;
using Core.Interfaces.IExternalServices;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public DeviceRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }

        public async Task<ResponseViewModel> Add(Device model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}Device", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Device>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new Device() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new Device() };
            }
        }

        public async Task<List<Device>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Device", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<Device>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<Device>();
            }
            return new List<Device>();
        }
        public async Task<Device> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Device/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Device>(content);
                    return Content;
                }
            }
            catch
            {
                return new Device();
            }
            return null;
        }
        public async Task<ResponseViewModel> Update(Device model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}Device", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Device>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new Device() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new Device() };
            }
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}Device/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
