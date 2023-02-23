 
// <copyright file="Initialization.cs"  
 

using FluentAssertions;
using StateMachine.AsyncMachine;
using Xbehave;

namespace StateMachine.Specs.Async
{
    public class Initialization
    {
        private const int TestState = 1;

        [Scenario]
        public void Start(
            AsyncPassiveStateMachine<int, int> machine,
            bool entryActionExecuted,
            CurrentStateExtension currentStateExtension)
        {
            "establish an initialized state machine".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(TestState)
                        .ExecuteOnEntry(() => entryActionExecuted = true);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(TestState)
                    .Build()
                    .CreatePassiveStateMachine();

                currentStateExtension = new CurrentStateExtension();
                machine.AddExtension(currentStateExtension);
            });

            "when starting the state machine".x(() =>
                machine.Start());

            "should set current state of state machine to state to which it is initialized".x(() =>
                currentStateExtension.CurrentState.Should().Be(TestState));

            "should execute entry action of state to which state machine is initialized".x(() =>
                entryActionExecuted.Should().BeTrue());
        }
    }
}