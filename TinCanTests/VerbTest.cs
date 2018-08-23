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
namespace TinCanTests
{
    using NUnit.Framework;
    using Newtonsoft.Json.Linq;
    using TinCan;
    using TinCan.Json;

    [TestFixture]
    internal class VerbTest
    {
        [Test]
        public void TestEmptyCtr()
        {
            var obj = new Verb();
            Assert.IsInstanceOf<Verb>(obj);
            Assert.IsNull(obj.Id);
            Assert.IsNull(obj.Display);

            StringAssert.AreEqualIgnoringCase("{}", obj.ToJson());
        }

        [Test]
        public void TestJObjectCtr()
        {
            var id = "http://adlnet.gov/expapi/verbs/experienced";

            var cfg = new JObject
            {
                { "id", id }
            };

            var obj = new Verb(cfg);
            Assert.IsInstanceOf<Verb>(obj);
            Assert.That(obj.ToJson(), Is.EqualTo("{\"id\":\"" + id + "\"}"));
        }

        [Test]
        public void TestStringOfJsonCtr()
        {
            var id = "http://adlnet.gov/expapi/verbs/experienced";
            var json = "{\"id\":\"" + id + "\"}";
            var strOfJson = new StringOfJson(json);

            var obj = new Verb(strOfJson);
            Assert.IsInstanceOf<Verb>(obj);
            Assert.That(obj.ToJson(), Is.EqualTo(json));
        }
    }
}
