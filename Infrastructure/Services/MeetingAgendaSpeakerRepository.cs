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
    public class MeetingAgendaSpeakerRepository : IMeetingAgendaSpeakerRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly IMeetingAttendeeRepository _meetingAttendee;
        private readonly UserToken _userToken;
        public MeetingAgendaSpeakerRepository(IRestOperation restOperation, UserToken userToken, IMeetingAttendeeRepository meetingAttendee)
        {
            _restOperation = restOperation;
            _userToken = userToken;
            _meetingAttendee = meetingAttendee;
        }
        public async Task<ResponseViewModel> Add(MeetingAgendaSpeaker model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}MeetingAgendaSpeaker", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAgendaSpeaker>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaSpeaker() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgendaSpeaker() };
            }
        }

        public async Task<List<MeetingAgendaSpeaker>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingAgendaSpeaker", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAgendaSpeaker>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAgendaSpeaker>();
            }
            return new List<MeetingAgendaSpeaker>();
        }

        public async Task<MeetingAgendaSpeaker> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingAgendaSpeaker/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAgendaSpeaker>(content);
                    return Content;
                }
            }
            catch
            {
                return new MeetingAgendaSpeaker();
            }
            return null;
        }

        public Task<ResponseViewModel> Update(MeetingAgendaSpeaker model)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}MeetingAgendaSpeaker/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

    }
}