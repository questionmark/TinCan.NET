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
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    public class LanguageMap : JsonModel, IEnumerable
    {
        private readonly Dictionary<string, string> _map;

        public LanguageMap() {
            _map = new Dictionary<string, string>();
        }
        public LanguageMap(Dictionary<string, string> map)
        {
            _map = map;
        }

        public LanguageMap(StringOfJson json) : this(json.ToJObject()) { }
        public LanguageMap(JObject jobj) : this()
        {
            foreach (var entry in jobj) {
                _map.Add(entry.Key, (string)entry.Value);
            }
        }

        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();
            foreach (var entry in _map)
            {
                result.Add(entry.Key, entry.Value);
            }

            return result;
        }

        public bool IsEmpty()
        {
            return _map.Count <= 0;
        }

        public void Add(string lang, string value)
        {
            _map.Add(lang, value);
        }

        public static explicit operator LanguageMap(JObject jobj)
        {
            return new LanguageMap(jobj);
        }

        public override string ToString()
        {
            return string.Join(", ", _map.Values);
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
