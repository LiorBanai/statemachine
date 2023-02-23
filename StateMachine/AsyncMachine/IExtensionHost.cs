 
// <copyright file="IExtensionHost.cs"  
 

using System;
using System.Threading.Tasks;

namespace StateMachine.AsyncMachine
{
    /// <summary>
    /// Interface to execute actions on all extensions of the event broker.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IExtensionHost<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Executes the specified action for all extensions.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ForEach(Func<IExtensionInternal<TState, TEvent>, Task> action);
    }
}