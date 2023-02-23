 
// <copyright file="IExitActionSyntax.cs"  
 

using System;
using System.Threading.Tasks;

namespace StateMachine.AsyncSyntax
{
    /// <summary>
    /// Defines the exit action syntax.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IExitActionSyntax<TState, TEvent> : IEventSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines an exit action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Event syntax.</returns>
        IExitActionSyntax<TState, TEvent> ExecuteOnExit(Action action);

        /// <summary>
        /// Defines an exit action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Event syntax.</returns>
        /// <typeparam name="T">Type of the event argument passed to the action.</typeparam>
        IExitActionSyntax<TState, TEvent> ExecuteOnExit<T>(Action<T> action);

        /// <summary>
        /// Defines an exit action.
        /// </summary>
        /// <typeparam name="T">Type of the parameter of the exit action method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter that will be passed to the exit action.</param>
        /// <returns>Exit action syntax.</returns>
        IExitActionSyntax<TState, TEvent> ExecuteOnExitParametrized<T>(Action<T> action, T parameter);

        /// <summary>
        /// Defines an exit action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Event syntax.</returns>
        IExitActionSyntax<TState, TEvent> ExecuteOnExit(Func<Task> action);

        /// <summary>
        /// Defines an exit action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Event syntax.</returns>
        /// <typeparam name="T">Type of the event argument passed to the action.</typeparam>
        IExitActionSyntax<TState, TEvent> ExecuteOnExit<T>(Func<T, Task> action);

        /// <summary>
        /// Defines an exit action.
        /// </summary>
        /// <typeparam name="T">Type of the parameter of the exit action method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter that will be passed to the exit action.</param>
        /// <returns>Exit action syntax.</returns>
        IExitActionSyntax<TState, TEvent> ExecuteOnExitParametrized<T>(Func<T, Task> action, T parameter);
    }
}