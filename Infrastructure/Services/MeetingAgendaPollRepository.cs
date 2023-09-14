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
    public class MeetingAgendaPollRepository : IMeetingAgendaPollRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public MeetingAgendaPollRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }
        public async Task<ResponseViewModel> Add(MeetingAgendaPoll model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}MeetingPoll", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Device>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaPoll() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaPoll() };
            }
        }

        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}MeetingPoll/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<MeetingAgendaPoll>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingPoll", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAgendaPoll>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAgendaPoll>();
            }
            return null;
        }

        public async Task<MeetingAgendaPoll> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingPoll/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAgendaPoll>(content);
                    return Content;
                }
            }
            catch
            {
                return new MeetingAgendaPoll();
            }
            return null;
        }

        public async Task<ResponseViewModel> Update(MeetingAgendaPoll model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}MeetingPoll", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAgendaPoll>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaPoll() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaPoll() };
            }
        }
        public async Task<List<MeetingAgendaPoll>> GetByMeetingId(int Id)
        {
            try
            {
                var response = await _restOperation.Get("https://mms.compass-dx.com/api/MeetingPoll", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAgendaPoll>>(content).Where(x => x.meetingId == Id).ToList();
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAgendaPoll>();
            }
            return new List<MeetingAgendaPoll>();
        }
    }
}
