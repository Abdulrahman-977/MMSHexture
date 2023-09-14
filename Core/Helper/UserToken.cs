using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class UserToken
    {

        private readonly SessionHandler _sessionHandler;
        private const string USER_SESSION_VALUE = "USER_SESSION_VALUE";
        public UserToken(SessionHandler sessionHandler)
        {
            _sessionHandler = sessionHandler;
        }
        public TokenViewModel Token
        {
            get
            {
                return _sessionHandler.Load<TokenViewModel>(USER_SESSION_VALUE, new TokenViewModel());
            }
            set
            {
                _sessionHandler.Save<TokenViewModel>(USER_SESSION_VALUE, value);
            }

        }
    }
}
