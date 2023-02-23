 
// <copyright file="ActiveStateMachines.cs"  
 

using System.Threading;
using FluentAssertions;
using StateMachine.Machine;
using Xbehave;

namespace StateMachine.Specs.Sync
{
    public class ActiveStateMachines
    {
        [Scenario]
        public void DefaultStateMachineName(
            ActiveStateMachine<string, int> machine,
            StateMachineNameReporter reporter)
        {
            "establish an instantiated active state machine".x(() =>
                machine = new StateMachineDefinitionBuilder<string, int>()
                    .WithInitialState("initial")
                    .Build()
                    .CreateActiveStateMachine());

            "establish a state machine reporter".x(() =>
            {
                reporter = new StateMachineNameReporter();
            });

            "when the state machine report is generated".x(() =>
                machine.Report(reporter));

            "it should use the type of the state machine as name for state machine".x(() =>
                reporter.StateMachineName
                    .Should().Be("StateMachine.ActiveStateMachine<System.String,System.Int32>"));
        }

        [Scenario]
        public void CustomStateMachineName(
            ActiveStateMachine<string, int> machine,
            StateMachineNameReporter reporter)
        {
            const string Name = "custom name";

            "establish an instantiated active state machine with custom name".x(() =>
                machine = new StateMachineDefinitionBuilder<string, int>()
                    .WithInitialState("initial")
                    .Build()
                    .CreateActiveStateMachine(Name));

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
            IStateMachine<string, int> machine,
            AutoResetEvent signal)
        {
            const int FirstEvent = 0;
            const int SecondEvent = 1;

            "establish an active state machine with transitions".x(() =>
            {
                signal = new AutoResetEvent(false);

                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder.In("A").On(FirstEvent).Goto("B");
                stateMachineDefinitionBuilder.In("B").On(SecondEvent).Goto("C");
                stateMachineDefinitionBuilder.In("C").ExecuteOnEntry(() => signal.Set());
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("A")
                    .Build()
                    .CreateActiveStateMachine();
            });

            "when firing an event onto the state machine".x(() =>
            {
                machine.Fire(FirstEvent);
                machine.Fire(SecondEvent);
                machine.Start();
            });

            "it should queue event at the end".x(() =>
                signal
                    .WaitOne(1000)
                    .Should()
                    .BeTrue("state machine should arrive at destination state"));
        }

        [Scenario]
        public void PriorityEventsQueueing(
            IStateMachine<string, int> machine,
            AutoResetEvent signal)
        {
            const int FirstEvent = 0;
            const int SecondEvent = 1;

            "establish an active state machine with transitions".x(() =>
            {
                signal = new AutoResetEvent(false);

                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder.In("A").On(SecondEvent).Goto("B");
                stateMachineDefinitionBuilder.In("B").On(FirstEvent).Goto("C");
                stateMachineDefinitionBuilder.In("C").ExecuteOnEntry(() => signal.Set());
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("A")
                    .Build()
                    .CreateActiveStateMachine();
            });

            "when firing a priority event onto the state machine".x(() =>
            {
                machine.Fire(FirstEvent);
                machine.FirePriority(SecondEvent);
                machine.Start();
            });

            "it should queue event at the front".x(() =>
                signal
                    .WaitOne(1000)
                    .Should()
                    .BeTrue("state machine should arrive at destination state"));
        }
    }
}