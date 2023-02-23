 
// <copyright file="ImplicitAddIfNotAvailableStateDefinitionDictionary.cs"  
 

using System;
using System.Collections.Generic;
using System.Linq;
using StateMachine.AsyncMachine.States;

namespace StateMachine.AsyncMachine
{
    public class ImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent> : IImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly Dictionary<TState, StateDefinition<TState, TEvent>> dictionary = new Dictionary<TState, StateDefinition<TState, TEvent>>();

        public StateDefinition<TState, TEvent> this[TState stateId]
        {
            get
            {
                if (!this.dictionary.ContainsKey(stateId))
                {
                    this.dictionary.Add(stateId, new StateDefinition<TState, TEvent>(stateId));
                }

                return this.dictionary[stateId];
            }
        }

        public IReadOnlyDictionary<TState, IStateDefinition<TState, TEvent>> ReadOnlyDictionary =>
            this.dictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => (IStateDefinition<TState, TEvent>)pair.Value);
    }
}