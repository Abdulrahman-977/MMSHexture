﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
   public class ResetPasswordViewModel
    {
        public string userId { get; set; }
        public string code { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
