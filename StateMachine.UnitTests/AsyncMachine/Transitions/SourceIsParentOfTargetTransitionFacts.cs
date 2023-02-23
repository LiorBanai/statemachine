 
// <copyright file="SourceIsParentOfTargetTransitionFacts.cs"  
 

using System.Threading.Tasks;
using FakeItEasy;
using StateMachine.AsyncMachine.States;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class SourceIsParentOfTargetTransitionFacts : SuccessfulTransitionWithExecutedActionsFactsBase
    {
        private readonly IStateDefinition<States, Events> intermediate;

        public SourceIsParentOfTargetTransitionFacts()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.intermediate = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.Source).Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.intermediate).Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public async Task EntersAllStatesBelowSourceDownToTarget()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.intermediate, this.TransitionContext)).MustHaveHappened()
                .Then(A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappened());
        }
    }
}