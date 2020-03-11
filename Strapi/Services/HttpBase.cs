using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Strapi.Services
{
    public class HttpBase : IHttpBase
    {
        public async Task<string> Get(string Url, string Token = null)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.GET);
            if (!string.IsNullOrEmpty(Token))
            {
                request.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            }

            request.AddHeader("Accept", "*/*");
            IRestResponse result = await client.ExecuteAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Content;
            }
            return null;
        }

        public async Task<string> Put(string Url, string json, string Token = null)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.PUT);
            if (!string.IsNullOrEmpty(Token))
            {
                request.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            }
            if (json != null)
            {
                request.AddJsonBody(json);
            }

            IRestResponse result = await client.ExecuteAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Content;
            }
            return null;
        }

        public async Task<string> Post(string Url, string json, string Token = null)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);
            if (!string.IsNullOrEmpty(Token))
            {
                request.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            }
            if (json != null)
            {
                request.AddJsonBody(json);
            }
            IRestResponse result = await client.ExecuteAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Content;
            }
            return null;
        }

        public async Task<string> Delete(string Url, int id = 0, string Token = null)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.DELETE);
            if (!string.IsNullOrEmpty(Token))
            {
                request.AddParameter("Authorization", string.Format("Bearer " + Token), ParameterType.HttpHeader);
            }
            IRestResponse result = await client.ExecuteAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result.Content;
            }
            return null;
        }
    }
}
