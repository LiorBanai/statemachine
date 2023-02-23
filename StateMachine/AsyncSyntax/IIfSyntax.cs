 
// <copyright file="IIfSyntax.cs"  
 

using System;
using System.Threading.Tasks;

namespace StateMachine.AsyncSyntax
{
    /// <summary>
    /// Defines the If syntax.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IIfSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines the target state of the transition.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>Go to syntax.</returns>
        IGotoInIfSyntax<TState, TEvent> Goto(TState target);

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
        IIfOrOtherwiseSyntax<TState, TEvent> Execute<T>(Action<T> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The actions to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IIfOrOtherwiseSyntax<TState, TEvent> Execute(Func<Task> action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The actions to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IIfOrOtherwiseSyntax<TState, TEvent> Execute<T>(Func<T, Task> action);
    }
}