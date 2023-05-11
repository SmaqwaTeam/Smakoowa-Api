﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smakoowa_Api.Services.Interfaces.Helper;

namespace Smakoowa_Api.Services.Helper
{
    public class JsonKeyValueGetter : IJsonKeyValueGetter
    {
        public string GetValueByKey(string jsonString, string key)
        {
            var json = (JObject)JsonConvert.DeserializeObject(jsonString);
            return json[key].Value<string>();
        }
    }
}
