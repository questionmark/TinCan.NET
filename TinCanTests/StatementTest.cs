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
    using System;
    using NUnit.Framework;
    using Newtonsoft.Json.Linq;
    using TinCan;

    [TestFixture]
    internal class StatementTest
    {
        [SetUp]
        public void Init()
        {
            Console.WriteLine("Running " + TestContext.CurrentContext.Test.FullName);
        }

        [Test]
        public void TestEmptyCtr()
        {
            var obj = new Statement();
            Assert.IsInstanceOf<Statement>(obj);
            Assert.IsNull(obj.Id);
            Assert.IsNull(obj.Actor);
            Assert.IsNull(obj.Verb);
            Assert.IsNull(obj.Target);
            Assert.IsNull(obj.Result);
            Assert.IsNull(obj.Context);
            Assert.IsNull(obj.Version);
            Assert.IsNull(obj.Timestamp);
            Assert.IsNull(obj.Stored);

            StringAssert.AreEqualIgnoringCase("{\"version\":\"1.0.3\"}", obj.ToJson());
        }

        [Test]
        public void TestJObjectCtrSubStatement()
        {
            var cfg = new JObject
            {
                { "actor", Support.Agent.ToJObject() },
                { "verb", Support.Verb.ToJObject() },
                { "object", Support.SubStatement.ToJObject() }
            };

            var obj = new Statement(cfg);
            Assert.IsInstanceOf<Statement>(obj);
            Assert.IsInstanceOf<SubStatement>(obj.Target);
        }
    }
}
