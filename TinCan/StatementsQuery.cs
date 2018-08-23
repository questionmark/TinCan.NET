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
using System.Collections.Generic;

namespace TinCan
{
    public class StatementsQuery
    {
        // TODO: put in common location
        private const string IsoDateTimeFormat = "o";

        public Agent Agent { get; set; }
        public Uri VerbId { get; set; }
        private string _activityId;
        public string ActivityId
        {
            get => _activityId;
            set
            {
                var uri = new Uri(value);
                _activityId = value;
            }
        }

        public Guid? Registration { get; set; }
        public bool? RelatedActivities { get; set; }
        public bool? RelatedAgents { get; set; }
        public DateTime? Since { get; set; }
        public DateTime? Until { get; set; }
        public int? Limit { get; set; }
        public StatementsQueryResultFormat Format { get; set; }
        public bool? Ascending { get; set; }

        public StatementsQuery() {}

        public Dictionary<string, string> ToParameterMap (TCAPIVersion version)
        {
            var result = new Dictionary<string, string>();

            if (Agent != null)
            {
                result.Add("agent", Agent.ToJson(version));
            }
            if (VerbId != null)
            {
                result.Add("verb", VerbId.ToString());
            }
            if (ActivityId != null)
            {
                result.Add("activity", ActivityId);
            }
            if (Registration != null)
            {
                result.Add("registration", Registration.Value.ToString());
            }
            if (RelatedActivities != null)
            {
                result.Add("related_activities", RelatedActivities.Value.ToString());
            }
            if (RelatedAgents != null)
            {
                result.Add("related_agents", RelatedAgents.Value.ToString());
            }
            if (Since != null)
            {
                result.Add("since", Since.Value.ToString(IsoDateTimeFormat));
            }
            if (Until != null)
            {
                result.Add("until", Until.Value.ToString(IsoDateTimeFormat));
            }
            if (Limit != null)
            {
                result.Add("limit", Limit.ToString());
            }
            if (Format != null)
            {
                result.Add("format", Format.ToString());
            }
            if (Ascending != null)
            {
                result.Add("ascending", Ascending.Value.ToString());
            }

            return result;
        }
    }
}
