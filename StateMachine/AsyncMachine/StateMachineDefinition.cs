 
// <copyright file="StateMachineDefinition.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.AsyncMachine.States;
using StateMachine.AsyncMachine.Transitions;

namespace StateMachine.AsyncMachine
{
    public class StateMachineDefinition<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly IStateDefinitionDictionary<TState, TEvent> stateDefinitions;
        private readonly IReadOnlyDictionary<TState, TState> initiallyLastActiveStates;
        private readonly TState initialState;

        public StateMachineDefinition(
            IStateDefinitionDictionary<TState, TEvent> stateDefinitions,
            IReadOnlyDictionary<TState, TState> initiallyLastActiveStates,
            TState initialState)
        {
            this.stateDefinitions = stateDefinitions;
            this.initiallyLastActiveStates = initiallyLastActiveStates;
            this.initialState = initialState;
        }

        public AsyncPassiveStateMachine<TState, TEvent> CreatePassiveStateMachine()
        {
            var name = typeof(AsyncPassiveStateMachine<TState, TEvent>).FullNameToString();
            return this.CreatePassiveStateMachine(name);
        }

        public AsyncPassiveStateMachine<TState, TEvent> CreatePassiveStateMachine(string name)
        {
            var stateContainer = new StateContainer<TState, TEvent>(name);
            foreach (var stateIdAndLastActiveState in this.initiallyLastActiveStates)
            {
                stateContainer.SetLastActiveStateFor(stateIdAndLastActiveState.Key, stateIdAndLastActiveState.Value);
            }

            var transitionLogic = new TransitionLogic<TState, TEvent>(stateContainer);
            var stateLogic = new StateLogic<TState, TEvent>(transitionLogic, stateContainer);
            transitionLogic.SetStateLogic(stateLogic);

            var standardFactory = new StandardFactory<TState, TEvent>();
            var stateMachine = new StateMachine<TState, TEvent>(standardFactory, stateLogic);

            return new AsyncPassiveStateMachine<TState, TEvent>(stateMachine, stateContainer, this.stateDefinitions, this.initialState);
        }

        public AsyncActiveStateMachine<TState, TEvent> CreateActiveStateMachine()
        {
            var name = typeof(AsyncActiveStateMachine<TState, TEvent>).FullNameToString();
            return this.CreateActiveStateMachine(name);
        }

        public AsyncActiveStateMachine<TState, TEvent> CreateActiveStateMachine(string name)
        {
            var stateContainer = new StateContainer<TState, TEvent>(name);
            foreach (var stateIdAndLastActiveState in this.initiallyLastActiveStates)
            {
                stateContainer.SetLastActiveStateFor(stateIdAndLastActiveState.Key, stateIdAndLastActiveState.Value);
            }

            var transitionLogic = new TransitionLogic<TState, TEvent>(stateContainer);
            var stateLogic = new StateLogic<TState, TEvent>(transitionLogic, stateContainer);
            transitionLogic.SetStateLogic(stateLogic);

            var standardFactory = new StandardFactory<TState, TEvent>();
            var stateMachine = new StateMachine<TState, TEvent>(standardFactory, stateLogic);

            return new AsyncActiveStateMachine<TState, TEvent>(stateMachine, stateContainer, this.stateDefinitions, this.initialState);
        }
    }
}