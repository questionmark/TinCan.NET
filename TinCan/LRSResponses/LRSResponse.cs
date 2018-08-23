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
using System;

namespace TinCan.LrsResponses
{
    //
    // this isn't abstract because some responses for an LRS won't have content
    // so in those cases we can get by just returning this base response
    //
    public class LrsResponse
    {
        public bool Success { get; set; }
        public Exception HttpException { get; set; }
        public string ErrMsg { get; set; }

        public void SetErrMsgFromBytes(byte[] content)
        {
            ErrMsg = System.Text.Encoding.UTF8.GetString(content);
        }
    }
}
