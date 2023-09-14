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
    public class AmenityRepository : IAmenityRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public AmenityRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }
        public async Task<ResponseViewModel> AddAsync(AmenityViewModel model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}Amenity", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<AmenityViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new AmenityViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new AmenityViewModel() };
            }
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AmenityViewModel>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Amenity", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<AmenityViewModel>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<AmenityViewModel>();
            }
            return new List<AmenityViewModel>();
        }

        public Task<AmenityViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseViewModel> Update(AmenityViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
