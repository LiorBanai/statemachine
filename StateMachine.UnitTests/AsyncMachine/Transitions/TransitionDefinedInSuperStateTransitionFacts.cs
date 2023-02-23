 
// <copyright file="TransitionDefinedInSuperStateTransitionFacts.cs"  
 

using System.Threading.Tasks;
using FakeItEasy;
using StateMachine.AsyncMachine.States;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class TransitionDefinedInSuperStateTransitionFacts : TransitionFactsBase
    {
        private readonly IStateDefinition<States, Events> intermediate;
        private readonly IStateDefinition<States, Events> current;

        public TransitionDefinedInSuperStateTransitionFacts()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.intermediate = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.Source).Build();
            this.current = Builder<States, Events>.CreateStateDefinition().WithSuperState(this.intermediate).Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.current).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public async Task ExitsAllStatesFromCurrentUpToSource()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Exit(this.current, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened()
                .Then(A.CallTo(() => this.StateLogic.Exit(this.intermediate, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened())
                .Then(A.CallTo(() => this.StateLogic.Exit(this.Source, this.TransitionContext, this.LastActiveStateModifier)).MustHaveHappened());
        }
    }
}