 
// <copyright file="TransitionInfo.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Machine.ActionHolders;
using StateMachine.Machine.GuardHolders;
using StateMachine.Machine.States;

namespace StateMachine.Machine.Transitions
{
    /// <summary>
    /// Describes a transition.
    /// </summary>
    /// <typeparam name="TState">Type fo the states.</typeparam>
    /// <typeparam name="TEvent">Type of the events.</typeparam>
    public class TransitionInfo<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        public TransitionInfo(TEvent eventId, IStateDefinition<TState, TEvent> source, IStateDefinition<TState, TEvent> target, IGuardHolder guard, IEnumerable<IActionHolder> actions)
        {
            this.EventId = eventId;
            this.Source = source;
            this.Target = target;
            this.Guard = guard;
            this.Actions = actions;
        }

        /// <summary>
        /// Gets the event id.
        /// </summary>
        /// <value>The event id.</value>
        public TEvent EventId { get; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        public IStateDefinition<TState, TEvent> Source { get; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        public IStateDefinition<TState, TEvent> Target { get; }

        /// <summary>
        /// Gets the guard.
        /// </summary>
        /// <value>The guard.</value>
        public IGuardHolder Guard { get; }

        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <value>The actions.</value>
        public IEnumerable<IActionHolder> Actions { get; }
    }
}