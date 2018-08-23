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

using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    public class Agent : JsonModel, IStatementTarget
    {
        public static readonly string OBJECT_TYPE = "Agent";
        public string ObjectType => OBJECT_TYPE;

        public string Name { get; set; }
        public string Mbox { get; set; }
        public string MboxSha1Sum { get; set; }
        public string Openid { get; set; }
        public AgentAccount Account { get; set; }

        public Agent() { }

        public Agent(StringOfJson json) : this(json.ToJObject()) { }

        public Agent(JObject jobj)
        {
            if (jobj["name"] != null)
            {
                Name = jobj.Value<string>("name");
            }

            if (jobj["mbox"] != null)
            {
                Mbox = jobj.Value<string>("mbox");
            }
            if (jobj["mbox_sha1sum"] != null)
            {
                MboxSha1Sum = jobj.Value<string>("mbox_sha1sum");
            }
            if (jobj["openid"] != null)
            {
                Openid = jobj.Value<string>("openid");
            }
            if (jobj["account"] != null)
            {
                Account = (AgentAccount)jobj.Value<JObject>("account");
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject
            {
                { "objectType", ObjectType }
            };

            if (Name != null)
            {
                result.Add("name", Name);
            }

            if (Account != null)
            {
                result.Add("account", Account.ToJObject(version));
            }
            else if (Mbox != null)
            {
                result.Add("mbox", Mbox);
            }
            else if (MboxSha1Sum != null)
            {
                result.Add("mbox_sha1sum", MboxSha1Sum);
            }
            else if (Openid != null)
            {
                result.Add("openid", Openid);
            }

            return result;
        }

        public static explicit operator Agent(JObject jobj)
        {
            return new Agent(jobj);
        }
    }
}
