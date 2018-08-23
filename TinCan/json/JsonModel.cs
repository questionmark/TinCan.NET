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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TinCan.Json
{
    public abstract class JsonModel : IJsonModel
    {
        // TODO: rename methods to ToJObject and ToJSON
        public abstract JObject ToJObject(TCAPIVersion version);

        public JObject ToJObject()
        {
            return ToJObject(TCAPIVersion.Latest());
        }

        public string ToJson(TCAPIVersion version, bool pretty = false)
        {
            var formatting = Formatting.None;
            if (pretty)
            {
                formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(ToJObject(version), formatting);
        }

        public string ToJson(bool pretty = false)
        {
            return ToJson(TCAPIVersion.Latest(), pretty);
        }
    }
}
