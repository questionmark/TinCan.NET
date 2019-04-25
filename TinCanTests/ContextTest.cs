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
    using System;
    using TinCan;

    [TestFixture]
    class ContextTest
    {
        [TestCase(false)]
        [TestCase(true)]
        public void TestGroupInstructor(bool isGroup)
        {
            // Build our test JObject
            var exampleContext = BuildTextContext();
            exampleContext.Instructor = isGroup ? new Group() : new Agent();

            var contextObj = exampleContext.ToJObject();

            // Ensure that Context.instructor is the correct Type
            var testContext = new Context(contextObj);
            Assert.IsTrue(testContext.Instructor is Agent);
            Assert.AreEqual(isGroup, testContext.Instructor is Group);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestGroupTeam(bool isGroup)
        {
            // Build our test JObject
            var exampleContext = BuildTextContext();
            exampleContext.Team = isGroup ? new Group() : new Agent();

            var contextObj = exampleContext.ToJObject();

            // Ensure that Context.instructor is the correct Type
            var testContext = new Context(contextObj);
            Assert.IsTrue(testContext.Team is Agent);
            Assert.AreEqual(isGroup, testContext.Team is Group);
        }

        private Context BuildTextContext()
        {
            var registration = new Guid("42c0855b-8f64-47f3-b0e2-3f337930045a");
            var contextActivities = new ContextActivities();
            var revision = "";
            var platform = "";
            var language = "";
            var statement = new StatementRef();
            var extensions = new Extensions();

            var context = new Context
            {
                Registration = registration,
                ContextActivities = contextActivities,
                Revision = revision,
                Platform = platform,
                Language = language,
                Statement = statement,
                Extensions = extensions
            };

            return context;
        }
    }
}
