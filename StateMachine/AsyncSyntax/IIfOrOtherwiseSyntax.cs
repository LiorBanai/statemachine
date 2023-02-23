 
// <copyright file="IIfOrOtherwiseSyntax.cs"  
 

using System;
using System.Threading.Tasks;

namespace StateMachine.AsyncSyntax
{
    /// <summary>
    /// Defines the syntax for If or Otherwise.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IIfOrOtherwiseSyntax<TState, TEvent> : IEventSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines a transition guard. The transition is only taken if the guard is fulfilled.
        /// </summary>
        /// <typeparam name="T">The type of the guard argument.</typeparam>
        /// <param name="guard">The guard.</param>
        /// <returns>If syntax.</returns>
        IIfSyntax<TState, TEvent> If<T>(Func<T, bool> guard);

        /// <summary>
        /// Defines a transition guard. The transition is only taken if the guard is fulfilled.
        /// </summary>
        /// <typeparam name="T">The type of the guard argument.</typeparam>
        /// <param name="guard">The guard.</param>
        /// <returns>If syntax.</returns>
        IIfSyntax<TState, TEvent> If<T>(Func<T, Task<bool>> guard);

        /// <summary>
        /// Defines a transition guard. The transition is only taken if the guard is fulfilled.
        /// </summary>
        /// <param name="guard">The guard.</param>
        /// <returns>If syntax.</returns>
        IIfSyntax<TState, TEvent> If(Func<bool> guard);

        /// <summary>
        /// Defines a transition guard. The transition is only taken if the guard is fulfilled.
        /// </summary>
        /// <param name="guard">The guard.</param>
        /// <returns>If syntax.</returns>
        IIfSyntax<TState, TEvent> If(Func<Task<bool>> guard);

        /// <summary>
        /// Defines the transition that is taken when the guards of all other transitions did not match.
        /// </summary>
        /// <returns>Default syntax.</returns>
        IOtherwiseSyntax<TState, TEvent> Otherwise();

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The actions to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IIfOrOtherwiseSyntax<TState, TEvent> Execute(Func<Task> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The actions to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IIfOrOtherwiseSyntax<TState, TEvent> Execute(Action action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The actions to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IIfOrOtherwiseSyntax<TState, TEvent> Execute<T>(Func<T, Task> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The actions to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IIfOrOtherwiseSyntax<TState, TEvent> Execute<T>(Action<T> action);
    }
}