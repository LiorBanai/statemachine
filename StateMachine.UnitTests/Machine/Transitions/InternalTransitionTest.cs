 
// <copyright file="InternalTransitionTest.cs"  
 

using FakeItEasy;
using Xunit;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class InternalTransitionTest : SuccessfulTransitionWithExecutedActionsTestBase
    {
        public InternalTransitionTest()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = this.Source;
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithStateDefinition(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = null; // target == null indicates an internal transition
        }

        [Fact]
        public void DoesNotExitState()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.Source, this.TransitionContext, this.LastActiveStateModifier)).MustNotHaveHappened();
        }

        [Fact]
        public void DoesNotEnterState()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Source, this.TransitionContext)).MustNotHaveHappened();
        }
    }
}