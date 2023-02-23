 
// <copyright file="Reporting.cs"  
 

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