 
// <copyright file="PassiveStateMachines.cs"  
 

using FluentAssertions;
using StateMachine.Machine;
using Xbehave;

namespace StateMachine.Specs.Sync
{
    public class PassiveStateMachines
    {
        [Scenario]
        public void DefaultStateMachineName(
            PassiveStateMachine<string, int> machine,
            StateMachineNameReporter reporter)
        {
            "establish an instantiated passive state machine".x(() =>
                machine = new StateMachineDefinitionBuilder<string, int>()
                    .WithInitialState("initial")
                    .Build()
                    .CreatePassiveStateMachine());

            "establish a state machine reporter".x(() =>
            {
                reporter = new StateMachineNameReporter();
            });

            "when the state machine report is generated".x(() =>
                machine.Report(reporter));

            "it should use the type of the state machine as name for state machine".x(() =>
                reporter.StateMachineName
                    .Should().Be("StateMachine.PassiveStateMachine<System.String,System.Int32>"));
        }

        [Scenario]
        public void CustomStateMachineName(
            PassiveStateMachine<string, int> machine,
            StateMachineNameReporter reporter)
        {
            const string Name = "custom name";

            "establish an instantiated passive state machine with custom name".x(() =>
                machine = new StateMachineDefinitionBuilder<string, int>()
                    .WithInitialState("initial")
                    .Build()
                    .CreatePassiveStateMachine(Name));

            "establish a state machine reporter".x(() =>
            {
                reporter = new StateMachineNameReporter();
            });

            "when the state machine report is generated".x(() =>
                machine.Report(reporter));

            "it should use custom name for state machine".x(() =>
                reporter.StateMachineName
                    .Should().Be(Name));
        }

        [Scenario]
        public void EventsQueueing(
            IStateMachine<string, int> machine)
        {
            const int FirstEvent = 0;
            const int SecondEvent = 1;

            var arrived = false;

            "establish a passive state machine with transitions".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder.In("A").On(FirstEvent).Goto("B");
                stateMachineDefinitionBuilder.In("B").On(SecondEvent).Goto("C");
                stateMachineDefinitionBuilder.In("C").ExecuteOnEntry(() => arrived = true);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("A")
                    .Build()
                    .CreatePassiveStateMachine();
            });

            "when firing an event onto the state machine".x(() =>
            {
                machine.Fire(FirstEvent);
                machine.Fire(SecondEvent);
                machine.Start();
            });

            "it should queue event at the end".x(() =>
                arrived
                    .Should()
                    .BeTrue("state machine should arrive at destination state"));
        }

        [Scenario]
        public void PriorityEventsQueueing(
            IStateMachine<string, int> machine)
        {
            const int FirstEvent = 0;
            const int SecondEvent = 1;

            var arrived = false;

            "establish a passive state machine with transitions".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder.In("A").On(SecondEvent).Goto("B");
                stateMachineDefinitionBuilder.In("B").On(FirstEvent).Goto("C");
                stateMachineDefinitionBuilder.In("C").ExecuteOnEntry(() => arrived = true);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("A")
                    .Build()
                    .CreatePassiveStateMachine();
            });

            "when firing a priority event onto the state machine".x(() =>
            {
                machine.Fire(FirstEvent);
                machine.FirePriority(SecondEvent);
                machine.Start();
            });

            "it should queue event at the front".x(() =>
                arrived.Should().BeTrue("state machine should arrive at destination state"));
        }
    }
}