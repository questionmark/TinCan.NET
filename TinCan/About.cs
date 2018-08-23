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

using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    public class About : JsonModel
    {
        public List<TCAPIVersion> Version { get; set; }
        public Extensions Extensions { get; set; }

        public About(string str) : this(new StringOfJson(str)) {}
        public About(StringOfJson json) : this(json.ToJObject()) {}

        public About(JObject jobj)
        {
            if (jobj["version"] != null)
            {
                Version = new List<TCAPIVersion>();
                foreach (string item in jobj.Value<JArray>("version"))
                {
                    Version.Add((TCAPIVersion)item);
                }
            }
            if (jobj["extensions"] != null)
            {
                Extensions = new Extensions(jobj.Value<JObject>("extensions"));
            }
        }

        public override JObject ToJObject(TCAPIVersion version) {
            var result = new JObject();
            if (Version != null)
            {
                var versions = new JArray();
                foreach (var v in Version) {
                    versions.Add(v.ToString());
                }
                result.Add("version", versions);
            }

            if (Extensions != null && ! Extensions.IsEmpty())
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }
    }
}