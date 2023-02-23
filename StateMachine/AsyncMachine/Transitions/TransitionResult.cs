 
// <copyright file="TransitionResult.cs"  
 

using System;

namespace StateMachine.AsyncMachine.Transitions
{
    public class TransitionResult<TState>
        : ITransitionResult<TState>
        where TState : IComparable
    {
        public static readonly ITransitionResult<TState> NotFired = new TransitionResult<TState>(false, default(TState));

        public TransitionResult(bool fired, TState newState)
        {
            this.Fired = fired;
            this.NewState = newState;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITransitionResult{TState}"/> is fired.
        /// </summary>
        /// <value><c>true</c> if fired; otherwise, <c>false</c>.</value>
        public bool Fired { get; }

        /// <summary>
        /// Gets the new state the state machine is in.
        /// </summary>
        /// <value>The new state.</value>
        public TState NewState { get; }
    }
}