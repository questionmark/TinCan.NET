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
    internal class SubStatementTest
    {
        [SetUp]
        public void Init()
        {
            Console.WriteLine("Running " + TestContext.CurrentContext.Test.FullName);
        }

        [Test]
        public void TestEmptyCtr()
        {
            var obj = new SubStatement();
            Assert.IsInstanceOf<SubStatement>(obj);
            Assert.IsNull(obj.Actor);
            Assert.IsNull(obj.Verb);
            Assert.IsNull(obj.Target);
            Assert.IsNull(obj.Result);
            Assert.IsNull(obj.Context);

            StringAssert.AreEqualIgnoringCase("{\"objectType\":\"SubStatement\"}", obj.ToJson());
        }

        [Test]
        public void TestJObjectCtrNestedSubStatement()
        {
            var cfg = new JObject
            {
                { "actor", Support.Agent.ToJObject() },
                { "verb", Support.Verb.ToJObject() },
                { "object", Support.SubStatement.ToJObject() }
            };

            var obj = new SubStatement(cfg);
            Assert.IsInstanceOf<SubStatement>(obj);
            Assert.IsNull(obj.Target);
        }
    }
}
