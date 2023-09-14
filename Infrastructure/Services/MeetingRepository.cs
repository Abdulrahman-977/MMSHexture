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
    public class MeetingRepository : IMeetingRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly IMeetingAttendeeRepository _meetingAttendee;
        private readonly UserToken _userToken;
        public MeetingRepository(IRestOperation restOperation, UserToken userToken, IMeetingAttendeeRepository meetingAttendee)
        {
            _restOperation = restOperation;
            _userToken = userToken;
            _meetingAttendee = meetingAttendee;
        }

        public async Task<ResponseViewModel> Add(Meeting model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}Meeting", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Meeting>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new Meeting() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new Meeting() };
            }
        }

        public async Task<List<Meeting>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Meeting", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<Meeting>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<Meeting>();
            }
            return new List<Meeting>();
        }
        public async Task<Meeting> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Meeting/" + Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Meeting>(content);
                    return Content;
                }
            }
            catch(Exception ex)
            {
                return new Meeting();
            }
            return null;
        }
        public async Task<ResponseViewModel> Update(Meeting model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}Meeting", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<Meeting>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new Meeting() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new Meeting() };
            }
        }
        public async Task<List<Meeting>> GetByUserId(string UserId)
        {
            try
            {
                var MeetingIds = (await _meetingAttendee.GetByUserId(UserId)).Select(x => x.meetingId).ToList();
                var Meetings = (await this.GetAll()).Where(x => MeetingIds.Contains(x.meetingId)).ToList();
                return Meetings;
            }catch(Exception ex)
            {
                return new List<Meeting>();
            }
            
        }
        public async Task<List<Meeting>> GetByStatus(int Status)
        {
            try { 
                var Meetings = (await this.GetAll()).Where(x => x.meetingStatus == Status).ToList();
                return Meetings;
            }catch(Exception ex)
            {
                return new List<Meeting>();
            }
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}Meeting/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

    }
}
