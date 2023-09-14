using Core.Helper;
using Core.Interfaces.IExternalServices;
using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IRestOperation _restOperation;
        private readonly UserToken _userToken;
        public BoardRepository(IRestOperation restOperation, UserToken userToken)
        {
            _restOperation = restOperation;
            _userToken = userToken;
        }

        public async Task<ResponseViewModel> Add(BoardViewwModel model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}Board", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<BoardViewwModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new BoardViewwModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new BoardViewwModel() };
            }
        }
        public async Task<ResponseViewModel> AddBoardUser(BoardUserViewModel model)
        {
            var response = await _restOperation.Post($"{Constatnts.APIUrl}BoardUser", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<BoardUserViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new BoardUserViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new BoardUserViewModel() };
            }
        }
        public async Task<List<BoardViewwModel>> GetAll()
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Board", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<BoardViewwModel>>(content);
                    return Content;
                }
            }
            catch
            {
                return new List<BoardViewwModel>();
            }
            return new List<BoardViewwModel>();
        }
        public async Task<BoardViewwModel> GetById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}Board/"+Id, _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<BoardViewwModel>(content);
                    return Content;
                }
            }
            catch
            {
                return new BoardViewwModel();
            }
            return null;
        }
        public async Task<ResponseViewModel> Update(BoardViewwModel model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}Board", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<BoardViewwModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new BoardViewwModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new BoardViewwModel() };
            }
        }
        public async Task<bool> Delete(int Id)
        {
            var response = await _restOperation.Delete($"{Constatnts.APIUrl}Board/{Id}", _userToken.Token.authData.tokenInfo.token);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
        public async Task<ResponseViewModel> UpdateBoardUser(BoardUserViewModel model)
        {
            var response = await _restOperation.Put($"{Constatnts.APIUrl}BoardUser", model, _userToken.Token.authData.tokenInfo.token);//
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<BoardUserViewModel>(content);
                    return new ResponseViewModel { isSuccess = true, data = Content };
                }
                return new ResponseViewModel { isSuccess = false, data = new BoardUserViewModel() };
            }
            catch
            {
                return new ResponseViewModel { isSuccess = false, data = new BoardUserViewModel() };
            }
        }
        public async Task<BoardUserViewModel> GetBoardUserById(int Id)
        {
            try
            {
                var response = await _restOperation.Get($"{Constatnts.APIUrl}BoardUser", _userToken.Token.authData.tokenInfo.token);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Content = JsonConvert.DeserializeObject<List<BoardUserViewModel>>(content);
                    return Content.Where(x=>x.boardId == Id).FirstOrDefault();
                }
            }
            catch
            {
                return new BoardUserViewModel();
            }
            return null;
        }
    }
}
