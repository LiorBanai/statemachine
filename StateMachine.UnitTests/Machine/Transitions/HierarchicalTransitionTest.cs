 
// <copyright file="HierarchicalTransitionTest.cs"  
 

using FakeItEasy;
using StateMachine.Machine.States;
using Xunit;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class HierarchicalTransitionTest : SuccessfulTransitionWithExecutedActionsTestBase
    {
        private readonly IStateDefinition<States, Events> root;
        private readonly IStateDefinition<States, Events> superStateOfSource;
        private readonly IStateDefinition<States, Events> superStateOfTarget;

        public HierarchicalTransitionTest()
        {
            this.root = Builder<States, Events>.CreateStateDefinition().Build();
            this.superStateOfSource = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.root).Build();
            this.Source = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.superStateOfSource).Build();
            this.superStateOfTarget = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.root).Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.superStateOfTarget).Build();

            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithStateDefinition(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public void ExitsStatesUpToBelowCommonSuperState()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.Source, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened()
                .Then(A.CallTo(() => this.StateLogic.Exit(this.superStateOfSource, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened());
        }

        [Fact]
        public void EntersStatesBelowCommonSuperStateToTarget()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.superStateOfTarget, this.TransitionContext)).MustHaveHappened()
                .Then(A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappened());
        }

        [Fact]
        public void DoesNotExitCommonSuperState()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.root, this.TransitionContext, this.LastActiveStateModifier)).MustNotHaveHappened();
        }

        [Fact]
        public void DoesNotEnterCommonSuperState()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.root, this.TransitionContext)).MustNotHaveHappened();
        }
    }
}