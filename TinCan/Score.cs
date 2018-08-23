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

using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    public class Score : JsonModel
    {
        public double? Scaled { get; set; }
        public double? Raw { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }

        public Score() {}

        public Score(StringOfJson json): this(json.ToJObject()) {}

        public Score(JObject jobj)
        {
            if (jobj["scaled"] != null)
            {
                Scaled = jobj.Value<double>("scaled");
            }
            if (jobj["raw"] != null)
            {
                Raw = jobj.Value<double>("raw");
            }
            if (jobj["min"] != null)
            {
                Min = jobj.Value<double>("min");
            }
            if (jobj["max"] != null)
            {
                Max = jobj.Value<double>("max");
            }
        }

        public override JObject ToJObject(TCAPIVersion version) {
            var result = new JObject();

            if (Scaled != null)
            {
                result.Add("scaled", Scaled);
            }
            if (Raw != null)
            {
                result.Add("raw", Raw);
            }
            if (Min != null)
            {
                result.Add("min", Min);
            }
            if (Max != null)
            {
                result.Add("max", Max);
            }

            return result;
        }

        public static explicit operator Score(JObject jobj)
        {
            return new Score(jobj);
        }
    }
}
