 
// <copyright file="Guards.cs"  
 

using FluentAssertions;
using StateMachine.Machine;
using Xbehave;

namespace StateMachine.Specs.Sync
{
    public class Guards
    {
        private const int SourceState = 1;
        private const int DestinationState = 2;
        private const int ErrorState = 3;

        private const int Event = 2;

        [Scenario]
        public void MatchingGuard(
            PassiveStateMachine<int, int> machine,
            CurrentStateExtension currentStateExtension)
        {
            "establish a state machine with guarded transitions".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(SourceState)
                        .On(Event)
                        .If(() => false).Goto(ErrorState)
                        .If(() => true).Goto(DestinationState)
                        .If(() => true).Goto(ErrorState)
                        .Otherwise().Goto(ErrorState);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(SourceState)
                    .Build()
                    .CreatePassiveStateMachine();

                currentStateExtension = new CurrentStateExtension();
                machine.AddExtension(currentStateExtension);

                machine.Start();
            });

            "when an event is fired".x(() =>
                machine.Fire(Event));

            "it should take transition guarded with first matching guard".x(() =>
                currentStateExtension.CurrentState.Should().Be(DestinationState));
        }

        [Scenario]
        public void OtherwiseGuard(
            PassiveStateMachine<int, int> machine,
            CurrentStateExtension currentStateExtension)
        {
            "establish a state machine with otherwise guard and no matching other guard".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(SourceState)
                        .On(Event)
                        .If(() => false).Goto(ErrorState)
                        .Otherwise().Goto(DestinationState);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(SourceState)
                    .Build()
                    .CreatePassiveStateMachine();

                currentStateExtension = new CurrentStateExtension();
                machine.AddExtension(currentStateExtension);

                machine.Start();
            });

            "when an event is fired".x(() =>
                machine.Fire(Event));

            "it should_take_transition_guarded_with_otherwise".x(() =>
                currentStateExtension.CurrentState.Should().Be(DestinationState));
        }

        [Scenario]
        public void NoMatchingGuard(
            PassiveStateMachine<int, int> machine)
        {
            var declined = false;

            "establish state machine with no matching guard".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(SourceState)
                        .On(Event)
                        .If(() => false).Goto(ErrorState);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(SourceState)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.TransitionDeclined += (sender, e) => declined = true;

                machine.Start();
            });

            "when an event is fired".x(() =>
                machine.Fire(Event));

            "it should notify about declined transition".x(() =>
                declined.Should().BeTrue("TransitionDeclined event should be fired"));
        }
    }
}