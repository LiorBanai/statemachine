 
// <copyright file="GuardsTransitionFacts.cs"  
 

using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.Transitions;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class GuardsTransitionFacts : TransitionFactsBase
    {
        public GuardsTransitionFacts()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public async Task Executes_WhenGuardIsMet()
        {
            var guard = Builder<States, Events>.CreateGuardHolder().ReturningTrue().Build();
            this.TransitionDefinition.Guard = guard;

            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DoesNotExecute_WhenGuardIsNotMet()
        {
            var guard = Builder<States, Events>.CreateGuardHolder().ReturningFalse().Build();
            this.TransitionDefinition.Guard = guard;

            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustNotHaveHappened();
        }

        [Fact]
        public async Task ReturnsNotFiredTransitionResult_WhenGuardIsNotMet()
        {
            var guard = Builder<States, Events>.CreateGuardHolder().ReturningFalse().Build();
            this.TransitionDefinition.Guard = guard;

            var result = await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            result.Should().BeNotFiredTransitionResult<States>();
        }

        [Fact]
        public async Task NotifiesExtensions_WhenGuardIsNotMet()
        {
            var extension = A.Fake<IExtensionInternal<States, Events>>();
            this.ExtensionHost.Extension = extension;

            var guard = Builder<States, Events>.CreateGuardHolder().ReturningFalse().Build();
            this.TransitionDefinition.Guard = guard;

            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => extension.SkippedTransition(
                A<ITransitionDefinition<States, Events>>.That.Matches(t => t.Source == this.Source && t.Target == this.Target),
                this.TransitionContext)).MustHaveHappened();
        }
    }
}