 
// <copyright file="INotifier.cs"  
 

using System;

namespace StateMachine.AsyncMachine
{
    /// <summary>
    /// Provides functionalities to notify events.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface INotifier<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Called when an exception was thrown.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        void OnExceptionThrown(ITransitionContext<TState, TEvent> context, Exception exception);

        /// <summary>
        /// Called before a transition is executed.
        /// </summary>
        /// <param name="transitionContext">The context.</param>
        void OnTransitionBegin(ITransitionContext<TState, TEvent> transitionContext);
    }
}