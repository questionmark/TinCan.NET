using System;
using System.Collections.Generic;

namespace TinCan
{
    public interface IRemoteLrs : ILrs
    {
        Uri Endpoint { get; set; }
        TCAPIVersion Version { get; set; }
        string Auth { get; set; }
        Dictionary<string, string> Extended { get; set; }
        Dictionary<string, string> Headers { get; set; }
        bool UseHttpClient { get; set; }
        string GetJsonStringFromStatements(List<Statement> statements);
        void SetAuth(string username, string password);
    }
}
