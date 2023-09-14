using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BoardUserViewModel
    {
        public int boardUserId { get; set; }
          public string userId { get; set; }
          public int boardId { get; set; }
          public DateTime joiningDate { get; set; }
    }
}
