 
// <copyright file="ITransitionDictionary.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Machine.Transitions;

namespace StateMachine.Machine
{
    public interface ITransitionDictionary<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Adds the specified event id.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="transitionDefinition">The transition.</param>
        void Add(TEvent eventId, TransitionDefinition<TState, TEvent> transitionDefinition);

        /// <summary>
        /// Gets all transitions.
        /// </summary>
        /// <returns>All transitions.</returns>
        IEnumerable<TransitionInfo<TState, TEvent>> GetTransitions();
    }
}