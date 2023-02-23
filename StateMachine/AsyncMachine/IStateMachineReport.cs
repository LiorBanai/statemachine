 
// <copyright file="IStateMachineReport.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.AsyncMachine.States;

namespace StateMachine.AsyncMachine
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
        /// <param name="stateDefinitions">The states.</param>
        /// <param name="initialStateId">The initial state id.</param>
        void Report(string name, IEnumerable<IStateDefinition<TState, TEvent>> stateDefinitions, TState initialStateId);
    }
}