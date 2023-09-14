using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.LocalDataModels
{
    public class Decision
    {
        [Key]
        public int Id { get; set; }
        public int meetingId { get; set; }
        public int BoardId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}