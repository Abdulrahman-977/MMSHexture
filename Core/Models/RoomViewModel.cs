using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Device
    {
        public int deviceId { get; set; }
        public string ipAddress { get; set; }
        public bool isTouchScreen { get; set; }
        public int roomId { get; set; }
        public string userId { get; set; }
        public bool isActive { get; set; }
    }

    public class RoomAmenity
    {
        public int roomId { get; set; }
        public int amenityId { get; set; }
        public string amenityName { get; set; }
        public string amenityIcon { get; set; }
    }

    public class RoomViewModel
    {
        public int roomId { get; set; }
        public string roomName { get; set; }
        public string roomLocation { get; set; }
        public bool roomIsClosed { get; set; }
        public string roomIsClosedReason { get; set; }
        public bool roomIsActive { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public int capacity { get; set; }
        public string userId { get; set; }
        public List<RoomAmenity> roomAmenities { get; set; }
        public List<Device> devices { get; set; }
    }
    
}
