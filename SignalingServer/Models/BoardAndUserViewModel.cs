using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class BoardAndUserViewModel
    {
        public int boardId { get; set; }
        public string boardName { get; set; }
        public bool boardIsActive { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string usereamail { get; set; }
    }
}