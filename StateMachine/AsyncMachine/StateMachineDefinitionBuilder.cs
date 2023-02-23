 
// <copyright file="StateMachineDefinitionBuilder.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.AsyncSyntax;

namespace StateMachine.AsyncMachine
{
    public class StateMachineDefinitionBuilder<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StandardFactory<TState, TEvent> factory = new StandardFactory<TState, TEvent>();
        private readonly ImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent> stateDefinitionDictionary = new ImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent>();
        private readonly Dictionary<TState, TState> initiallyLastActiveStates = new Dictionary<TState, TState>();
        private TState initialState;
        private bool isInitialStateConfigured;

        public IEntryActionSyntax<TState, TEvent> In(TState state)
        {
            return new StateBuilder<TState, TEvent>(state, this.stateDefinitionDictionary, this.factory);
        }

        public IHierarchySyntax<TState> DefineHierarchyOn(TState superStateId)
        {
            return new HierarchyBuilder<TState, TEvent>(superStateId, this.stateDefinitionDictionary, this.initiallyLastActiveStates);
        }

        public StateMachineDefinitionBuilder<TState, TEvent> WithInitialState(TState initialStateToUse)
        {
            this.initialState = initialStateToUse;
            this.isInitialStateConfigured = true;
            return this;
        }

        public StateMachineDefinition<TState, TEvent> Build()
        {
            if (!this.isInitialStateConfigured)
            {
                throw new InvalidOperationException(ExceptionMessages.InitialStateNotConfigured);
            }

            var stateDefinitions = new StateDefinitionDictionary<TState, TEvent>(this.stateDefinitionDictionary.ReadOnlyDictionary);
            return new StateMachineDefinition<TState, TEvent>(stateDefinitions, this.initiallyLastActiveStates, this.initialState);
        }
    }
}