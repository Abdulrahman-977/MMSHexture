using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class RoomAmenity
    {
        public int roomId { get; set; }
        public int amenityId { get; set; }
        public string amenityName { get; set; }
        public string amenityIcon { get; set; }
    }

    public class Room
    {
        public int roomId { get; set; }
        public string roomName { get; set; }
        public string roomLocation { get; set; }
        public bool roomIsClosed { get; set; }
        public string roomIsClosedReason { get; set; }
        public bool roomIsActive { get; set; }
        public List<RoomAmenity> roomAmenities { get; set; }
    }
    
}