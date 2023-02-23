 
// <copyright file="IStateMachineReport.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Machine.States;

namespace StateMachine.Machine
{
    /// <summary>
    /// Generates a report of the state machine.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IStateMachineReport<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Generates a report of the state machine.
        /// </summary>
        /// <param name="name">The name of the state machine.</param>
        /// <param name="states">The states.</param>
        /// <param name="initialStateId">The initial state id.</param>
        void Report(string name, IEnumerable<IStateDefinition<TState, TEvent>> states, TState initialStateId);
    }
}