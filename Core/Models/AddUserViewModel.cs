using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AddUserViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string title { get; set; }
        public string fullname { get; set; }
        public string designation { get; set; }
        public string mobile { get; set; }
        public string role { get; set; }
        public bool sendValidationEmail { get; set; }
        public string gender { get; set; }
    }
    public class UpdateUserProfileViewModel{
        public string userId { get; set; }
        public string fullname { get; set; }
        public string designation { get; set; }
        public string mobile { get; set; }
        public string title { get; set; }
        public string profileImage { get; set; }
        public string gender { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string role { get; set; }
    }
}
