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
    public class MeetingAgendaPollOptionRepository : IMeetingAgendaPollOptionRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public MeetingAgendaPollOptionRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }
        public async Task<ResponseViewModel> Add(MeetingAgendaPollOption model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}MeetingPollOption", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Device>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaPollOption() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaPollOption() };
            }
        }

        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}MeetingPollOption/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<MeetingAgendaPollOption>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingPollOption", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAgendaPollOption>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAgendaPollOption>();
            }
            return null;
        }

        public async Task<MeetingAgendaPollOption> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingPollOption/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAgendaPollOption>(content);
                    return Content;
                }
            }
            catch
            {
                return new MeetingAgendaPollOption();
            }
            return null;
        }
        public async Task<List<MeetingAgendaPollOption>> GetByPollId(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingPollOption", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAgendaPollOption>>(content).Where(x => x.pollId == Id).ToList();
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAgendaPollOption>();
            }
            return new List<MeetingAgendaPollOption>();
        }
        public async Task<ResponseViewModel> Update(MeetingAgendaPollOption model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}MeetingExecutionAgendaPollOption", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAgendaPollOption>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaPollOption() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaPollOption() };
            }
        }

        Task<MeetingAgendaPollOption> IMeetingAgendaPollOptionRepository.GetByPollId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
