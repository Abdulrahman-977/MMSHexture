using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  public   class UserDropDownViewModel
    {
        public string id { get; set; }
        public string fullname { get; set; }
        public string designation { get; set; }
        public string mobile { get; set; }
        public string title { get; set; }
        public  string profileImage { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string registrationDate { get; set; }
        public List<string> roles { get; set; }
    }
}
