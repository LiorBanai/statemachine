 
// <copyright file="SourceIsDescendantOfTargetTransitionTest.cs"  
 

using FakeItEasy;
using StateMachine.Machine.States;
using Xunit;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class SourceIsDescendantOfTargetTransitionTest : SuccessfulTransitionWithExecutedActionsTestBase
    {
        private readonly IStateDefinition<States, Events> intermediate;

        public SourceIsDescendantOfTargetTransitionTest()
        {
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.intermediate = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.Target).Build();
            this.Source = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.intermediate).Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithStateDefinition(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public void ExitsOfAllStatesFromSourceUpToTarget()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.Source, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened()
                .Then(A.CallTo(() => this.StateLogic.Exit(this.intermediate, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened())
                .Then(A.CallTo(() => this.StateLogic.Exit(this.Target, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened());
        }

        [Fact]
        public void EntersTargetState()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappened();
        }
    }
}