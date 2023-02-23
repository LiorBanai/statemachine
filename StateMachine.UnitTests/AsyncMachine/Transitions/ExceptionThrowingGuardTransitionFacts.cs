 
// <copyright file="ExceptionThrowingGuardTransitionFacts.cs"  
 

using System;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using StateMachine.AsyncMachine;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class ExceptionThrowingGuardTransitionFacts : TransitionFactsBase
    {
        private Exception exception;

        public ExceptionThrowingGuardTransitionFacts()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;

            this.exception = new Exception();

            var guard = Builder<States, Events>.CreateGuardHolder().Throwing(this.exception).Build();
            this.TransitionDefinition.Guard = guard;
        }

        [Fact]
        public async Task CallsExtensionToHandleException()
        {
            var extension = A.Fake<IExtensionInternal<States, Events>>();

            this.ExtensionHost.Extension = extension;

            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => extension.HandlingGuardException(this.TransitionDefinition, this.TransitionContext, ref this.exception)).MustHaveHappened();
            A.CallTo(() => extension.HandledGuardException(this.TransitionDefinition, this.TransitionContext, this.exception)).MustHaveHappened();
        }

        [Fact]
        public async Task ReturnsNotFiredTransitionResult()
        {
            var result = await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            result.Fired.Should().BeFalse();
        }

        [Fact]
        public async Task NotifiesExceptionOnTransitionContext()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.TransitionContext.OnExceptionThrown(this.exception)).MustHaveHappened();
        }
    }
}