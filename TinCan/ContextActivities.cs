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
    public class ContextActivities : JsonModel
    {
        public List<Activity> Parent { get; set; }
        public List<Activity> Grouping { get; set; }
        public List<Activity> Category { get; set; }
        public List<Activity> Other { get; set; }

        public ContextActivities() { }

        public ContextActivities(StringOfJson json) : this(json.ToJObject()) { }

        public ContextActivities(JObject jobj)
        {
            if (jobj["parent"] != null)
            {
                Parent = new List<Activity>();
                if (jobj["parent"].Type == JTokenType.Array)
                {
                    foreach (JObject jactivity in jobj["parent"])
                    {
                        Parent.Add((Activity)jactivity);
                    }
                }
                else
                {
                    Parent.Add((Activity)jobj["parent"]);
                }
            }
            if (jobj["grouping"] != null)
            {
                Grouping = new List<Activity>();
                if (jobj["grouping"].Type == JTokenType.Array)
                {
                    foreach (JObject jactivity in jobj["grouping"])
                    {
                        Grouping.Add((Activity)jactivity);
                    }
                }
                else
                {
                    Grouping.Add((Activity)jobj["grouping"]);
                }
            }
            if (jobj["category"] != null)
            {
                Category = new List<Activity>();
                if (jobj["category"].Type == JTokenType.Array)
                {
                    foreach (JObject jactivity in jobj["category"])
                    {
                        Category.Add((Activity)jactivity);
                    }
                }
                else
                {
                    Category.Add((Activity)jobj["category"]);
                }
            }
            if (jobj["other"] != null)
            {
                Other = new List<Activity>();
                if (jobj["other"].Type == JTokenType.Array)
                {
                    foreach (JObject jactivity in jobj["other"])
                    {
                        Other.Add((Activity)jactivity);
                    }
                }
                else
                {
                    Other.Add((Activity)jobj["other"]);
                }
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Parent != null && Parent.Count > 0)
            {
                var jparent = new JArray();
                result.Add("parent", jparent);

                foreach (var activity in Parent)
                {
                    jparent.Add(activity.ToJObject(version));
                }
            }
            if (Grouping != null && Grouping.Count > 0)
            {
                var jgrouping = new JArray();
                result.Add("grouping", jgrouping);

                foreach (var activity in Grouping)
                {
                    jgrouping.Add(activity.ToJObject(version));
                }
            }
            if (Category != null && Category.Count > 0)
            {
                var jcategory = new JArray();
                result.Add("category", jcategory);

                foreach (var activity in Category)
                {
                    jcategory.Add(activity.ToJObject(version));
                }
            }
            if (Other != null && Other.Count > 0)
            {
                var jother = new JArray();
                result.Add("other", jother);

                foreach (var activity in Other)
                {
                    jother.Add(activity.ToJObject(version));
                }
            }

            return result;
        }

        public static explicit operator ContextActivities(JObject jobj)
        {
            return new ContextActivities(jobj);
        }
    }
}
