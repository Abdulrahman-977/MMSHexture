using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BoardViewwModel
    {
        public int boardId { get; set; }
        public string boardName { get; set; }
        public bool boardIsActive { get; set; }
        public string userId { get; set; }
    }
}
