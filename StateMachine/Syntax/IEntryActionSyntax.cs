 
// <copyright file="IEntryActionSyntax.cs"  
 

using System;

namespace StateMachine.Syntax
{
    /// <summary>
    /// Defines the entry action syntax.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IEntryActionSyntax<TState, TEvent> : IExitActionSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines an entry action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Exit action syntax.</returns>
        IEntryActionSyntax<TState, TEvent> ExecuteOnEntry(Action action);

        /// <summary>
        /// Defines an entry action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Exit action syntax.</returns>
        /// <typeparam name="T">Type of the event argument passed to the action.</typeparam>
        IEntryActionSyntax<TState, TEvent> ExecuteOnEntry<T>(Action<T> action);

        /// <summary>
        /// Defines an entry action.
        /// </summary>
        /// <typeparam name="T">Type of the parameter of the entry action method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter that will be passed to the entry action.</param>
        /// <returns>Exit action syntax.</returns>
        IEntryActionSyntax<TState, TEvent> ExecuteOnEntryParametrized<T>(Action<T> action, T parameter);
    }
}