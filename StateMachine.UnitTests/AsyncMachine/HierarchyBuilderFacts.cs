

using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.States;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine
{
    public class HierarchyBuilderFacts
    {
        private const string SuperState = "SuperState";
        private readonly HierarchyBuilder<string, int> testee;
        private readonly IImplicitAddIfNotAvailableStateDefinitionDictionary<string, int> states;
        private readonly StateDefinition<string, int> superState;
        private readonly IDictionary<string, string> initiallyLastActiveStates;

        public HierarchyBuilderFacts()
        {
            this.superState = new StateDefinition<string, int>(SuperState);
            this.states = A.Fake<IImplicitAddIfNotAvailableStateDefinitionDictionary<string, int>>();
            A.CallTo(() => this.states[SuperState]).Returns(this.superState);
            this.initiallyLastActiveStates = A.Fake<IDictionary<string, string>>();

            this.testee = new HierarchyBuilder<string, int>(SuperState, this.states, this.initiallyLastActiveStates);
        }

        [Theory]
        [InlineData(HistoryType.Deep)]
        [InlineData(HistoryType.Shallow)]
        [InlineData(HistoryType.None)]
        public void SetsHistoryTypeOfSuperState(HistoryType historyType)
        {
            this.testee.WithHistoryType(historyType);

            this.superState.HistoryType
                .Should().Be(historyType);
        }

        [Fact]
        public void SetsInitialSubStateOfSuperState()
        {
            const string SubState = "SubState";
            var subState = new StateDefinition<string, int>(SubState)
            {
                SuperStateModifiable = null
            };
            A.CallTo(() => this.states[SubState]).Returns(subState);

            this.testee.WithInitialSubState(SubState);

            this.superState.InitialState
                .Should().BeSameAs(subState);
        }

        [Fact]
        public void SettingTheInitialSubStateAlsoAddsItToTheInitiallyLastActiveStates()
        {
            const string SubState = "SubState";
            var subState = new StateDefinition<string, int>(SubState)
            {
                SuperStateModifiable = null
            };
            A.CallTo(() => this.states[SubState]).Returns(subState);

            this.testee.WithInitialSubState(SubState);

            A.CallTo(() => this.initiallyLastActiveStates.Add(SuperState, SubState)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void AddsSubStatesToSuperState()
        {
            const string AnotherSubState = "AnotherSubState";
            var anotherSubState = new StateDefinition<string, int>(AnotherSubState)
            {
                SuperStateModifiable = null
            };
            A.CallTo(() => this.states[AnotherSubState]).Returns(anotherSubState);

            this.testee
                .WithSubState(AnotherSubState);

            this.superState
                .SubStates
                .Should()
                .HaveCount(1)
                .And
                .Contain(anotherSubState);
        }

        [Fact]
        public void ThrowsExceptionIfSubStateAlreadyHasASuperState()
        {
            const string SubState = "SubState";
            var subState = new StateDefinition<string, int>(SubState)
            {
                SuperStateModifiable = new StateDefinition<string, int>("SomeOtherSuperState")
            };
            A.CallTo(() => this.states[SubState]).Returns(subState);

            this.testee.Invoking(t => t.WithInitialSubState(SubState))
                .Should().Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.CannotSetStateAsASuperStateBecauseASuperStateIsAlreadySet(
                    SuperState,
                    subState));
        }
    }
}