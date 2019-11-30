using System;
using System.Collections.Generic;

namespace Jrpg.System
{
    public class Configuration
    {
        private Dictionary<string, object> Config;

        public Configuration(string contents)
        {
            Config = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(contents);
        }

        public bool GetBoolean(string key)
        {
            return (bool)Config[key];
        }

        public string GetString(string key)
        {
            return (string)Config[key];
        }

        public T Get<T>(string key)
        {
            var type = Config[key].GetType();

            if (type == typeof(Newtonsoft.Json.Linq.JArray))
            {
                return ((Newtonsoft.Json.Linq.JArray)(Config[key])).ToObject<T>();
            }
            if (type == typeof(Newtonsoft.Json.Linq.JObject))
            {
                return ((Newtonsoft.Json.Linq.JObject)(Config[key])).ToObject<T>();
            }

            return ((Newtonsoft.Json.Linq.JObject)(Config[key])).ToObject<T>();
        }
    }
}
