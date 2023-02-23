 
// <copyright file="AsyncActiveStateMachines.cs"  
 

using System.Threading;
using FluentAssertions;
using StateMachine.AsyncMachine;
using Xbehave;

namespace StateMachine.Specs.Async
{
    public class AsyncActiveStateMachines
    {
        [Scenario]
        public void DefaultStateMachineName(
            AsyncActiveStateMachine<string, int> machine,
            StateMachineNameReporter reporter)
        {
            "establish an instantiated active state machine".x(()
                => machine = new StateMachineDefinitionBuilder<string, int>()
                    .WithInitialState("initial")
                    .Build()
                    .CreateActiveStateMachine());

            "establish a state machine reporter".x(()
                => reporter = new StateMachineNameReporter());

            "when the state machine report is generated".x(()
                => machine.Report(reporter));

            "it should use the type of the state machine as name for state machine".x(()
                => reporter.StateMachineName
                    .Should().Be("StateMachine.AsyncActiveStateMachine<System.String,System.Int32>"));
        }

        [Scenario]
        public void CustomStateMachineName(
            AsyncActiveStateMachine<string, int> machine,
            StateMachineNameReporter reporter)
        {
            const string name = "custom name";

            "establish an instantiated active state machine with custom name".x(()
                => machine = new StateMachineDefinitionBuilder<string, int>()
                    .WithInitialState("initial")
                    .Build()
                    .CreateActiveStateMachine(name));

            "establish a state machine reporter".x(()
                => reporter = new StateMachineNameReporter());

            "when the state machine report is generated".x(()
                => machine.Report(reporter));

            "it should use custom name for state machine".x(()
                => reporter.StateMachineName
                    .Should().Be(name));
        }

        [Scenario]
        public void EventsQueueing(
            IAsyncStateMachine<string, int> machine,
            AutoResetEvent signal)
        {
            const int firstEvent = 0;
            const int secondEvent = 1;

            "establish an active state machine with transitions".x(() =>
            {
                signal = new AutoResetEvent(false);

                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder.In("A").On(firstEvent).Goto("B");
                stateMachineDefinitionBuilder.In("B").On(secondEvent).Goto("C");
                stateMachineDefinitionBuilder.In("C").ExecuteOnEntry(() => signal.Set());
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("A")
                    .Build()
                    .CreateActiveStateMachine();
            });

            "when firing an event onto the state machine".x(async () =>
            {
                await machine.Fire(firstEvent);
                await machine.Fire(secondEvent);
                await machine.Start();
            });

            "it should queue event at the end".x(() =>
                signal
                    .WaitOne(1000)
                    .Should()
                    .BeTrue("state machine should arrive at destination state"));
        }

        [Scenario]
        public void PriorityEventsQueueing(
            IAsyncStateMachine<string, int> machine,
            AutoResetEvent signal)
        {
            const int firstEvent = 0;
            const int secondEvent = 1;

            "establish an active state machine with transitions".x(() =>
            {
                signal = new AutoResetEvent(false);

                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder.In("A").On(secondEvent).Goto("B");
                stateMachineDefinitionBuilder.In("B").On(firstEvent).Goto("C");
                stateMachineDefinitionBuilder.In("C").ExecuteOnEntry(() => signal.Set());
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("A")
                    .Build()
                    .CreateActiveStateMachine();
            });

            "when firing a priority event onto the state machine".x(async () =>
            {
                await machine.Fire(firstEvent);
                await machine.FirePriority(secondEvent);
                await machine.Start();
            });

            "it should queue event at the front".x(() =>
                signal.WaitOne(1000).Should().BeTrue("state machine should arrive at destination state"));
        }

        [Scenario]
        public void PriorityEventsWhileExecutingNormalEvents(
            IAsyncStateMachine<string, int> machine,
            AutoResetEvent signal)
        {
            const int firstEvent = 0;
            const int secondEvent = 1;
            const int priorityEvent = 2;

            "establish an active state machine with transitions".x(() =>
            {
                signal = new AutoResetEvent(false);

                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder
                    .In("A")
                        .On(firstEvent)
                        .Goto("B")
                        .Execute(() => machine.FirePriority(priorityEvent));
                stateMachineDefinitionBuilder
                    .In("B")
                        .On(priorityEvent)
                        .Goto("C");
                stateMachineDefinitionBuilder
                    .In("C")
                    .On(secondEvent)
                    .Goto("D");
                stateMachineDefinitionBuilder
                    .In("D")
                    .ExecuteOnEntry(() => signal.Set());
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("A")
                    .Build()
                    .CreateActiveStateMachine();
            });

            "when firing a priority event onto the state machine".x(async () =>
            {
                await machine.Fire(firstEvent);
                await machine.Fire(secondEvent);
                await machine.Start();
            });

            "it should queue event at the front".x(()
                => signal.WaitOne(1000).Should().BeTrue("state machine should arrive at destination state"));
        }
    }
}