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
    public class Statement : StatementBase
    {
        // TODO: put in common location
        private const string IsoDateTimeFormat = "o";

        public Guid? Id { get; set; }
        public DateTime? Stored { get; set; }
        public Agent Authority { get; set; }
        public TCAPIVersion Version { get; set; }
        //public List<Attachment> attachments { get; set; }

        public Statement() : base() { }
        public Statement(StringOfJson json) : this(json.ToJObject()) { }

        public Statement(JObject jobj) : base(jobj) {
            if (jobj["id"] != null)
            {
                Id = new Guid(jobj.Value<string>("id"));
            }
            if (jobj["stored"] != null)
            {
                Stored = jobj.Value<DateTime>("stored");
            }
            if (jobj["authority"] != null)
            {
                Authority = (Agent)jobj.Value<JObject>("authority");
            }
            if (jobj["version"] != null)
            {
                Version = (TCAPIVersion)jobj.Value<string>("version");
            }

            //
            // handle SubStatement as target which isn't provided by StatementBase
            // because SubStatements are not allowed to nest
            //
            if (jobj["object"] != null && (string)jobj["object"]["objectType"] == SubStatement.OBJECT_TYPE)
            {
                Target = (SubStatement)jobj.Value<JObject>("object");
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = base.ToJObject(version);

            if (Id != null)
            {
                result.Add("id", Id.ToString());
            }
            if (Stored != null)
            {
                result.Add("stored", Stored.Value.ToString(IsoDateTimeFormat));
            }
            if (Authority != null)
            {
                result.Add("authority", Authority.ToJObject(version));
            }
            if (version != null)
            {
                result.Add("version", version.ToString());
            }

            return result;
        }

        public void Stamp()
        {
            if (Id == null)
            {
                Id = Guid.NewGuid();
            }
            if (Timestamp == null)
            {
                Timestamp = DateTime.UtcNow;
            }
        }
    }
}
