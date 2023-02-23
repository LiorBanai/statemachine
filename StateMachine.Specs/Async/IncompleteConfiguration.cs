 
// <copyright file="IncompleteConfiguration.cs"  
 

using System;
using FluentAssertions;
using StateMachine.AsyncMachine;
using Xbehave;

namespace StateMachine.Specs.Async
{
    public class IncompleteConfiguration
    {
        [Scenario]
        public void TransitionActionException(
            AsyncPassiveStateMachine<int, int> machine,
            Exception receivedException,
            int state = 0)
        {
            "establish a StateDefinition without configurations".x(() =>
            {
                machine = new StateMachineDefinitionBuilder<int, int>()
                    .WithInitialState(state)
                    .Build()
                    .CreatePassiveStateMachine();
            });

            "when the state machine is started".x(async () =>
                receivedException = await Catch.Exception(async () => await machine.Start()));

            "it should throw an exception, indicating the missing configuration".x(() =>
                receivedException
                    .Message
                    .Should()
                    .Be($"Cannot find StateDefinition for state {state}. Are you sure you have configured this state via myStateDefinitionBuilder.In(..) or myStateDefinitionBuilder.DefineHierarchyOn(..)?"));
        }

        [Scenario]
        public void BuildingAStateMachineWithoutInitialStateThenInvalidOperationException()
        {
            var testee = new StateMachineDefinitionBuilder<int, int>();

            Action action = () => testee.Build();

            action
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Initial state is not configured.");
        }
    }
}