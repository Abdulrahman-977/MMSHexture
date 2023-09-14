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
    public class TaskRepository : ITaskRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public TaskRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }
        public async Task<ResponseViewModel> Add(TaskViewModel model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}MeetingNotes", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<TaskViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new TaskViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new TaskViewModel() };
            }
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}MeetingNotes/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<TaskViewModel>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingNotes", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<TaskViewModel>>(content);
                    return Content;
                }
            }
            catch (Exception ex)
            {
                return new List<TaskViewModel>();
            }
            return new List<TaskViewModel>();
        }

        public async Task<List<TaskViewModel>> GetByMeetingId(int mid)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}MeetingNotes", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<TaskViewModel>>(content).Where(x => x.meetingId == mid).ToList();
                    return Content;
                }
            }
            catch
            {
                return new List<TaskViewModel>();
            }
            return new List<TaskViewModel>();
        }
    }
}
