 
// <copyright file="TransitionTest.cs"  
 

using FakeItEasy;
using Xunit;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class TransitionTest : SuccessfulTransitionWithExecutedActionsTestBase
    {
        public TransitionTest()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithStateDefinition(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public void EntersDestinationState()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void ExitsSourceState()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.Source, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void NotifiesTransitionBeginOnTransitionContext()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.TransitionContext.OnTransitionBegin()).MustHaveHappened();
        }
    }
}