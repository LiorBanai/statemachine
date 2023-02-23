 
// <copyright file="IStateMachineLoader.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Infrastructure;

namespace StateMachine.Persistence
{
    public interface IStateMachineLoader<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Returns the state to be set as the current state of the state machine.
        /// </summary>
        /// <returns>State id.</returns>
        IInitializable<TState> LoadCurrentState();

        /// <summary>
        /// Returns the last active state of all super states that have a last active state (i.e. they count as visited).
        /// </summary>
        /// <returns>Key = id of super state, Value = id of last active state.</returns>
        IReadOnlyDictionary<TState, TState> LoadHistoryStates();

        /// <summary>
        /// Returns the events to be processed by the state machine.
        /// </summary>
        /// <returns>The events.</returns>
        IReadOnlyCollection<EventInformation<TEvent>> LoadEvents();
    }
}