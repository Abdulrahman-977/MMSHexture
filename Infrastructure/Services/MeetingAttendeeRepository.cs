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
    public class MeetingAttendeeRepository : IMeetingAttendeeRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public MeetingAttendeeRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }

        public async Task<ResponseViewModel> Add(MeetingAttendee model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}MeetingAttendee", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAttendee>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAttendee() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAttendee() };
            }
        }

        public async Task<List<MeetingAttendee>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingAttendee", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAttendee>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAttendee>();
            }
            return new List<MeetingAttendee>();
        }
        public async Task<MeetingAttendee> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingAttendee/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAttendee>(content);
                    return Content;
                }
            }
            catch
            {
                return new MeetingAttendee();
            }
            return null;
        }
        public async Task<ResponseViewModel> Update(MeetingAttendee model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}MeetingAttendee", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<MeetingAttendee>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAttendee() };
            }
            catch(Exception ex)
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAttendee() };
            }
        }
        public async Task<List<MeetingAttendee>> GetByUserId(string userId)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingAttendee", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAttendee>>(content).Where(x=>x.userId == userId).ToList();
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAttendee>();
            }
            return new List<MeetingAttendee>();
        }
        public async Task<ResponseViewModel> BulkAdd(List<MeetingAttendee> list)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}MeetingAttendee/bulk", list, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAttendee>>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new MeetingAttendee() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new MeetingAttendee() };
            }
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}MeetingAttendee/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<MeetingAttendee>> GetByMeetingId(string mid)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingAttendee", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<MeetingAttendee>>(content).Where(x => x.meetingId == Convert.ToInt32(mid)).ToList();
                    return Content;
                }
            }
            catch
            {
                return new List<MeetingAttendee>();
            }
            return new List<MeetingAttendee>();
        }
    }
}
