/*
    Copyright 2014 Rustici Software
    Modifications copyright (C) 2018 Neal Daniel

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
*/
using System;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    public class AgentAccount : JsonModel
    {
        // TODO: check to make sure is absolute?
        public Uri HomePage { get; set; }
        public string Name { get; set; }

        public AgentAccount() { }

        public AgentAccount(StringOfJson json) : this(json.ToJObject()) { }

        public AgentAccount(JObject jobj)
        {
            if (jobj["homePage"] != null)
            {
                HomePage = new Uri(jobj.Value<string>("homePage"));
            }
            if (jobj["name"] != null)
            {
                Name = jobj.Value<string>("name");
            }
        }

        public AgentAccount(Uri homePage, string name)
        {
            HomePage = homePage;
            Name = name;
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();
            if (HomePage != null)
            {
                result.Add("homePage", HomePage.ToString());
            }
            if (Name != null)
            {
                result.Add("name", Name);
            }

            return result;
        }

        public static explicit operator AgentAccount(JObject jobj)
        {
            return new AgentAccount(jobj);
        }
    }
}
