 
// <copyright file="SourceIsParentOfTargetTransitionTest.cs"  
 

using FakeItEasy;
using StateMachine.Machine.States;
using Xunit;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class SourceIsParentOfTargetTransitionTest : SuccessfulTransitionWithExecutedActionsTestBase
    {
        private readonly IStateDefinition<States, Events> intermediate;

        public SourceIsParentOfTargetTransitionTest()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.intermediate = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.Source).Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.intermediate).Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithStateDefinition(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public void EntersAllStatesBelowSourceDownToTarget()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.intermediate, this.TransitionContext)).MustHaveHappened()
                .Then(A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappened());
        }
    }
}