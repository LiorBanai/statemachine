 
// <copyright file="IGotoInIfSyntax.cs"  
 

using System;
using System.Threading.Tasks;

namespace StateMachine.AsyncSyntax
{
    /// <summary>
    /// Defines the go to syntax inside an If.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IGotoInIfSyntax<TState, TEvent> : IEventSyntax<TState, TEvent>
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
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IGotoInIfSyntax<TState, TEvent> Execute(Action action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IGotoInIfSyntax<TState, TEvent> Execute<T>(Action<T> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IGotoInIfSyntax<TState, TEvent> Execute(Func<Task> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IGotoInIfSyntax<TState, TEvent> Execute<T>(Func<T, Task> action);
    }
}