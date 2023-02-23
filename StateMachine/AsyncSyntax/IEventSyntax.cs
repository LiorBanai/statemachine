 
// <copyright file="IEventSyntax.cs"  
 

namespace StateMachine.AsyncSyntax
{
    /// <summary>
    /// Defines the event syntax.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IEventSyntax<TState, TEvent>
    {
        /// <summary>
        /// Defines an event that is accepted.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>On syntax.</returns>
        IOnSyntax<TState, TEvent> On(TEvent eventId);
    }
}