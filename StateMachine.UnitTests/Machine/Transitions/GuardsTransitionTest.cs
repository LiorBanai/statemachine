 
// <copyright file="GuardsTransitionTest.cs"  
 

using FakeItEasy;
using FluentAssertions;
using StateMachine.Machine;
using StateMachine.Machine.Transitions;
using Xunit;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class GuardsTransitionTest : TransitionTestBase
    {
        public GuardsTransitionTest()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithStateDefinition(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;
        }

        [Fact]
        public void Executes_WhenGuardIsMet()
        {
            var guard = Builder<States, Events>.CreateGuardHolder().ReturningTrue().Build();
            this.TransitionDefinition.Guard = guard;

            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void DoesNotExecute_WhenGuardIsNotMet()
        {
            var guard = Builder<States, Events>.CreateGuardHolder().ReturningFalse().Build();
            this.TransitionDefinition.Guard = guard;

            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.StateLogic.Entry(this.Target, this.TransitionContext)).MustNotHaveHappened();
        }

        [Fact]
        public void ReturnsNotFiredTransitionResult_WhenGuardIsNotMet()
        {
            var guard = Builder<States, Events>.CreateGuardHolder().ReturningFalse().Build();
            this.TransitionDefinition.Guard = guard;

            var result = this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            result.Should().BeNotFiredTransitionResult<States>();
        }

        [Fact]
        public void NotifiesExtensions_WhenGuardIsNotMet()
        {
            var extension = A.Fake<IExtensionInternal<States, Events>>();
            this.ExtensionHost.Extension = extension;

            var guard = Builder<States, Events>.CreateGuardHolder().ReturningFalse().Build();
            this.TransitionDefinition.Guard = guard;

            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => extension.SkippedTransition(
                A<ITransitionDefinition<States, Events>>.That.Matches(t => t.Source == this.Source && t.Target == this.Target),
                this.TransitionContext)).MustHaveHappened();
        }
    }
}