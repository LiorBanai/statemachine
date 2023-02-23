 
// <copyright file="StateTest.cs"  
 

using System;
using FakeItEasy;
using FluentAssertions;
using StateMachine.Machine.States;
using Xunit;

namespace StateMachine.UnitTests.Machine.State
{
    public class StateTest
    {
        [Fact]
        public void HierarchyWhenDefiningAStateAsItsOwnSuperStateThenAnExceptionIsThrown()
        {
            var testee = new StateDefinition<States, Events>(States.A);

            Action action = () => testee.SuperStateModifiable = testee;

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage(StatesExceptionMessages.StateCannotBeItsOwnSuperState(testee.ToString()));
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
            const int Level = 2;
            var testee = new StateDefinition<States, Events>(States.A);
            var subState = A.Fake<StateDefinition<States, Events>>();
            testee.SubStatesModifiable.Add(subState);

            testee.Level = Level;

            subState.Level
                .Should().Be(Level + 1);
        }
    }
}