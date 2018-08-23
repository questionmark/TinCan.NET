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
using System.Collections.Generic;
using System.Threading.Tasks;
using TinCan.Documents;
using TinCan.LrsResponses;

namespace TinCan
{
    public interface ILrs
    {
        Task<AboutLrsResponse> AboutAsync();

        Task<StatementLrsResponse> SaveStatementAsync(Statement statement);
        Task<StatementLrsResponse> VoidStatementAsync(Guid id, Agent agent);
        Task<StatementsResultLrsResponse> SaveStatementsAsync(List<Statement> statements);
        Task<StatementLrsResponse> RetrieveStatementAsync(Guid id);
        Task<StatementLrsResponse> RetrieveVoidedStatementAsync(Guid id);
        Task<StatementsResultLrsResponse> QueryStatementsAsync(StatementsQuery query);
        Task<StatementsResultLrsResponse> MoreStatementsAsync(StatementsResult result);

        Task<ProfileKeysLrsResponse> RetrieveStateIdsAsync(Activity activity, Agent agent, Guid? registration = null);
        Task<StateLrsResponse> RetrieveStateAsync(string id, Activity activity, Agent agent, Guid? registration = null);
        Task<LrsResponse> SaveStateAsync(StateDocument state);
        Task<LrsResponse> DeleteStateAsync(StateDocument state);
        Task<LrsResponse> ClearStateAsync(Activity activity, Agent agent, Guid? registration = null);

        Task<ProfileKeysLrsResponse> RetrieveActivityProfileIdsAsync(Activity activity);
        Task<ActivityProfileLrsResponse> RetrieveActivityProfileAsync(string id, Activity activity);
        Task<LrsResponse> SaveActivityProfileAsync(ActivityProfileDocument profile);
        Task<LrsResponse> DeleteActivityProfileAsync(ActivityProfileDocument profile);

        Task<ProfileKeysLrsResponse> RetrieveAgentProfileIdsAsync(Agent agent);
        Task<AgentProfileLrsResponse> RetrieveAgentProfileAsync(string id, Agent agent);
        Task<LrsResponse> SaveAgentProfileAsync(AgentProfileDocument profile);
        Task<LrsResponse> DeleteAgentProfileAsync(AgentProfileDocument profile);
    }
}
