using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class DeviceViewModel
    {
        public int deviceId { get; set; }
        public string ipAddress { get; set; }
        public bool isTouchScreen { get; set; }
        public int roomId { get; set; }
        public string roomName { get; set; }
        public string UserName { get; set; }
        public string userId { get; set; }
        public bool isActive { get; set; }
    }
}
