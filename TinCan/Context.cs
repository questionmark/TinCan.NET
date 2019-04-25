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
    public class Context : JsonModel
    {
        public Guid? Registration { get; set; }
        public Agent Instructor { get; set; }
        public Agent Team { get; set; }
        public ContextActivities ContextActivities { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string Language { get; set; }
        public StatementRef Statement { get; set; }
        public Extensions Extensions { get; set; }

        public Context() {}

        public Context(StringOfJson json): this(json.ToJObject()) {}

        public Context(JObject jobj)
        {
            if (jobj["registration"] != null)
            {
                Registration = new Guid(jobj.Value<string>("registration"));
            }
            if (jobj["instructor"] != null)
            {
                if (jobj["instructor"]["objectType"] != null && (string)jobj["instructor"]["objectType"] == Group.OBJECT_TYPE)
                {
                    Instructor = (Group)jobj.Value<JObject>("instructor");
                }
                else
                {
                    Instructor = (Agent)jobj.Value<JObject>("instructor");
                }
            }
            if (jobj["team"] != null)
            {
                if (jobj["team"]["objectType"] != null && (string)jobj["team"]["objectType"] == Group.OBJECT_TYPE)
                {
                    Team = (Group)jobj.Value<JObject>("team");
                }
                else
                {
                    Team = (Agent)jobj.Value<JObject>("team");
                }
            }
            if (jobj["contextActivities"] != null)
            {
                ContextActivities = (ContextActivities)jobj.Value<JObject>("contextActivities");
            }
            if (jobj["revision"] != null)
            {
                Revision = jobj.Value<string>("revision");
            }
            if (jobj["platform"] != null)
            {
                Platform = jobj.Value<string>("platform");
            }
            if (jobj["language"] != null)
            {
                Language = jobj.Value<string>("language");
            }
            if (jobj["statement"] != null)
            {
                Statement = (StatementRef)jobj.Value<JObject>("statement");
            }
            if (jobj["extensions"] != null)
            {
                Extensions = (Extensions)jobj.Value<JObject>("extensions");
            }
        }

        public override JObject ToJObject(TCAPIVersion version) {
            var result = new JObject();

            if (Registration != null)
            {
                result.Add("registration", Registration.ToString());
            }
            if (Instructor != null)
            {
                result.Add("instructor", Instructor.ToJObject(version));
            }
            if (Team != null)
            {
                result.Add("team", Team.ToJObject(version));
            }
            if (ContextActivities != null)
            {
                result.Add("contextActivities", ContextActivities.ToJObject(version));
            }
            if (Revision != null)
            {
                result.Add("revision", Revision);
            }
            if (Platform != null)
            {
                result.Add("platform", Platform);
            }
            if (Language != null)
            {
                result.Add("language", Language);
            }
            if (Statement != null)
            {
                result.Add("statement", Statement.ToJObject(version));
            }
            if (Extensions != null)
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }

        public static explicit operator Context(JObject jobj)
        {
            return new Context(jobj);
        }
    }
}
