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
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Json;


namespace TinCan
{
    public class ActivityDefinition : JsonModel
    {
        public Uri Type { get; set; }
        public Uri MoreInfo { get; set; }
        public LanguageMap Name { get; set; }
        public LanguageMap Description { get; set; }
        public Extensions Extensions { get; set; }
        public InteractionType InteractionType { get; set; }
        public List<string> CorrectResponsesPattern { get; set; }
        public List<InteractionComponent> Choices { get; set; }
        public List<InteractionComponent> Scale { get; set; }
        public List<InteractionComponent> Source { get; set; }
        public List<InteractionComponent> Target { get; set; }
        public List<InteractionComponent> Steps { get; set; }

        public ActivityDefinition() { }

        public ActivityDefinition(StringOfJson json) : this(json.ToJObject()) { }

        public ActivityDefinition(JObject jobj)
        {
            if (jobj["type"] != null)
            {
                Type = new Uri(jobj.Value<string>("type"));
            }
            if (jobj["moreInfo"] != null)
            {
                MoreInfo = new Uri(jobj.Value<string>("moreInfo"));
            }
            if (jobj["name"] != null)
            {
                Name = (LanguageMap)jobj.Value<JObject>("name");
            }
            if (jobj["description"] != null)
            {
                Description = (LanguageMap)jobj.Value<JObject>("description");
            }
            if (jobj["extensions"] != null)
            {
                Extensions = (Extensions)jobj.Value<JObject>("extensions");
            }
            if (jobj["interactionType"] != null)
            {
                InteractionType = InteractionType.FromValue(jobj.Value<string>("interactionType"));
            }
            if (jobj["correctResponsesPattern"] != null)
            {
                CorrectResponsesPattern = ((JArray)jobj["correctResponsesPattern"]).Select(x => x.Value<string>()).ToList();
            }
            if (jobj["choices"] != null)
            {
                Choices = new List<InteractionComponent>();
                foreach (JObject jchoice in jobj["choices"])
                {
                    Choices.Add(new InteractionComponent(jchoice));
                }
            }
            if (jobj["scale"] != null)
            {
                Scale = new List<InteractionComponent>();
                foreach (JObject jscale in jobj["scale"])
                {
                    Scale.Add(new InteractionComponent(jscale));
                }
            }
            if (jobj["source"] != null)
            {
                Source = new List<InteractionComponent>();
                foreach (JObject jsource in jobj["source"])
                {
                    Source.Add(new InteractionComponent(jsource));
                }
            }
            if (jobj["target"] != null)
            {
                Target = new List<InteractionComponent>();
                foreach (JObject jtarget in jobj["target"])
                {
                    Target.Add(new InteractionComponent(jtarget));
                }
            }
            if (jobj["steps"] != null)
            {
                Steps = new List<InteractionComponent>();
                foreach (JObject jstep in jobj["steps"])
                {
                    Steps.Add(new InteractionComponent(jstep));
                }
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Type != null)
            {
                result.Add("type", Type.ToString());
            }
            if (MoreInfo != null)
            {
                result.Add("moreInfo", MoreInfo.ToString());
            }
            if (Name != null && !Name.IsEmpty())
            {
                result.Add("name", Name.ToJObject(version));
            }
            if (Description != null && !Description.IsEmpty())
            {
                result.Add("description", Description.ToJObject(version));
            }
            if (Extensions != null && !Extensions.IsEmpty())
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }
            if (InteractionType != null)
            {
                result.Add("interactionType", InteractionType.Value);
            }
            if (CorrectResponsesPattern != null && CorrectResponsesPattern.Count > 0)
            {
                result.Add("correctResponsesPattern", JToken.FromObject(CorrectResponsesPattern));
            }
            if (Choices != null && Choices.Count > 0)
            {
                var jchoices = new JArray();
                result.Add("choices", jchoices);

                foreach (var ichoice in Choices)
                {
                    jchoices.Add(ichoice.ToJObject(version));
                }
            }
            if (Scale != null && Scale.Count > 0)
            {
                var jscale = new JArray();
                result.Add("scale", jscale);

                foreach (var iscale in Scale)
                {
                    jscale.Add(iscale.ToJObject(version));
                }
            }
            if (Source != null && Source.Count > 0)
            {
                var jsource = new JArray();
                result.Add("source", jsource);

                foreach (var isource in Source)
                {
                    jsource.Add(isource.ToJObject(version));
                }
            }
            if (Target != null && Target.Count > 0)
            {
                var jtarget = new JArray();
                result.Add("target", jtarget);

                foreach (var itarget in Target)
                {
                    jtarget.Add(itarget.ToJObject(version));
                }
            }
            if (Steps != null && Steps.Count > 0)
            {
                var jsteps = new JArray();
                result.Add("steps", jsteps);

                foreach (var istep in Steps)
                {
                    jsteps.Add(istep.ToJObject(version));
                }
            }

            return result;
        }

        public static explicit operator ActivityDefinition(JObject jobj)
        {
            return new ActivityDefinition(jobj);
        }
    }
}
