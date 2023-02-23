 
// <copyright file="ExceptionThrowingActionTransitionFacts.cs"  
 

using System;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.ActionHolders;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class ExceptionThrowingActionTransitionFacts : TransitionFactsBase
    {
        private Exception exception;

        public ExceptionThrowingActionTransitionFacts()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithState(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;

            this.exception = new Exception();

            this.TransitionDefinition.ActionsModifiable.Add(new ArgumentLessActionHolder(() => throw this.exception));
        }

        [Fact]
        public async Task CallsExtensionToHandleException()
        {
            var extension = A.Fake<IExtensionInternal<States, Events>>();

            this.ExtensionHost.Extension = extension;

            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => extension.HandlingTransitionException(this.TransitionDefinition, this.TransitionContext, ref this.exception)).MustHaveHappened();
            A.CallTo(() => extension.HandledTransitionException(this.TransitionDefinition, this.TransitionContext, this.exception)).MustHaveHappened();
        }

        [Fact]
        public async Task ReturnsFiredTransitionResult()
        {
            var result = await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            result.Fired.Should().BeTrue();
        }

        [Fact]
        public async Task NotifiesExceptionOnTransitionContext()
        {
            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.TransitionContext.OnExceptionThrown(this.exception)).MustHaveHappened();
        }
    }
}