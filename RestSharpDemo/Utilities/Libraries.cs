using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo.Utilities
{
    public static class Libraries
    {
        public static Dictionary<string, string> DeserializeResponse(this IRestResponse restResponse)
        {
            var JSONObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse);
            return JSONObj;
        }
        
        public static string GetResponseObjects(this IRestResponse response, string responseObject1, string responseObject2)
        {
            JObject obs = JObject.Parse(response.Content);
            return obs[responseObject1][responseObject2].ToString();
        }

        public static string GetResponseObject(this IRestResponse response, string responseObject)
        {
            JObject obs = JObject.Parse(response.Content);
            return obs[responseObject].ToString();
        }
    }
}
