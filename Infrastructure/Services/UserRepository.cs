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
    public class UserRepository : IUserRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public UserRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }
        public async Task<List<UserDropDownViewModel>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Account/AllUsers", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<UserDropDownViewModel>>(content);
                    return Content;
                }
            }
            catch(Exception ex)
            {
                return new List<UserDropDownViewModel>();
            }
            return new List<UserDropDownViewModel>();
        }
        public async Task<UserDropDownViewModel> GetById(string UserId)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Account/User/"+UserId, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<UserDropDownViewModel>(content);
                    return Content;
                }
            }
            catch (Exception ex)
            {
                return new UserDropDownViewModel();
            }
            return new UserDropDownViewModel();
        }
        public async Task<bool> Delete(string Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}Account/DeleteUser/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
        public async Task<ResponseViewModel> Add(AddUserViewModel model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}Account/User", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Device>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new AddUserViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new AddUserViewModel() };
            }
        }
        public async Task<ResponseViewModel> UpdateProfile(UpdateUserProfileViewModel model)
        {
            var response = await _restOperation.Post("https://mms.compass-dx.com/api/Account/UpdateProfile", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<UpdateUserProfileViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new UpdateUserProfileViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new UpdateUserProfileViewModel() };
            }
        }

        public async Task<ResponseViewModel> ChangePassword(ChangePasswordViewModel model)
        {
            var response = await _restOperation.Post("https://mms.compass-dx.com/api/Account/ChangePassword", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<ChangePasswordViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new ChangePasswordViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new ChangePasswordViewModel() };
            }
        }

        public async Task<UserDropDownViewModel> GetByEmailAsync(string email)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Account/AllUsers", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<UserDropDownViewModel>>(content);
                    return Content.Where(x=>x.email == email).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
