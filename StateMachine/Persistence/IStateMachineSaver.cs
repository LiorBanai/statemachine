 
// <copyright file="IStateMachineSaver.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Infrastructure;

namespace StateMachine.Persistence
{
    public interface IStateMachineSaver<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Saves the current state of the state machine.
        /// </summary>
        /// <param name="currentStateId">Id of the current state.</param>
        void SaveCurrentState(IInitializable<TState> currentStateId);

        /// <summary>
        /// Saves the last active states of all super states.
        /// </summary>
        /// <param name="historyStates">Key = id of the super state; Value = if of last active state of super state.</param>
        void SaveHistoryStates(IReadOnlyDictionary<TState, TState> historyStates);

        /// <summary>
        /// Saves the not yet processed events of the state machine.
        /// </summary>
        /// <param name="events">The not yet processed events.</param>
        void SaveEvents(IReadOnlyCollection<EventInformation<TEvent>> events);
    }
}