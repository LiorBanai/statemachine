 
// <copyright file="GuardTest.cs"  
 

using FluentAssertions;
using StateMachine.Infrastructure;
using StateMachine.Machine;
using Xunit;

namespace StateMachine.UnitTests.Machine
{
    /// <summary>
    /// Tests the guard feature of the <see cref="StateMachine"/>.
    /// </summary>
    public class GuardTest
    {
        [Fact]
        public void EventArgumentIsPassedToTheGuard()
        {
            const string EventArgument = "test";
            string actualEventArgument = null;

            var stateDefinitionBuilder = new StateDefinitionsBuilder<States, Events>();
            stateDefinitionBuilder
                .In(States.A)
                    .On(Events.A)
                    .If<string>(argument =>
                    {
                        actualEventArgument = argument;
                        return true;
                    })
                    .Goto(States.B);
            var stateDefinitions = stateDefinitionBuilder
                .Build();
            var stateContainer = new StateContainer<States, Events>();

            var testee = new StateMachineBuilder<States, Events>()
                .WithStateContainer(stateContainer)
                .Build();

            testee.EnterInitialState(stateContainer, stateDefinitions, States.A);

            testee.Fire(Events.A, EventArgument, stateContainer, stateDefinitions);

            actualEventArgument.Should().Be(EventArgument);
        }

        [Fact]
        public void GuardWithoutArguments()
        {
            var stateDefinitionBuilder = new StateDefinitionsBuilder<States, Events>();
            stateDefinitionBuilder
                .In(States.A)
                    .On(Events.B)
                    .If(() => false).Goto(States.C)
                    .If(() => true).Goto(States.B);
            var stateDefinitions = stateDefinitionBuilder
                .Build();

            var stateContainer = new StateContainer<States, Events>();

            var testee = new StateMachineBuilder<States, Events>()
                .WithStateContainer(stateContainer)
                .Build();

            testee.EnterInitialState(stateContainer, stateDefinitions, States.A);

            testee.Fire(Events.B, stateContainer, stateContainer, stateDefinitions);

            stateContainer
                .CurrentStateId
                .Should()
                .BeEquivalentTo(Initializable<States>.Initialized(States.B));
        }

        [Fact]
        public void GuardWithASingleArgument()
        {
            var stateDefinitionBuilder = new StateDefinitionsBuilder<States, Events>();
            stateDefinitionBuilder
                .In(States.A)
                    .On(Events.B)
                    .If<int>(SingleIntArgumentGuardReturningFalse).Goto(States.C)
                    .If(() => false).Goto(States.D)
                    .If(() => false).Goto(States.E)
                    .If<int>(SingleIntArgumentGuardReturningTrue).Goto(States.B);
            var stateDefinitions = stateDefinitionBuilder
                    .Build();
            var stateContainer = new StateContainer<States, Events>();

            var testee = new StateMachineBuilder<States, Events>()
                .WithStateContainer(stateContainer)
                .Build();

            testee.EnterInitialState(stateContainer, stateDefinitions, States.A);

            testee.Fire(Events.B, 3, stateContainer, stateDefinitions);

            stateContainer
                .CurrentStateId
                .Should()
                .BeEquivalentTo(Initializable<States>.Initialized(States.B));
        }

        private static bool SingleIntArgumentGuardReturningTrue(int i)
        {
            return true;
        }

        private static bool SingleIntArgumentGuardReturningFalse(int i)
        {
            return false;
        }
    }
}