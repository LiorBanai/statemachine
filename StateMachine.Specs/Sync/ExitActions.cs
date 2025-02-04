// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitActions.cs"  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentAssertions;
using StateMachine.Machine;
using Xbehave;

namespace StateMachine.Specs.Sync
{
    public class ExitActions
    {
        private const int State = 1;
        private const int AnotherState = 2;
        private const int Event = 2;

        [Scenario]
        public void ExitAction(
            PassiveStateMachine<int, int> machine,
            bool exitActionExecuted)
        {
            "establish a state machine with exit action on a state".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(State)
                        .ExecuteOnExit(() => exitActionExecuted = true)
                        .On(Event).Goto(AnotherState);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(State)
                    .Build()
                    .CreatePassiveStateMachine();
            });

            "when leaving the state".x(() =>
            {
                machine.Start();
                machine.Fire(Event);
            });

            "it should execute the exit action".x(() =>
                exitActionExecuted.Should().BeTrue());
        }

        [Scenario]
        public void ExitActionWithParameter(
            PassiveStateMachine<int, int> machine,
            string parameter)
        {
            const string Parameter = "parameter";

            "establish a state machine with exit action with parameter on a state".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(State)
                        .ExecuteOnExitParametrized(p => parameter = p, Parameter)
                        .On(Event).Goto(AnotherState);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(State)
                    .Build()
                    .CreatePassiveStateMachine();
            });

            "when leaving the state".x(() =>
            {
                machine.Start();
                machine.Fire(Event);
            });

            "it should execute the exit action".x(() =>
                parameter.Should().NotBeNull());

            "it should pass parameter to the exit action".x(() =>
                parameter.Should().Be(Parameter));
        }

        [Scenario]
        public void MultipleExitActions(
            PassiveStateMachine<int, int> machine,
            bool exitAction1Executed,
            bool exitAction2Executed)
        {
            "establish a state machine with several exit actions on a state".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(State)
                        .ExecuteOnExit(() => exitAction1Executed = true)
                        .ExecuteOnExit(() => exitAction2Executed = true)
                        .On(Event).Goto(AnotherState);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(State)
                    .Build()
                    .CreatePassiveStateMachine();
            });

            "when leaving the state".x(() =>
            {
                machine.Start();
                machine.Fire(Event);
            });

            "It should execute all exit actions".x(() =>
            {
                exitAction1Executed
                    .Should().BeTrue("first action should be executed");

                exitAction2Executed
                    .Should().BeTrue("second action should be executed");
            });
        }

        [Scenario]
        public void ExceptionHandling(
            PassiveStateMachine<int, int> machine,
            bool exitAction1Executed,
            bool exitAction2Executed,
            bool exitAction3Executed)
        {
            var exception2 = new Exception();
            var exception3 = new Exception();
            var receivedExceptions = new List<Exception>();

            "establish a state machine with several exit actions on a state and some of them throw an exception".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(State)
                        .ExecuteOnExit(() => exitAction1Executed = true)
                        .ExecuteOnExit(() =>
                        {
                            exitAction2Executed = true;
                            throw exception2;
                        })
                        .ExecuteOnExit(() =>
                        {
                            exitAction3Executed = true;
                            throw exception3;
                        })
                        .On(Event).Goto(AnotherState);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(State)
                    .Build()
                    .CreatePassiveStateMachine();

                machine.TransitionExceptionThrown += (s, e) => receivedExceptions.Add(e.Exception);
            });

            "when entering the state".x(() =>
            {
                machine.Start();
                machine.Fire(Event);
            });

            "it should execute all entry actions on entry".x(() =>
            {
                exitAction1Executed
                    .Should().BeTrue("action 1 should be executed");

                exitAction2Executed
                    .Should().BeTrue("action 2 should be executed");

                exitAction3Executed
                    .Should().BeTrue("action 3 should be executed");
            });

            "it should handle all exceptions of all throwing entry actions by firing the TransitionExceptionThrown event".x(() =>
                receivedExceptions
                    .Should()
                    .HaveCount(2)
                    .And
                    .Contain(exception2)
                    .And
                    .Contain(exception3));
        }

        [Scenario]
        public void EventArgument(
            PassiveStateMachine<int, int> machine,
            int passedArgument)
        {
            const int Argument = 17;

            "establish a state machine with an exit action taking an event argument".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<int, int>();
                stateMachineDefinitionBuilder
                    .In(State)
                        .ExecuteOnExit((int argument) => passedArgument = argument)
                        .On(Event).Goto(AnotherState);
                stateMachineDefinitionBuilder
                    .In(AnotherState)
                        .ExecuteOnEntry((int argument) => passedArgument = argument);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(State)
                    .Build()
                    .CreatePassiveStateMachine();
            });

            "when leaving the state".x(() =>
            {
                machine.Start();
                machine.Fire(Event, Argument);
            });

            "it should pass event argument to exit action".x(() =>
                passedArgument.Should().Be(Argument));
        }
    }
}