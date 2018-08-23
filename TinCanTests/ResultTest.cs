/*
    Copyright 2015 Rustici Software
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
    internal class ResultTest
    {
        [Test]
        public void TestEmptyCtr()
        {
            var obj = new Result();
            Assert.IsInstanceOf<Result>(obj);
            Assert.IsNull(obj.Completion);
            Assert.IsNull(obj.Success);
            Assert.IsNull(obj.Response);
            Assert.IsNull(obj.Duration);
            Assert.IsNull(obj.Score);
            Assert.IsNull(obj.Extensions);

            StringAssert.AreEqualIgnoringCase("{}", obj.ToJson());
        }

        [Test]
        public void TestJObjectCtr()
        {
            var cfg = new JObject
            {
                { "completion", true },
                { "success", true },
                { "response", "Yes" }
            };

            var obj = new Result(cfg);
            Assert.IsInstanceOf<Result>(obj);
            Assert.That(obj.Completion, Is.EqualTo(true));
            Assert.That(obj.Success, Is.EqualTo(true));
            Assert.That(obj.Response, Is.EqualTo("Yes"));
        }

        [Test]
        public void TestStringOfJsonCtr()
        {
            var json = "{\"success\": true, \"completion\": true, \"response\": \"Yes\"}";
            var strOfJson = new StringOfJson(json);

            var obj = new Result(strOfJson);
            Assert.IsInstanceOf<Result>(obj);
            Assert.That(obj.Success, Is.EqualTo(true));
            Assert.That(obj.Completion, Is.EqualTo(true));
            Assert.That(obj.Response, Is.EqualTo("Yes"));
        }
    }
}
