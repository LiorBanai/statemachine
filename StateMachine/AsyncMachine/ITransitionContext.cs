 
// <copyright file="ITransitionContext.cs"  
 

using System;
using StateMachine.AsyncMachine.States;

namespace StateMachine.AsyncMachine
{
    /// <summary>
    /// Provides information about the current transition.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface ITransitionContext<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        IStateDefinition<TState, TEvent> StateDefinition { get; }

        Missable<TEvent> EventId { get; }

        object EventArgument { get; }

        void AddRecord(TState stateId, RecordType recordType);

        string GetRecords();

        void OnExceptionThrown(Exception exception);

        void OnTransitionBegin();
    }
}