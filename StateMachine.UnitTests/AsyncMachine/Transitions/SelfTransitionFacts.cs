 
// <copyright file="SelfTransitionFacts.cs"  
 

using System.Threading.Tasks;
using FakeItEasy;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class SelfTransitionFacts : SuccessfulTransitionWithExecutedActionsFactsBase
    {
        public SelfTransitionFacts()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = this.Source;
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Source;
        }

        [Fact]
        public async Task ExitsState()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.Source, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened();
        }

        [Fact]
        public async Task EntersState()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappened();
        }
    }
}