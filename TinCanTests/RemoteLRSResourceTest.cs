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

using System.Threading.Tasks;

namespace TinCanTests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using TinCan;
    using TinCan.Documents;

    [TestFixture]
    internal class RemoteLrsResourceTest
    {
        private RemoteLrs _lrs;

        [SetUp]
        public void Init()
        {
            Console.WriteLine("Running " + TestContext.CurrentContext.Test.FullName);

            //
            // these are credentials used by the other OSS libs when building via Travis-CI
            // so are okay to include in the repository, if you wish to have access to the
            // results of the test suite then supply your own Endpoint, username, and password
            //
            _lrs = new RemoteLrs(
                "https://cloud.scorm.com/tc/U2S4SI5FY0/sandbox/",
                "Nja986GYE1_XrWMmFUE",
                "Bd9lDr1kjaWWY6RID_4"
            );
        }

        [Test]
        public async Task TestAboutAsync()
        {
            var lrsRes = await _lrs.AboutAsync();
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestAboutFailureAsync()
        {
            _lrs.Endpoint = new Uri("http://cloud.scorm.com/tc/3TQLAI9/sandbox/");

            var lrsRes = await _lrs.AboutAsync();
            Assert.IsFalse(lrsRes.Success);
            Console.WriteLine("TestAboutFailure - errMsg: " + lrsRes.ErrMsg);
        }

        [Test]
        public async Task TestSaveStatementAsync()
        {
            var statement = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity
            };

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
            Assert.IsNotNull(lrsRes.Content.Id);
        }

        [Test]
        public async Task TestSaveStatementWithIdAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        [Test]
        public async Task TestSaveStatementStatementRefAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.StatementRef;

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        [Test]
        public async Task TestSaveStatementSubStatementAsync()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.SubStatement;

            Console.WriteLine(statement.ToJson(true));

            var lrsRes = await _lrs.SaveStatementAsync(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        [Test]
        public async Task TestVoidStatementAsync()
        {
            var toVoid = Guid.NewGuid();
            var lrsRes = await _lrs.VoidStatementAsync(toVoid, Support.Agent);

            Assert.IsTrue(lrsRes.Success, "LRS response successful");
            Assert.AreEqual(new Uri("http://adlnet.gov/expapi/verbs/voided"), lrsRes.Content.Verb.Id, "voiding statement uses voided verb");
            Assert.AreEqual(toVoid, ((StatementRef)lrsRes.Content.Target).Id, "voiding statement target correct id");
        }

        [Test]
        public async Task TestSaveStatementsAsync()
        {
            var statement1 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Parent
            };

            var statement2 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity,
                Context = Support.Context
            };

            var statements = new List<Statement>
            {
                statement1,
                statement2
            };

            var lrsRes = await _lrs.SaveStatementsAsync(statements);
            Assert.IsTrue(lrsRes.Success);
            // TODO: check statements match and ids not null
        }

        [Test]
        public async Task TestRetrieveStatementAsync()
        {
            var statement = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity,
                Context = Support.Context,
                Result = Support.Result
            };

            statement.Stamp();

            var saveRes = await _lrs.SaveStatementAsync(statement);
            if (saveRes.Success)
            {
                if (saveRes.Content.Id != null)
                {
                    var retRes = await _lrs.RetrieveStatementAsync(saveRes.Content.Id.Value);
                    Assert.IsTrue(retRes.Success);
                    Console.WriteLine("TestRetrieveStatement - statement: " + retRes.Content.ToJson(true));
                }
            }
            else
            {
                // TODO: skipped?
            }
        }

        [Test]
        public async Task TestQueryStatementsAsync()
        {
            var query = new StatementsQuery
            {
                Agent = Support.Agent,
                VerbId = Support.Verb.Id,
                ActivityId = Support.Parent.Id,
                RelatedActivities = true,
                RelatedAgents = true,
                Format = StatementsQueryResultFormat.Ids,
                Limit = 10
            };

            var lrsRes = await _lrs.QueryStatementsAsync(query);
            Assert.IsTrue(lrsRes.Success);
            Console.WriteLine("TestQueryStatements - statement count: " + lrsRes.Content.Statements.Count);
        }

        [Test]
        public async Task TestMoreStatementsAsync()
        {
            var query = new StatementsQuery
            {
                Format = StatementsQueryResultFormat.Ids,
                Limit = 2
            };

            var queryRes = await _lrs.QueryStatementsAsync(query);
            if (queryRes.Success && queryRes.Content.More != null)
            {
                var moreRes = await _lrs.MoreStatementsAsync(queryRes.Content);
                Assert.IsTrue(moreRes.Success);
                Console.WriteLine("TestMoreStatements - statement count: " + moreRes.Content.Statements.Count);
            }
            else
            {
                // TODO: skipped?
            }
        }

        [Test]
        public async Task TestRetrieveStateIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveStateIdsAsync(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveStateAsync()
        {
            var lrsRes = await _lrs.RetrieveStateAsync("test", Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<StateDocument>(lrsRes.Content);
        }

        [Test]
        public async Task TestSaveStateAsync()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                Id = "test",
                Content = System.Text.Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveStateAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestDeleteStateAsync()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                Id = "test"
            };

            var lrsRes = await _lrs.DeleteStateAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestClearStateAsync()
        {
            var lrsRes = await _lrs.ClearStateAsync(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveActivityProfileIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveActivityProfileIdsAsync(Support.Activity);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveActivityProfileAsync()
        {
            var lrsRes = await _lrs.RetrieveActivityProfileAsync("test", Support.Activity);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<ActivityProfileDocument>(lrsRes.Content);
        }

        [Test]
        public async Task TestSaveActivityProfileAsync()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                Id = "test",
                Content = System.Text.Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveActivityProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestDeleteActivityProfileAsync()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                Id = "test"
            };

            var lrsRes = await _lrs.DeleteActivityProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveAgentProfileIdsAsync()
        {
            var lrsRes = await _lrs.RetrieveAgentProfileIdsAsync(Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveAgentProfileAsync()
        {
            var lrsRes = await _lrs.RetrieveAgentProfileAsync("test", Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<AgentProfileDocument>(lrsRes.Content);
        }

        [Test]
        public async Task TestSaveAgentProfileAsync()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                Id = "test",
                Content = System.Text.Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveAgentProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestDeleteAgentProfileAsync()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                Id = "test"
            };

            var lrsRes = await _lrs.DeleteAgentProfileAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestExtendedParameters()
        {
            // RemoteLRS doesn't provide a helpful interface for testing
            // that we successfully altered the request URL, but this test
            // is helpful in manual testing and it at least ensures that
            // specifying values in extended doesn't cause errors.
            _lrs.Extended.Add("test", "param");
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                Id = "test",
                Content = System.Text.Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await _lrs.SaveStateAsync(doc);
            Assert.IsTrue(lrsRes.Success);
        }
    }
}