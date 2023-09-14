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
    public class MeetingPollVottingRepository : IMeetingPollVottingRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public MeetingPollVottingRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }
        public async Task<ResponseViewModel> Add(MeetingPollVottingViewwModel model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}MeetingPollVoting", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Device>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingPollVottingViewwModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingPollVottingViewwModel() };
            }
        }

        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}MeetingPollVotting/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<MeetingPollVottingViewwModel>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get("https://mms.compass-dx.com/api/MeetingPollVoting", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingPollVottingViewwModel>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingPollVottingViewwModel>();
            }
            return null;
        }

        public async Task<MeetingPollVottingViewwModel> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingPollVotting/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingPollVottingViewwModel>(content);
                    return Content;
                }
            }
            catch
            {
                return new MeetingPollVottingViewwModel();
            }
            return null;
        }

        public async Task<ResponseViewModel> Update(MeetingPollVottingViewwModel model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}MeetingPollVotting", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingPollVottingViewwModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingPollVottingViewwModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingPollVottingViewwModel() };
            }
        }
    }
}
