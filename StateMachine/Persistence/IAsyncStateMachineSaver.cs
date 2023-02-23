 
// <copyright file="IAsyncStateMachineSaver.cs"  
 

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StateMachine.Infrastructure;

namespace StateMachine.Persistence
{
    public interface IAsyncStateMachineSaver<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Saves the current state of the state machine.
        /// </summary>
        /// <param name="currentStateId">Id of the current state.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveCurrentState(IInitializable<TState> currentStateId);

        /// <summary>
        /// Saves the last active states of all super states.
        /// </summary>
        /// <param name="historyStates">Key = id of the super state; Value = if of last active state of super state.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveHistoryStates(IReadOnlyDictionary<TState, TState> historyStates);

        /// <summary>
        /// Saves the not yet processed events of the state machine.
        /// </summary>
        /// <param name="events">The not yet processed events.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveEvents(IReadOnlyCollection<EventInformation<TEvent>> events);

        /// <summary>
        /// Saves the not yet processed priority events of the state machine.
        /// </summary>
        /// <param name="priorityEvents">The not yet processed priority events.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SavePriorityEvents(IReadOnlyCollection<EventInformation<TEvent>> priorityEvents);
    }
}