using MojoPin.Frontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace MojoPin.Frontend.Services
{
    public class InstagramService
    {
        private string GetRecentMediaUrl = "https://api.instagram.com/v1/users/{0}/media/recent/?access_token={1}";
        private string AccessToken = "21905669.e6a976d.266994125d6a4820b10a8379a4984632";
        private string UserId = "21905669";
        public List<InstragramMediaModel> GetMedia(int numberOfItems)
        {
            var list = new List<InstragramMediaModel>();

            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(string.Format(GetRecentMediaUrl, UserId, AccessToken)).Result;

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var t1 = serializer.Deserialize<Response>(response);
                return t1.Data.Take(numberOfItems).ToList();
            }

            throw new Exception($"No User Found with id {UserId}");

        }
        public class Response
        {
            [JsonProperty("data")]
            public List<InstragramMediaModel> Data { get; set; }
        }
    }
}