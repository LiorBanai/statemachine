﻿ 
// <copyright file="StateMachineBuilder.cs"  
 

using System;
using StateMachine.Machine;
using StateMachine.Machine.States;
using StateMachine.Machine.Transitions;

namespace StateMachine.UnitTests.Machine
{
    public class StateMachineBuilder<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private StateContainer<TState, TEvent> stateContainer;

        public StateMachineBuilder()
        {
            this.stateContainer = new StateContainer<TState, TEvent>();
        }

        public StateMachineBuilder<TState, TEvent> WithStateContainer(StateContainer<TState, TEvent> stateContainerToUse)
        {
            this.stateContainer = stateContainerToUse;
            return this;
        }

        public StateMachine<TState, TEvent> Build()
        {
            var factory = new StandardFactory<TState, TEvent>();
            var transitionLogic = new TransitionLogic<TState, TEvent>(this.stateContainer);
            var stateLogic = new StateLogic<TState, TEvent>(transitionLogic, this.stateContainer);
            transitionLogic.SetStateLogic(stateLogic);

            return new StateMachine<TState, TEvent>(factory, stateLogic);
        }
    }
}
