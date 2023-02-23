 
// <copyright file="ExceptionThrowingActionTransitionTest.cs"  
 

using System;
using FakeItEasy;
using FluentAssertions;
using StateMachine.Machine;
using StateMachine.Machine.ActionHolders;
using Xunit;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class ExceptionThrowingActionTransitionTest : TransitionTestBase
    {
        private Exception exception;

        public ExceptionThrowingActionTransitionTest()
        {
            this.Source = Builder<States, Events>.CreateStateDefinition().Build();
            this.Target = Builder<States, Events>.CreateStateDefinition().Build();
            this.TransitionContext = Builder<States, Events>.CreateTransitionContext().WithStateDefinition(this.Source).Build();

            this.TransitionDefinition.Source = this.Source;
            this.TransitionDefinition.Target = this.Target;

            this.exception = new Exception();

            this.TransitionDefinition.ActionsModifiable.Add(new ArgumentLessActionHolder(() => throw this.exception));
        }

        [Fact]
        public void CallsExtensionToHandleException()
        {
            var extension = A.Fake<IExtensionInternal<States, Events>>();

            this.ExtensionHost.Extension = extension;

            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => extension.HandlingTransitionException(this.TransitionDefinition, this.TransitionContext, ref this.exception)).MustHaveHappened();
            A.CallTo(() => extension.HandledTransitionException(this.TransitionDefinition, this.TransitionContext, this.exception)).MustHaveHappened();
        }

        [Fact]
        public void ReturnsFiredTransitionResult()
        {
            var result = this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            result.Fired.Should().BeTrue();
        }

        [Fact]
        public void NotifiesExceptionOnTransitionContext()
        {
            this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            A.CallTo(() => this.TransitionContext.OnExceptionThrown(this.exception)).MustHaveHappened();
        }
    }
}