using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using Unity;
using Core.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WebApplication5.Models
{
    
    public class UserConnectionData
    {
        public String connectionId { get; set; }
        public String user_id { get; set; }
        public String user_id_data { get; set; }
        public string meeting_id { get; set; }
        public int connctedusers { get; set; }
    }
    public class VottingConnectionData
    {
        public String connectionId { get; set; }
        public string meeting_id { get; set; }
        public string userId { get; set; }
        public List<string> value { get; set; }
    }
    public class WebRtcHub : Hub
    {
        private readonly Core.Interfaces.IExternalServices.ITaskRepository _taskRepository;
       
        [Dependency]
        public Core.Interfaces.IExternalServices.IMeetingAgendaRepository _agendaRepository { get; set; }
        public static List<UserConnectionData> _userConnections = new List<UserConnectionData>();
        public static List<VottingConnectionData> _vottingConnections = new List<VottingConnectionData>();
        public async Task<Object> senddecision(String meetingid, int boardId,string boardName, string title,string startDate,  string createdDate)
        {

            var other_users = _userConnections.Where(p => p.meeting_id == meetingid && p.connectionId != Context.ConnectionId).ToList();

            foreach (var v in other_users)
            {
                Clients.Client(v.connectionId).adddecision(boardName, title, startDate, createdDate);
            }


            return true;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public async Task<Object> sendvote(String meetingid,string userid, List<string> votearray)
        {
            _vottingConnections.Add(new VottingConnectionData { connectionId = Context.ConnectionId, meeting_id = meetingid, userId = userid, value = votearray });
            var other_users = _userConnections.Where(p => p.meeting_id == meetingid ).ToList();

            if (_vottingConnections.Where(p => p.meeting_id == meetingid).Count() == _userConnections.Where(p => p.meeting_id == meetingid).Count())
            {
                foreach (var v in other_users)
                {
                    Clients.Client(v.connectionId).showvottingresult(_vottingConnections);
                   
                }
                _vottingConnections.RemoveAll(p => p.meeting_id == meetingid);
            }
            return _vottingConnections.Where(p => p.meeting_id == meetingid).Count() == _userConnections.Where(p => p.meeting_id == meetingid).Count();
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public async  Task<Object> sendtask(String meetingid,string Task, string TaskUser, string datepicker1,string username)
        {
            
            var other_users = _userConnections.Where(p => p.meeting_id == meetingid && p.connectionId != Context.ConnectionId).ToList();

            foreach (var v in other_users)
            {
                Clients.Client(v.connectionId).addtask(Task, TaskUser, datepicker1, username);
            }


            return true;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public async Task<Object> deletedecisionserver(String meetingid, int decisionid,int decisionrang)
        {
            var result = await deleteDecision(decisionid);
            var other_users = _userConnections.Where(p => p.meeting_id == meetingid && p.connectionId != Context.ConnectionId).ToList();
            if (result) { 
            foreach (var v in other_users)
            {
                Clients.Client(v.connectionId).deletedecision(decisionrang);
            }


            return true;
            }
            return false;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public async Task<Object> deletetaskserver(String meetingid, int decisionid, int decisionrang)
        {
            var result = await deleteTask(decisionid);
            var other_users = _userConnections.Where(p => p.meeting_id == meetingid && p.connectionId != Context.ConnectionId).ToList();
            if (result)
            {
                foreach (var v in other_users)
                {
                    Clients.Client(v.connectionId).deletetask(decisionrang);
                }


                return true;
            }
            return false;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public Object startspeakers(String meetingid, bool status)
        {
            var other_users = _userConnections.Where(p => p.meeting_id == meetingid && p.connectionId != Context.ConnectionId).ToList();
            foreach (var v in other_users)
            {
                Clients.Client(v.connectionId).showspeakerstab(status);
            }


            return true;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public Object startvotting(String meetingid, bool status)
        {
            var other_users = _userConnections.Where(p => p.meeting_id == meetingid && p.connectionId != Context.ConnectionId).ToList();
            foreach (var v in other_users)
            {
                Clients.Client(v.connectionId).showvottingstab(status);
            }


            return true;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public Object startspeaker(String meetingid, string userId, string Time)
        {
            var other_users = _userConnections.Where(p => p.meeting_id == meetingid && p.connectionId != Context.ConnectionId).ToList();
            foreach (var v in other_users)
            {
                Clients.Client(v.connectionId).starttspeakermeeting(userId, Time);
            }


            return true;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public Object getconnectusers( String meetingid)
        {
            var other_userslength = _userConnections.Where(p => p.meeting_id == meetingid).ToList();
           

            return other_userslength;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public Object Connect(String dsiplayName, String meetingid,string user_iddata)
        {
                _userConnections.Add(new UserConnectionData()
                {
                    meeting_id = meetingid,
                    user_id = dsiplayName,
                    connectionId = Context.ConnectionId,
                    user_id_data = user_iddata
                });
            var other_userslength = _userConnections.Where(p => p.meeting_id == meetingid).Count();
            //if(_userConnections.Where(p => p.meeting_id == meetingid && p.user_id_data == user_iddata).FirstOrDefault() != null)
            //{

            //}
            var other_users = _userConnections.Where(p => p.meeting_id == meetingid && p.connectionId != Context.ConnectionId && p.user_id_data != user_iddata).ToList();
            foreach (var v in other_users)
            {
                Clients.Client(v.connectionId).informAboutNewConnection(dsiplayName, Context.ConnectionId, other_userslength, user_iddata);
            }

            return other_users;
            //var users = list.Select(p => p.user_id).ToList();
            //foreach (var v in list)
            //{
            //    Clients.Client(v.connectionId).informAboutUsers(users);
            //}
        }
        public override Task OnDisconnected(bool stopCalled)
        {

            var connectionId = Context.ConnectionId;
            var meeting_id = _userConnections.Where(p => p.connectionId == connectionId).Select(p => p.meeting_id).FirstOrDefault();
            _vottingConnections.RemoveAll(p => p.connectionId == connectionId);
            _userConnections.RemoveAll(p => p.connectionId == connectionId);
            var list = _userConnections.Where(p => p.meeting_id == meeting_id).ToList();
            
            foreach (var v in list)
            {
                Clients.Client(v.connectionId).informAboutConnectionEnd(connectionId, _userConnections.Where(p => p.connectionId == connectionId).FirstOrDefault().user_id_data);
                
            }
            //if (list.Count() == 0)
            //{
            //    EndMeeting(Convert.ToInt32(meeting_id));
            //}
            return base.OnDisconnected(stopCalled);
        }
        public void ExchangeSDP(string message, String to_connid)
        {
            var from_connId = Context.ConnectionId;
            Clients.Client(to_connid).exchangeSDP(message, from_connId);
        }

        public void reset()
        {
            var connectionId = Context.ConnectionId;
            var meetingid = _userConnections.Where(p => p.connectionId == connectionId).Select(p => p.meeting_id).FirstOrDefault();

            var list = _userConnections.Where(p => p.meeting_id == meetingid).ToList();
            _userConnections.RemoveAll(p => p.meeting_id == meetingid);
            
            foreach(var v in list)
            {
                Clients.Client(v.connectionId).reset();
            }
        }

        public void sendMessage(string message)
        {
            var connectionId = Context.ConnectionId;
            var obj = _userConnections.Where(p => p.connectionId == connectionId).FirstOrDefault();
            var meetingid = obj.meeting_id;
            var from = obj.user_id;

            var list = _userConnections.Where(p => p.meeting_id == meetingid).ToList();

            foreach (var v in list)
            {   
                Clients.Client(v.connectionId).showChatMessage(from, message, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
               
            }
           

        }

        public async void EndMeeting(int meetingId)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Core.Helper.Constatnts.APIUrl}Meeting/" + meetingId);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token.authData.tokenInfo.token);//
            var response = await httpClient.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Content = JsonConvert.DeserializeObject<Meeting>(content);
                Content.meetingStatus = 3;
                 httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, $"{Core.Helper.Constatnts.APIUrl}Meeting");
                 var Json = JsonConvert.SerializeObject(Content, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });
                HttpContent httpContent = new StringContent(Json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpRequestMessage.Content = httpContent;
                 httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token.authData.tokenInfo.token);
                var result = await httpClient.SendAsync(httpRequestMessage);
            }
                
        }
        public async Task<bool> deleteDecision(int decisionId)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{Core.Helper.Constatnts.APIUrl}Decision/{decisionId}");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token.authData.tokenInfo.token);
            var response = await httpClient.SendAsync(httpRequestMessage);
            //HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Core.Helper.Constatnts.APIUrl}Meeting/" + meetingId);
            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token.authData.tokenInfo.token);//
            //var response = await httpClient.SendAsync(httpRequestMessage);
            return (response.IsSuccessStatusCode);
            

        }
        public async Task<bool> deleteTask(int decisionId)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{Core.Helper.Constatnts.APIUrl}MeetingNotes/{decisionId}");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token.authData.tokenInfo.token);
            var response = await httpClient.SendAsync(httpRequestMessage);
            //HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Core.Helper.Constatnts.APIUrl}Meeting/" + meetingId);
            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserToken.Token.authData.tokenInfo.token);//
            //var response = await httpClient.SendAsync(httpRequestMessage);
            return (response.IsSuccessStatusCode);


        }
    }
}