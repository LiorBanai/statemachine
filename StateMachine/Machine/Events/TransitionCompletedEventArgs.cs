 
// <copyright file="TransitionCompletedEventArgs.cs"  
 

using System;

namespace StateMachine.Machine.Events
{
    /// <summary>
    /// Provides information about a completed transition.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public class TransitionCompletedEventArgs<TState, TEvent>
        : TransitionEventArgs<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// The new state the state machine is in after the transition.
        /// </summary>
        private readonly TState newStateId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionCompletedEventArgs&lt;TState, TEvent&gt;"/> class.
        /// </summary>
        /// <param name="newStateId">The new state id.</param>
        /// <param name="context">The context.</param>
        public TransitionCompletedEventArgs(TState newStateId, ITransitionContext<TState, TEvent> context)
            : base(context)
        {
            this.newStateId = newStateId;
        }

        /// <summary>
        /// Gets the new state id the state machine is in after the transition.
        /// </summary>
        /// <value>The new state id the state machine is in after the transition.</value>
        public TState NewStateId
        {
            get { return this.newStateId; }
        }
    }
}