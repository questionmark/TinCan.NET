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
    public class StatementRef : JsonModel, IStatementTarget
    {
        public static readonly string OBJECT_TYPE = "StatementRef";
        public string ObjectType => OBJECT_TYPE;

        public Guid? Id { get; set; }

        public StatementRef() {}
        public StatementRef(Guid id)
        {
            Id = id;
        }

        public StatementRef(StringOfJson json): this(json.ToJObject()) {}

        public StatementRef(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                Id = new Guid(jobj.Value<string>("id"));
            }
        }

        public override JObject ToJObject(TCAPIVersion version) {
            var result = new JObject
            {
                { "objectType", ObjectType }
            };

            if (Id != null)
            {
                result.Add("id", Id.ToString());
            }

            return result;
        }

        public static explicit operator StatementRef(JObject jobj)
        {
            return new StatementRef(jobj);
        }
    }

}
