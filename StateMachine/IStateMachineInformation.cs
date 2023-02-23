 
// <copyright file="IStateMachineInformation.cs"  
 

using System;
using StateMachine.Infrastructure;

namespace StateMachine
{
    /// <summary>
    /// Provides information about a state machine.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    // ReSharper disable once UnusedTypeParameter because of symmetry with all other interface declarations (all have states and events)
    public interface IStateMachineInformation<out TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <value>The name of this instance.</value>
        string Name { get; }

        /// <summary>
        /// Gets the id of the current state.
        /// </summary>
        /// <value>The id of the current state.</value>
        IInitializable<TState> CurrentStateId { get; }
    }
}