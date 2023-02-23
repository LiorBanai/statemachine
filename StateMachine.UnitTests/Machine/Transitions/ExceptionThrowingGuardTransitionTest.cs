 
// <copyright file="ExceptionThrowingGuardTransitionTest.cs"  
 

using System;
using FakeItEasy;
using FluentAssertions;
using StateMachine.Machine;
using Xunit;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class ExceptionThrowingGuardTransitionTest : TransitionTestBase
    {
        private Exception exception;

        public ExceptionThrowingGuardTransitionTest()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithStateDefinition(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;

            this.exception = new Exception();

            var guard = Builder<States, Events>.CreateGuardHolder().Throwing(this.exception).Build();
            this.TransitionDefinition.Guard = guard;
        }

        [Fact]
        public void CallsExtensionToHandleException()
        {
            var extension = A.Fake<IExtensionInternal<States, Events>>();

            this.ExtensionHost.Extension = extension;

            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => extension.HandlingGuardException(this.TransitionDefinition, this.TransitionContext, ref this.exception)).MustHaveHappened();
            A.CallTo(() => extension.HandledGuardException(this.TransitionDefinition, this.TransitionContext, this.exception)).MustHaveHappened();
        }

        [Fact]
        public void ReturnsNotFiredTransitionResult()
        {
            var result = this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            result.Fired.Should().BeFalse();
        }

        [Fact]
        public void NotifiesExceptionOnTransitionContext()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.TransitionContext.OnExceptionThrown(this.exception)).MustHaveHappened();
        }
    }
}