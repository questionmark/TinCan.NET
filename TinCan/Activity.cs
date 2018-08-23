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
    public class Activity : JsonModel, IStatementTarget
    {
        public static readonly string OBJECT_TYPE = "Activity";
        public string ObjectType => OBJECT_TYPE;

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                var uri = new Uri(value);
                _id = value;
            }
        }

        public ActivityDefinition Definition { get; set; }

        public Activity() { }

        public Activity(StringOfJson json) : this(json.ToJObject()) { }

        public Activity(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                var idFromJson = jobj.Value<string>("id");
                var uri = new Uri(idFromJson);
                Id = idFromJson;
            }
            if (jobj["definition"] != null)
            {
                Definition = (ActivityDefinition)jobj.Value<JObject>("definition");
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject
            {
                { "objectType", ObjectType }
            };

            if (Id != null)
            {
                result.Add("id", Id);
            }
            if (Definition != null)
            {
                result.Add("definition", Definition.ToJObject(version));
            }

            return result;
        }

        public static explicit operator Activity(JObject jobj)
        {
            return new Activity(jobj);
        }
    }
}
