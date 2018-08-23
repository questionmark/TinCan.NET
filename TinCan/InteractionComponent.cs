/*
    Copyright 2015 Rustici Software
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
    public class InteractionComponent : JsonModel
    {
        public string Id;
        public LanguageMap Description { get; set; }

        public InteractionComponent()
        {

        }

        public InteractionComponent(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                Id = jobj.Value<string>("id");
            }
            if (jobj["description"] != null)
            {
                Description = (LanguageMap)jobj.Value<JObject>("description");
            }
 
        }

        public override JObject ToJObject(TCAPIVersion version) {
            var result = new JObject();

            if (Id != null)
            {
                result.Add("id", Id);
            }
            if (Description != null && !Description.IsEmpty())
            {
                result.Add("description", Description.ToJObject(version));
            }

            return result;
        }

        public static explicit operator InteractionComponent(JObject jobj)
        {
            return new InteractionComponent(jobj);
        }

    }

}
