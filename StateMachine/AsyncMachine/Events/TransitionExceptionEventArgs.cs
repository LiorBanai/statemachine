 
// <copyright file="TransitionExceptionEventArgs.cs"  
 

using System;

namespace StateMachine.AsyncMachine.Events
{
    /// <summary>
    /// Event arguments providing transition exceptions.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public class TransitionExceptionEventArgs<TState, TEvent>
        : TransitionEventArgs<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionExceptionEventArgs{TState,TEvent}"/> class.
        /// </summary>
        /// <param name="context">The event context.</param>
        /// <param name="exception">The exception.</param>
        public TransitionExceptionEventArgs(ITransitionContext<TState, TEvent> context, Exception exception)
            : base(context)
        {
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; }
    }
}