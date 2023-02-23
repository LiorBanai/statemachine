 
// <copyright file="TransitionExceptionEventArgs.cs"  
 

using System;

namespace StateMachine.Machine.Events
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
        /// The exception.
        /// </summary>
        private readonly Exception exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionExceptionEventArgs&lt;TState, TEvent&gt;"/> class.
        /// </summary>
        /// <param name="context">The event context.</param>
        /// <param name="exception">The exception.</param>
        public TransitionExceptionEventArgs(ITransitionContext<TState, TEvent> context, Exception exception)
            : base(context)
        {
            this.exception = exception;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get { return this.exception; }
        }
    }
}