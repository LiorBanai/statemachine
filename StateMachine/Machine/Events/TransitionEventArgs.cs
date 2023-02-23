 
// <copyright file="TransitionEventArgs.cs"  
 

using System;
using System.Globalization;

namespace StateMachine.Machine.Events
{
    /// <summary>
    /// Event arguments providing a transition context.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public class TransitionEventArgs<TState, TEvent>
        : ContextEventArgs<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionEventArgs&lt;TState, TEvent&gt;"/> class.
        /// </summary>
        /// <param name="context">The event context.</param>
        public TransitionEventArgs(ITransitionContext<TState, TEvent> context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets the id of the source state of the transition.
        /// </summary>
        /// <value>The id of the source state of the transition.</value>
        public TState StateId => this.Context.StateDefinition.Id;

        /// <summary>
        /// Gets the event id.
        /// </summary>
        /// <value>The event id.</value>
        public TEvent EventId => this.Context.EventId.Value;

        /// <summary>
        /// Gets the event argument.
        /// </summary>
        /// <value>The event argument.</value>
        public object EventArgument => this.Context.EventArgument;

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Transition from state {0} on event {1}.", this.StateId, this.EventId);
        }
    }
}