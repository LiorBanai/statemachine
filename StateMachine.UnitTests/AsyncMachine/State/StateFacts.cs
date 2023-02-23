 
// <copyright file="StateFacts.cs"  
 

using System;
using FakeItEasy;
using FluentAssertions;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.States;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.State
{
    public class StateFacts
    {
        [Fact]
        public void HierarchyWhenDefiningAStateAsItsOwnSuperStateThenAnExceptionIsThrown()
        {
            var testee = new StateDefinition<States, Events>(States.A);

            Action action = () => testee.SuperStateModifiable = testee;

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage(ExceptionMessages.StateCannotBeItsOwnSuperState(testee.ToString()));
        }

        [Fact]
        public void HierarchyWhenDefiningAStateAsItsOwnInitialSubStateThenAnExceptionIsThrown()
        {
            var testee = new StateDefinition<States, Events>(States.A);

            Action action = () => testee.InitialStateModifiable = testee;

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage(StatesExceptionMessages.StateCannotBeTheInitialSubStateToItself(testee.ToString()));
        }

        [Fact]
        public void HierarchyWhenDefiningAStateAAndAssigningAnInitialStateThatDoesntHaveStateAAsSuperStateThenAnExceptionIsThrown()
        {
            var testee = new StateDefinition<States, Events>(States.A);

            var initialState = A.Fake<StateDefinition<States, Events>>();
            initialState.SuperStateModifiable = A.Fake<StateDefinition<States, Events>>();

            Action action = () => testee.InitialStateModifiable = initialState;

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage(StatesExceptionMessages.StateCannotBeTheInitialStateOfSuperStateBecauseItIsNotADirectSubState(initialState.ToString(), testee.ToString()));
        }

        [Fact]
        public void HierarchyWhenSettingLevelThenTheLevelOfAllChildrenIsUpdated()
        {
            const int level = 2;
            var testee = new StateDefinition<States, Events>(States.A);
            var subState = A.Fake<StateDefinition<States, Events>>();
            testee.SubStatesModifiable.Add(subState);

            testee.Level = level;

            subState.Level
                .Should().Be(level + 1);
        }
    }
}