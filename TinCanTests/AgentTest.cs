﻿/*
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
namespace TinCanTests
{
    using NUnit.Framework;
    using Newtonsoft.Json.Linq;
    using TinCan;
    using TinCan.Json;

    [TestFixture]
    internal class AgentTest
    {
        [Test]
        public void TestEmptyCtr()
        {
            var obj = new Agent();
            Assert.IsInstanceOf<Agent>(obj);
            Assert.IsNull(obj.Mbox);

            StringAssert.AreEqualIgnoringCase("{\"objectType\":\"Agent\"}", obj.ToJson());
        }

        [Test]
        public void TestJObjectCtr()
        {
            var mbox = "mailto:tincancsharp@tincanapi.com";

            var cfg = new JObject
            {
                { "mbox", mbox }
            };

            var obj = new Agent(cfg);
            Assert.IsInstanceOf<Agent>(obj);
            Assert.That(obj.Mbox, Is.EqualTo(mbox));
        }

        [Test]
        public void TestStringOfJsonCtr()
        {
            var mbox = "mailto:tincancsharp@tincanapi.com";

            var json = "{\"mbox\":\"" + mbox + "\"}";
            var strOfJson = new StringOfJson(json);

            var obj = new Agent(strOfJson);
            Assert.IsInstanceOf<Agent>(obj);
            Assert.That(obj.Mbox, Is.EqualTo(mbox));
        }
    }
}
