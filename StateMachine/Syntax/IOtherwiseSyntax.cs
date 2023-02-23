 
// <copyright file="IOtherwiseSyntax.cs"  
 

using System;

namespace StateMachine.Syntax
{
    /// <summary>
    /// Defines the Otherwise syntax.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IOtherwiseSyntax<TState, TEvent> : IOtherwiseExecuteSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines the target state of the transition.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>Go to syntax.</returns>
        IGotoSyntax<TState, TEvent> Goto(TState target);
    }

    public interface IOtherwiseExecuteSyntax<TState, TEvent> : IEventSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IOtherwiseExecuteSyntax<TState, TEvent> Execute(Action action);

        /// <summary>
        /// Defines the transition actions.
        /// </summary>
        /// <typeparam name="T">The type of the action argument.</typeparam>
        /// <param name="action">The action to execute when the transition is taken.</param>
        /// <returns>Event syntax.</returns>
        IOtherwiseExecuteSyntax<TState, TEvent> Execute<T>(Action<T> action);
    }
}