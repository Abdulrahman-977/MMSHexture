using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IExternalServices
{
    public interface IRestOperation
    {
        Task<HttpResponseMessage> Post<T>(string Url, T data, string accessToken);//
        Task<HttpResponseMessage> Get(string Url, string accessToken); //, string accessToken
        Task<HttpResponseMessage> Put<T>(string Url, T data, string accessToken);
        Task<HttpResponseMessage> Delete(string Url, string accessToken);

    }
}
