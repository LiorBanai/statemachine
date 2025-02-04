﻿ 
// <copyright file="Builder.cs"  
 

using System;
using FakeItEasy;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.GuardHolders;
using StateMachine.AsyncMachine.States;

namespace StateMachine.UnitTests.AsyncMachine
{
    public static class Builder<TState, TEvent>
            where TState : IComparable
            where TEvent : IComparable
    {
        public static GuardBuilder CreateGuardHolder()
        {
            return new GuardBuilder();
        }

        public static StateBuilder CreateStateDefinition()
        {
            return new StateBuilder();
        }

        public static TransitionContextBuilder CreateTransitionContext()
        {
            return new TransitionContextBuilder();
        }

        public class GuardBuilder
        {
            private readonly IGuardHolder guardHolder;

            public GuardBuilder()
            {
                this.guardHolder = A.Fake<IGuardHolder>();
            }

            public GuardBuilder ReturningTrue()
            {
                A.CallTo(() => this.guardHolder.Execute(A<object>._)).Returns(true);

                return this;
            }

            public GuardBuilder ReturningFalse()
            {
                A.CallTo(() => this.guardHolder.Execute(A<object>._)).Returns(false);

                return this;
            }

            public GuardBuilder Throwing(Exception exception)
            {
                A.CallTo(() => this.guardHolder.Execute(A<object>._)).Throws(exception);

                return this;
            }

            public IGuardHolder Build()
            {
                return this.guardHolder;
            }
        }

        public class StateBuilder
        {
            private readonly IStateDefinition<TState, TEvent> stateDefinition;

            private IStateDefinition<TState, TEvent> superState;

            private int level;

            public StateBuilder()
            {
                this.stateDefinition = A.Fake<IStateDefinition<TState, TEvent>>();
            }

            public StateBuilder WithSuperState(IStateDefinition<TState, TEvent> newSuperState)
            {
                this.superState = newSuperState;
                this.level = newSuperState.Level + 1;

                return this;
            }

            public IStateDefinition<TState, TEvent> Build()
            {
                A.CallTo(() => this.stateDefinition.SuperState).Returns(this.superState);
                A.CallTo(() => this.stateDefinition.Level).Returns(this.level);

                return this.stateDefinition;
            }
        }

        public class TransitionContextBuilder
        {
            private readonly ITransitionContext<TState, TEvent> transitionContext;

            public TransitionContextBuilder()
            {
                this.transitionContext = A.Fake<ITransitionContext<TState, TEvent>>();
            }

            public TransitionContextBuilder WithState(IStateDefinition<TState, TEvent> state)
            {
                A.CallTo(() => this.transitionContext.StateDefinition).Returns(state);

                return this;
            }

            public ITransitionContext<TState, TEvent> Build()
            {
                return this.transitionContext;
            }
        }
    }
}