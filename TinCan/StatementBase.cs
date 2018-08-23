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
    public abstract class StatementBase : JsonModel
    {
        private const string IsoDateTimeFormat = "o";

        public Agent Actor { get; set; }
        public Verb Verb { get; set; }
        public IStatementTarget Target { get; set; }
        public Result Result { get; set; }
        public Context Context { get; set; }
        public DateTime? Timestamp { get; set; }

        protected StatementBase() { }
        protected StatementBase(StringOfJson json) : this(json.ToJObject()) { }

        protected StatementBase(JObject jobj)
        {
            if (jobj["actor"] != null)
            {
                if (jobj["actor"]["objectType"] != null && (string)jobj["actor"]["objectType"] == Agent.OBJECT_TYPE)
                {
                    Actor = (Group)jobj.Value<JObject>("actor");
                }
                else
                {
                    Actor = (Agent)jobj.Value<JObject>("actor");
                }
            }
            if (jobj["verb"] != null)
            {
                Verb = (Verb)jobj.Value<JObject>("verb");
            }
            if (jobj["object"] != null)
            {
                if (jobj["object"]["objectType"] != null)
                {
                    if ((string)jobj["object"]["objectType"] == Agent.OBJECT_TYPE)
                    {
                        Target = (Group)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == Agent.OBJECT_TYPE)
                    {
                        Target = (Agent)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == Activity.OBJECT_TYPE)
                    {
                        Target = (Activity)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == StatementRef.OBJECT_TYPE)
                    {
                        Target = (StatementRef)jobj.Value<JObject>("object");
                    }
                }
                else
                {
                    Target = (Activity)jobj.Value<JObject>("object");
                }
            }
            if (jobj["result"] != null)
            {
                Result = (Result)jobj.Value<JObject>("result");
            }
            if (jobj["context"] != null)
            {
                Context = (Context)jobj.Value<JObject>("context");
            }
            if (jobj["timestamp"] != null)
            {
                Timestamp = jobj.Value<DateTime>("timestamp");
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Actor != null)
            {
                result.Add("actor", Actor.ToJObject(version));
            }

            if (Verb != null)
            {
                result.Add("verb", Verb.ToJObject(version));
            }

            if (Target != null)
            {
                result.Add("object", Target.ToJObject(version));
            }
            if (Result != null)
            {
                result.Add("result", Result.ToJObject(version));
            }
            if (Context != null)
            {
                result.Add("context", Context.ToJObject(version));
            }
            if (Timestamp != null)
            {
                result.Add("timestamp", Timestamp.Value.ToString(IsoDateTimeFormat));
            }

            return result;
        }
    }
}
