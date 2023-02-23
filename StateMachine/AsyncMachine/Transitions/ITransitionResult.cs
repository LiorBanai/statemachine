 
// <copyright file="ITransitionResult.cs"  
 

using System;

namespace StateMachine.AsyncMachine.Transitions
{
    /// <summary>
    /// Represents the result of a transition.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public interface ITransitionResult<TState>
        where TState : IComparable
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ITransitionResult{TState}"/> is fired.
        /// </summary>
        /// <value><c>true</c> if fired; otherwise, <c>false</c>.</value>
        bool Fired { get; }

        /// <summary>
        /// Gets the new state.
        /// </summary>
        /// <value>The new state.</value>
        TState NewState { get; }
    }
}