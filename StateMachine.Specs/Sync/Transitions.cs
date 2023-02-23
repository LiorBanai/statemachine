 
// <copyright file="Transitions.cs"  
 

using FluentAssertions;
using StateMachine.Machine;
using Xbehave;

namespace StateMachine.Specs.Sync
{
    public class Transitions
    {
        private const int SourceState = 1;
        private const int DestinationState = 2;
        private const int Event = 2;

        private const string Parameter = "parameter";

        private static readonly CurrentStateExtension CurrentStateExtension = new CurrentStateExtension();

        [Scenario]
        public void ExecutingTransition(
            PassiveStateMachine<int, int> machine,
            string actualParameter,
            bool exitActionExecuted,
            bool entryActionExecuted)
        {
            "establish a state machine with transitions".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(SourceState)
                        .ExecuteOnExit(() => exitActionExecuted = true)
                        .On(Event)
                        .Goto(DestinationState)
                        .Execute<string>(p => actualParameter = p);
                stateMachineDefinitionBuilder
                    .In(DestinationState)
                        .ExecuteOnEntry(() => entryActionExecuted = true);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(SourceState)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.AddExtension(CurrentStateExtension);

                machine.Start();
            });

            "when firing an event onto the state machine".x(() =>
                machine.Fire(Event, Parameter));

            "it should_execute_transition_by_switching_state".x(() =>
                 CurrentStateExtension.CurrentState.Should().Be(DestinationState));

            "it should_execute_transition_actions".x(() =>
                 actualParameter.Should().NotBeNull());

            "it should_pass_parameters_to_transition_action".x(() =>
                 actualParameter.Should().Be(Parameter));

            "it should_execute_exit_action_of_source_state".x(() =>
                 exitActionExecuted.Should().BeTrue());

            "it should_execute_entry_action_of_destination_state".x(() =>
                entryActionExecuted.Should().BeTrue());
        }
    }
}