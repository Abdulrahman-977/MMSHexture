using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Helper
{
    public class SessionHandler
    {
        
        public void Save<VarType>(string key, VarType value)
        {
            HttpContext.Current.Session[key] = JsonConvert.SerializeObject(value);
        }
        public VarType Load<VarType>(string key, VarType def)
        {
            var value = HttpContext.Current.Session[key].ToString();
            return value == null ? def : JsonConvert.DeserializeObject<VarType>(value);
        }
        public void ClearSession()
        {
            HttpContext.Current.Session.Clear();
            //FormsAuthentication.SignOut();
        }
        public void RemoveKey(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}
