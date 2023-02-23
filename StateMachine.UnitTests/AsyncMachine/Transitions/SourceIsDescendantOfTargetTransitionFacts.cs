 
// <copyright file="SourceIsDescendantOfTargetTransitionFacts.cs"  
 

using System.Threading.Tasks;
using FakeItEasy;
using StateMachine.AsyncMachine.States;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class SourceIsDescendantOfTargetTransitionFacts : SuccessfulTransitionWithExecutedActionsFactsBase
    {
        private readonly IStateDefinition<States, Events> intermediate;

        public SourceIsDescendantOfTargetTransitionFacts()
        {
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.intermediate = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.Target).Build();
            this.Source = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.intermediate).Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public async Task ExitsOfAllStatesFromSourceUpToTarget()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.Source, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened()
                .Then(A.CallTo(() => this.StateLogic.Exit(this.intermediate, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened())
                .Then(A.CallTo(() => this.StateLogic.Exit(this.Target, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened());
        }

        [Fact]
        public async Task EntersTargetState()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappened();
        }
    }
}