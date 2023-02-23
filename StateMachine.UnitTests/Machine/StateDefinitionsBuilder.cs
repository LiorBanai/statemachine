 
// <copyright file="StateDefinitionsBuilder.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Machine;
using StateMachine.Syntax;

namespace StateMachine.UnitTests.Machine
{
    public class StateDefinitionsBuilder<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StandardFactory<TState, TEvent> factory = new StandardFactory<TState, TEvent>();
        private readonly ImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent> stateDefinitionDictionary = new ImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent>();
        private readonly Dictionary<TState, TState> initiallyLastActiveStates = new Dictionary<TState, TState>();

        public IEntryActionSyntax<TState, TEvent> In(TState state)
        {
            return new StateBuilder<TState, TEvent>(state, this.stateDefinitionDictionary, this.factory);
        }

        public IHierarchySyntax<TState> DefineHierarchyOn(TState superStateId)
        {
            return new HierarchyBuilder<TState, TEvent>(superStateId, this.stateDefinitionDictionary, this.initiallyLastActiveStates);
        }

        public IStateDefinitionDictionary<TState, TEvent> Build()
        {
            return new StateDefinitionDictionary<TState, TEvent>(this.stateDefinitionDictionary.ReadOnlyDictionary);
        }
    }
}