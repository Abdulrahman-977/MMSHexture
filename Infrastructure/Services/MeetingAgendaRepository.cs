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
    public class MeetingAgendaRepository : IMeetingAgendaRepository
    {private readonly IRestOperation _restOperation;
        private readonly IMeetingAttendeeRepository _meetingAttendee;
        private readonly UserToken _userToken;
        public MeetingAgendaRepository(IRestOperation restOperation, UserToken userToken, IMeetingAttendeeRepository meetingAttendee)
        {
            _restOperation = restOperation;
            _userToken = userToken;
            _meetingAttendee = meetingAttendee;
        }
        public async Task<ResponseViewModel> Add(MeetingAgenda model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}MeetingAgenda", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAgenda>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgenda() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAgenda() };
            }
        }

        public async Task<List<MeetingAgenda>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingAgenda", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAgenda>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAgenda>();
            }
            return new List<MeetingAgenda>();
        }

        public async Task<MeetingAgenda> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingAgenda/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAgenda>(content);
                    return Content;
                }
            }
            catch
            {
                return new MeetingAgenda();
            }
            return null;
        }

        public Task<ResponseViewModel> Update(MeetingAgenda model)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}MeetingAgenda/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
