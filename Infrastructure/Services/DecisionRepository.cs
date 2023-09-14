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
    public class DecisionRepository :IDecisionRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public DecisionRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }
        public async Task<ResponseViewModel> Add(DecisionViewModel model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}Decision", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<DecisionViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new DecisionViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new DecisionViewModel() };
            }
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}Decision/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<DecisionViewModel>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Decision", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<DecisionViewModel>>(content);
                    return Content;
                }
            }
            catch (Exception ex)
            {
                return new List<DecisionViewModel>();
            }
            return new List<DecisionViewModel>();
        }

        public async Task<List<DecisionViewModel>> GetByMeetingId(int mid)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Decision", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<DecisionViewModel>>(content).Where(x => x.meetingId == mid).ToList();
                    return Content;
                }
            }
            catch
            {
                return new List<DecisionViewModel>();
            }
            return new List<DecisionViewModel>();
        }
    }
}
