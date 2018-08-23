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
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    public class Extensions : JsonModel
    {
        private readonly Dictionary<Uri, JToken> _map;

        public Extensions()
        {
            _map = new Dictionary<Uri, JToken>();
        }

        public Extensions(JObject jobj) : this()
        {
            foreach (var item in jobj)
            {
                _map.Add(new Uri(item.Key), item.Value); 
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();
            foreach (var entry in _map)
            {
                result.Add(entry.Key.ToString(), entry.Value);
            }

            return result;
        }

        public bool IsEmpty()
        {
            return _map.Count <= 0;
        }

        public static explicit operator Extensions(JObject jobj)
        {
            return new Extensions(jobj);
        }
    }
}