 
// <copyright file="ContextEventArgs.cs"  
 

using System;

namespace StateMachine.Machine.Events
{
    /// <summary>
    /// Event arguments holding context information.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public class ContextEventArgs<TState, TEvent>
        : EventArgs
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly ITransitionContext<TState, TEvent> context;

        protected ContextEventArgs(ITransitionContext<TState, TEvent> context)
        {
            this.context = context;
        }

        protected ITransitionContext<TState, TEvent> Context
        {
            get { return this.context; }
        }
    }
}