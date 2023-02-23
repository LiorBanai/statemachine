 
// <copyright file="IGotoSyntax.cs"  
 

using System;
using System.Threading.Tasks;

namespace StateMachine.AsyncSyntax
{
    /// <summary>
    /// Defines the syntax after go to.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IGotoSyntax<TState, TEvent> : IEventSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IGotoSyntax<TState, TEvent> Execute(Action action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IGotoSyntax<TState, TEvent> Execute<T>(Action<T> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IGotoSyntax<TState, TEvent> Execute(Func<Task> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IGotoSyntax<TState, TEvent> Execute<T>(Func<T, Task> action);
    }
}