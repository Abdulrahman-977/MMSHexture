using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class AuthData
    {
        public object userId { get; set; }
        public string title { get; set; }
        public string fullname { get; set; }
        public string designation { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public bool emailConfirmed { get; set; }
        public DateTime registerationDate { get; set; }
        public string gender { get; set; }
        public TokenInfo tokenInfo { get; set; }
        public List<object> claims { get; set; }
    }

    public class TokenViewModel
    {
        public AuthData authData { get; set; }
    }

    public class TokenInfo
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
        public DateTime expiryDate { get; set; }
        public string issuer { get; set; }
    }
    
}
