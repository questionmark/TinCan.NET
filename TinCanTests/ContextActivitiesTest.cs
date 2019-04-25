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
    using System.Collections.Generic;
    using TinCan;

    [TestFixture]
    class ContextActivitiesTest
    {
        private Activity sampleActivity1 = new Activity
        {
            Id = "http://0.bar/"
        };

        private Activity sampleActivity2 = new Activity
        {
            Id = "http://1.bar/"
        };

        [Test]
        public void ConstructorWithObject()
        {
            var json = "{" +
                "\"parent\": " + sampleActivity1.ToJson() + "," +
                "\"grouping\": " + sampleActivity1.ToJson() + "," +
                "\"category\": " + sampleActivity1.ToJson() + "," +
                "\"other\": " + sampleActivity1.ToJson() +
            "}";

            ContextActivities contextActivities = new ContextActivities(new TinCan.Json.StringOfJson(json));

            ValidateActivityList(contextActivities.Parent, 1);
            ValidateActivityList(contextActivities.Grouping, 1);
            ValidateActivityList(contextActivities.Category, 1);
            ValidateActivityList(contextActivities.Other, 1);
        }

        [Test]
        public void ConstructorWithArray()
        {
            var json = "{" +
                "\"parent\": [" + sampleActivity1.ToJson() + ", " + sampleActivity2.ToJson() + "]," +
                "\"grouping\": [" + sampleActivity1.ToJson() + ", " + sampleActivity2.ToJson() + "]," +
                "\"category\": [" + sampleActivity1.ToJson() + ", " + sampleActivity2.ToJson() + "]," +
                "\"other\": [" + sampleActivity1.ToJson() + ", " + sampleActivity2.ToJson() + "]" +
            "}";

            ContextActivities contextActivities = new ContextActivities(new TinCan.Json.StringOfJson(json));

            ValidateActivityList(contextActivities.Parent, 2);
            ValidateActivityList(contextActivities.Grouping, 2);
            ValidateActivityList(contextActivities.Category, 2);
            ValidateActivityList(contextActivities.Other, 2);
        }

        private void ValidateActivityList(List<Activity> list, int expectedLength)
        {
            Assert.IsTrue(list.Count == expectedLength);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.IsTrue(list[i].Id == "http://" + i + ".bar/");
            }
        }
    }
}
