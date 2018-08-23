/*
    Copyright 2014 Rustici Software
    Modifications copyright (C) 2018 Neal Daniel
    Sourced from: https://github.com/RusticiSoftware/TinCan.NET/pull/35

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

namespace TinCan
{
    public sealed class InteractionType
    {
        private const string Choice = "choice";
        private const string Sequencing = "sequencing";
        private const string Likert = "likert";
        private const string Matching = "matching";
        private const string Performance = "performance";
        private const string Truefalse = "true-false";
        private const string Fillin = "fill-in";
        private const string Longfillin = "long-fill-in";
        private const string Numeric = "numeric";
        private const string Other = "other";

        public static readonly InteractionType ChoiceType = new InteractionType(Choice);
        public static readonly InteractionType SequencingType = new InteractionType(Sequencing);
        public static readonly InteractionType LikertType = new InteractionType(Likert);
        public static readonly InteractionType MatchingType = new InteractionType(Matching);
        public static readonly InteractionType PerformanceType = new InteractionType(Performance);
        public static readonly InteractionType TrueFalseType = new InteractionType(Truefalse);
        public static readonly InteractionType FillInType = new InteractionType(Fillin);
        public static readonly InteractionType LongFillInType = new InteractionType(Longfillin);
        public static readonly InteractionType NumericType = new InteractionType(Numeric);
        public static readonly InteractionType OtherType = new InteractionType(Other);

        private InteractionType(string value)
        {
            Value = value;
        }

        public static InteractionType FromValue(string value)
        {
            switch (value)
            {
                case Choice:
                    return ChoiceType;

                case Sequencing:
                    return SequencingType;

                case Likert:
                    return LikertType;

                case Matching:
                    return MatchingType;

                case Performance:
                    return PerformanceType;

                case Truefalse:
                    return TrueFalseType;

                case Fillin:
                    return FillInType;
                    
                case Longfillin:
                    return LongFillInType;

                case Numeric:
                    return NumericType;

                case Other:
                    return OtherType;

                default:
                    throw new ArgumentOutOfRangeException(nameof(value),
                        $"'{value}' is not a valid interactionType.");
            }
        }

        public string Value { get; }
    }
}
