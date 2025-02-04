 
// <copyright file="ExceptionHandling.cs"  
 

using System;
using FluentAssertions;
using StateMachine.Machine;
using StateMachine.Machine.Events;
using Xbehave;

namespace StateMachine.Specs.Sync
{
    public class ExceptionHandling
    {
        private TransitionExceptionEventArgs<int, int> receivedTransitionExceptionEventArgs;

        [Scenario]
        public void TransitionActionException(PassiveStateMachine<int, int> machine)
        {
            "establish a transition action throwing an exception".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(Values.Source)
                        .On(Values.Event)
                        .Goto(Values.Destination)
                        .Execute(() => throw Values.Exception);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(Values.Source)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.TransitionExceptionThrown += (s, e) => this.receivedTransitionExceptionEventArgs = e;
            });

            "when executing the transition".x(() =>
            {
                machine.Start();
                machine.Fire(Values.Event, Values.Parameter);
            });

            this.ItShouldHandleTransitionException();
        }

        [Scenario]
        public void EntryActionException(PassiveStateMachine<int, int> machine)
        {
            "establish an entry action throwing an exception".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(Values.Source)
                        .On(Values.Event)
                        .Goto(Values.Destination);
                stateMachineDefinitionBuilder
                    .In(Values.Destination)
                        .ExecuteOnEntry(() => throw Values.Exception);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(Values.Source)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.TransitionExceptionThrown += (s, e) => this.receivedTransitionExceptionEventArgs = e;
            });

            "when executing the transition".x(() =>
            {
                machine.Start();
                machine.Fire(Values.Event, Values.Parameter);
            });

            this.ItShouldHandleTransitionException();
        }

        [Scenario]
        public void ExitActionException(PassiveStateMachine<int, int> machine)
        {
            "establish an exit action throwing an exception".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(Values.Source)
                        .ExecuteOnExit(() => throw Values.Exception)
                        .On(Values.Event)
                        .Goto(Values.Destination);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(Values.Source)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.TransitionExceptionThrown += (s, e) => this.receivedTransitionExceptionEventArgs = e;
            });

            "when executing the transition".x(() =>
            {
                machine.Start();
                machine.Fire(Values.Event, Values.Parameter);
            });

            this.ItShouldHandleTransitionException();
        }

        [Scenario]
        public void GuardException(PassiveStateMachine<int, int> machine)
        {
            "establish a guard throwing an exception".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(Values.Source)
                        .On(Values.Event)
                        .If(() => throw Values.Exception)
                        .Goto(Values.Destination);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(Values.Source)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.TransitionExceptionThrown += (s, e) => this.receivedTransitionExceptionEventArgs = e;
            });

            "when executing the transition".x(() =>
            {
                machine.Start();
                machine.Fire(Values.Event, Values.Parameter);
            });

            this.ItShouldHandleTransitionException();
        }

        [Scenario]
        public void StartingException(PassiveStateMachine<int, int> machine)
        {
            const int State = 1;

            "establish a entry action for the initial state that throws an exception".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(State)
                        .ExecuteOnEntry(() => throw Values.Exception);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(State)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.TransitionExceptionThrown += (s, e) => this.receivedTransitionExceptionEventArgs = e;
            });

            "when starting the state machine".x(() =>
                machine.Start());

            "should catch exception and fire transition exception event".x(() =>
                this.receivedTransitionExceptionEventArgs.Exception.Should().NotBeNull());

            "should pass thrown exception to event arguments of transition exception event".x(() =>
                this.receivedTransitionExceptionEventArgs.Exception.Should().BeSameAs(Values.Exception));
        }

        [Scenario]
        public void NoExceptionHandlerRegistered(
            PassiveStateMachine<int, int> machine,
            Exception catchedException)
        {
            "establish an exception throwing state machine without a registered exception handler".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(Values.Source)
                        .On(Values.Event)
                        .Execute(() => throw Values.Exception);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(Values.Source)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.Start();
            });

            "when an exception occurs".x(() =>
                catchedException = Catch.Exception(() => machine.Fire(Values.Event)));

            "should (re-)throw exception".x(() =>
                catchedException.InnerException
                    .Should().BeSameAs(Values.Exception));
        }

        private void ItShouldHandleTransitionException()
        {
            "should catch exception and fire transition exception event".x(() =>
                this.receivedTransitionExceptionEventArgs.Should().NotBeNull());

            "should pass source state of failing transition to event arguments of transition exception event".x(() =>
                this.receivedTransitionExceptionEventArgs.StateId.Should().Be(Values.Source));

            "should pass event id causing transition to event arguments of transition exception event".x(() =>
                this.receivedTransitionExceptionEventArgs.EventId.Should().Be(Values.Event));

            "should pass thrown exception to event arguments of transition exception event".x(() =>
                this.receivedTransitionExceptionEventArgs.Exception.Should().BeSameAs(Values.Exception));

            "should pass event parameter to event argument of transition exception event".x(() =>
                this.receivedTransitionExceptionEventArgs.EventArgument.Should().Be(Values.Parameter));
        }

        public static class Values
        {
            public const int Source = 1;
            public const int Destination = 2;
            public const int Event = 0;

            public const string Parameter = "oh oh";

            public static readonly Exception Exception = new Exception();
        }
    }
}