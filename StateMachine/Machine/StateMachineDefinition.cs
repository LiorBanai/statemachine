 
// <copyright file="StateMachineDefinition.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Machine.States;
using StateMachine.Machine.Transitions;

namespace StateMachine.Machine
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

        public PassiveStateMachine<TState, TEvent> CreatePassiveStateMachine()
        {
            var name = typeof(PassiveStateMachine<TState, TEvent>).FullNameToString();
            return this.CreatePassiveStateMachine(name);
        }

        public PassiveStateMachine<TState, TEvent> CreatePassiveStateMachine(string name)
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

            return new PassiveStateMachine<TState, TEvent>(stateMachine, stateContainer, this.stateDefinitions, this.initialState);
        }

        public ActiveStateMachine<TState, TEvent> CreateActiveStateMachine()
        {
            var name = typeof(ActiveStateMachine<TState, TEvent>).FullNameToString();
            return this.CreateActiveStateMachine(name);
        }

        public ActiveStateMachine<TState, TEvent> CreateActiveStateMachine(string name)
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

            return new ActiveStateMachine<TState, TEvent>(stateMachine, stateContainer, this.stateDefinitions, this.initialState);
        }
    }
}