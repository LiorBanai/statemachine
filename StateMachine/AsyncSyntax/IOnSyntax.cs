 
// <copyright file="IOnSyntax.cs"  
 

using System;
using System.Threading.Tasks;

namespace StateMachine.AsyncSyntax
{
    /// <summary>
    /// Defines the syntax after On.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IOnSyntax<TState, TEvent> : IOnExecuteSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines the target state of the transition.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>Go to syntax.</returns>
        IGotoSyntax<TState, TEvent> Goto(TState target);

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
    }

    public interface IOnExecuteSyntax<TState, TEvent> : IEventSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IOnExecuteSyntax<TState, TEvent> Execute(Action action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IOnExecuteSyntax<TState, TEvent> Execute<T>(Action<T> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IOnExecuteSyntax<TState, TEvent> Execute(Func<Task> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IOnExecuteSyntax<TState, TEvent> Execute<T>(Func<T, Task> action);
    }
}