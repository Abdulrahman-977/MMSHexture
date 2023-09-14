using Core.Helper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public static class UserToken
    {
        public  static SessionHandler _sessionHandler;
        public const string USER_SESSION_VALUE = "USER_SESSION_VALUE";
        
        public static TokenViewModel Token { get; set; }
      
    }
}