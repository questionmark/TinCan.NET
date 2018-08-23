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
using System.Xml;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    public class Result : JsonModel
    {
        public bool? Completion { get; set; }
        public bool? Success { get; set; }
        public string Response { get; set; }
        public TimeSpan? Duration { get; set; }
        public Score Score { get; set; }
        public Extensions Extensions { get; set; }

        public Result() {}

        public Result(StringOfJson json): this(json.ToJObject()) {}

        public Result(JObject jobj)
        {
            if (jobj["completion"] != null)
            {
                Completion = jobj.Value<bool>("completion");
            }
            if (jobj["success"] != null)
            {
                Success = jobj.Value<bool>("success");
            }
            if (jobj["response"] != null)
            {
                Response = jobj.Value<string>("response");
            }
            if (jobj["duration"] != null)
            {
                Duration = XmlConvert.ToTimeSpan(jobj.Value<string>("duration"));
            }
            if (jobj["score"] != null)
            {
                Score = (Score)jobj.Value<JObject>("score");
            }
            if (jobj["extensions"] != null)
            {
                Extensions = (Extensions)jobj.Value<JObject>("extensions");
            }
        }

        public override JObject ToJObject(TCAPIVersion version) {
            var result = new JObject();

            if (Completion != null)
            {
                result.Add("completion", Completion);
            }
            if (Success != null)
            {
                result.Add("success", Success);
            }
            if (Response != null)
            {
                result.Add("response", Response);
            }
            if (Duration != null)
            {
                result.Add("duration", XmlConvert.ToString((TimeSpan)Duration));
            }
            if (Score != null)
            {
                result.Add("score", Score.ToJObject(version));
            }
            if (Extensions != null)
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }

        public static explicit operator Result(JObject jobj)
        {
            return new Result(jobj);
        }
    }
}
