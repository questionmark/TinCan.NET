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
    public class Group : Agent
    {
        public List<Agent> Member { get; set; }

        public Group() : base() { }
        public Group(StringOfJson json) : this(json.ToJObject()) { }

        public Group(JObject jobj) : base(jobj)
        {
            if (jobj["member"] == null) return;
            Member = new List<Agent>();
            foreach (JObject jagent in jobj["member"])
            {
                Member.Add(new Agent(jagent));
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = base.ToJObject(version);
            if (Member != null && Member.Count > 0)
            {
                var jmember = new JArray();
                result.Add("member", jmember);

                foreach (var agent in Member)
                {
                    jmember.Add(agent.ToJObject(version));
                }
            }

            return result;
        }

        public static explicit operator Group(JObject jobj)
        {
            return new Group(jobj);
        }
    }
}
