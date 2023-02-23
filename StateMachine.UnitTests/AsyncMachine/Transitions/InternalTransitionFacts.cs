 
// <copyright file="InternalTransitionFacts.cs"  
 

using System.Threading.Tasks;
using FakeItEasy;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class InternalTransitionFacts : SuccessfulTransitionWithExecutedActionsFactsBase
    {
        public InternalTransitionFacts()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = this.Source;
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = null; // target == null indicates an internal transition
        }

        [Fact]
        public async Task DoesNotExitState()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.Source, this.TransitionContext, this.LastActiveStateModifier)).MustNotHaveHappened();
        }

        [Fact]
        public async Task DoesNotEnterState()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Source, this.TransitionContext)).MustNotHaveHappened();
        }
    }
}