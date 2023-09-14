using Infrastructure.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Unity;

namespace WebApplication5.Models
{
    public class UserHubModels
    {
        public string UserName { get; set; }
             public HashSet<string> ConnectionIds { get; set; }
    }
    public class NotificationHub : Hub
    {
        
       
        private static readonly ConcurrentDictionary<string, UserHubModels> Users =
            new ConcurrentDictionary<string, UserHubModels>(StringComparer.InvariantCultureIgnoreCase);


        public  Task Connect(string userId)
        {
            string userName = userId;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserHubModels
            {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            UserHubModels user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        UserHubModels removedUser;
                        Users.TryRemove(userName, out removedUser);
                        Clients.Others.userDisconnected(userName);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }
        //public async Task GetNotification(string userId)
        //{
        //    try
        //    {

        //        //Get TotalNotification  
        //        string totalNotif = await LoadNotifData(userId);

        //        //Send To  
        //        UserHubModels receiver;
        //        if (Users.TryGetValue(userId, out receiver))
        //        {
        //            var cid = receiver.ConnectionIds.FirstOrDefault();
        //            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //            context.Clients.Client(cid).broadcaastNotif(totalNotif);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}
        public async Task SendNotification(string SentTo, string message,string title,string img)
        {
            try
            {
                //Get TotalNotification  
                //string totalNotif = await LoadNotifData(SentTo);

                //Send To  
                UserHubModels receiver;
                if (Users.TryGetValue(SentTo, out receiver))
                {
                    var cid = receiver.ConnectionIds;
                    foreach(var id in cid)
                    {
                        var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                        context.Clients.Client(id).broadcaastNotif(message, title, img, SentTo);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        //private async Task<string> LoadNotifData(string userId)
        //{
        //    int total = 0;
           
        //    total = (await _notiRepository.GetAllByUserId(userId)).Count ;
        //    return total.ToString();
        //}
    }
}