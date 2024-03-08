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
    using System.Collections.Generic;
    using TinCan;

    internal static class Support
    {
        public static Agent Agent;
        public static Verb Verb;
        public static Activity Activity;
        public static Activity Parent;
        public static Context Context;
        public static Result Result;
        public static Score Score;
        public static StatementRef StatementRef;
        public static SubStatement SubStatement;

        static Support()
        {
            Agent = new Agent
            {
                Mbox = "mailto:tincancsharp@tincanapi.com"
            };

            Verb = new Verb("http://adlnet.gov/expapi/verbs/experienced")
            {
                Display = new LanguageMap()
            };
            Verb.Display.Add("en-US", "experienced");

            Activity = new Activity
            {
                Id = "http://tincanapi.com/TinCanCSharp/Test/Unit/0",
                Definition = new ActivityDefinition
                {
                    Type = new Uri("http://id.tincanapi.com/activitytype/unit-test"),
                    Name = new LanguageMap()
                }
            };
            Activity.Definition.Name.Add("en-US", "Tin Can C# Tests: Unit 0");
            Activity.Definition.Description =
                new LanguageMap { { "en-US", "Unit test 0 in the test suite for the Tin Can C# library." } };

            Activity.Definition.InteractionType = InteractionType.ChoiceType;
            Activity.Definition.Choices = new List<InteractionComponent>();

            for (int i = 1; i <= 3; i++)
            {
                var interactionComponent = new InteractionComponent
                {
                    Id = "choice-" + i.ToString(),
                    Description = new LanguageMap()
                };

                interactionComponent.Description.Add("en-US", "Choice " + i.ToString());

                Activity.Definition.Choices.Add(interactionComponent);
            }

            Activity.Definition.CorrectResponsesPattern = new List<string>();

            for (int i = 1; i <= 2; i++)
            {
                Activity.Definition.CorrectResponsesPattern.Add("choice-" + i.ToString());
            }

            Parent = new Activity
            {
                Id = "http://tincanapi.com/TinCanCSharp/Test",
                Definition = new ActivityDefinition
                {
                    Type = new Uri("http://id.tincanapi.com/activitytype/unit-test-suite"),
                    Name = new LanguageMap()
                }
            };
            Parent.Definition.Name.Add("en-US", "Tin Can C# Tests");
            Parent.Definition.Description = new LanguageMap { { "en-US", "Unit test suite for the Tin Can C# library." } };

            StatementRef = new StatementRef(Guid.NewGuid());

            Context = new Context
            {
                Registration = Guid.NewGuid(),
                Statement = StatementRef,
                ContextActivities = new ContextActivities
                {
                    Parent = new List<Activity>()
                }
            };
            Context.ContextActivities.Parent.Add(Parent);

            Score = new Score
            {
                Raw = 97,
                Scaled = 0.97,
                Max = 100,
                Min = 0
            };

            Result = new Result
            {
                Score = Score,
                Success = true,
                Completion = true,
                Duration = new TimeSpan(1, 2, 16, 43),
                Response = "choice-2"
            };

            SubStatement = new SubStatement
            {
                Actor = Agent,
                Verb = Verb,
                Target = Parent
            };
        }
    }
}