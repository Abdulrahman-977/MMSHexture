using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class RoomHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.addNewMessageToPage(message);
        }
    }
}