//-------------------------------------------------------------------------------
// <copyright file="Reporting.cs" company="Appccelerate">
//   Copyright (c) 2008-2019 Appccelerate
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

using System.Collections.Generic;
using FakeItEasy;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.States;
using Xbehave;

namespace StateMachine.Specs.Async
{
    public class Reporting
    {
        [Scenario]
        public void Report(
            IAsyncStateMachine<string, int> machine,
            IStateMachineReport<string, int> report)
        {
            "establish a state machine".x(()
                => machine = new StateMachineDefinitionBuilder<string, int>()
                    .WithInitialState("initial")
                    .Build()
                    .CreatePassiveStateMachine());

            "establish a state machine reporter".x(()
                => report = A.Fake<IStateMachineReport<string, int>>());

            "when creating a report".x(()
                => machine.Report(report));

            "it should call the passed reporter".x(()
                => A.CallTo(() =>
                        report.Report(A<string>._, A<IEnumerable<IStateDefinition<string, int>>>._, A<string>._))
                    .MustHaveHappened());
        }
    }
}