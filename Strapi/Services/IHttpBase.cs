using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strapi.Services
{
    public interface IHttpBase
    {
        Task<string> Get(string Url, string Token = null);
        Task<string> Put(string Url, string json, string Token = null);
        Task<string> Post(string Url, string json, string Token = null);
        Task<string> Delete(string Url, int id = 0, string Token = null);
    }
}
