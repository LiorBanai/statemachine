 
// <copyright file="IStateLogic.cs"  
 

using System;
using System.Threading.Tasks;
using StateMachine.AsyncMachine.Transitions;

namespace StateMachine.AsyncMachine.States
{
    public interface IStateLogic<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Fires the specified event id on this state.
        /// </summary>
        /// <param name="stateDefinition">The definition of the state onto which the event should be fired.</param>
        /// <param name="context">The event context.</param>
        /// <param name="lastActiveStateModifier">The last active state modifier.</param>
        /// <param name="stateDefinitions">The definitions for all states of this state Machine.</param>
        /// <returns>The result of the transition.</returns>
        Task<ITransitionResult<TState>> Fire(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context, ILastActiveStateModifier<TState> lastActiveStateModifier, IStateDefinitionDictionary<TState, TEvent> stateDefinitions);

        Task Entry(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context);

        Task Exit(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context, ILastActiveStateModifier<TState> lastActiveStateModifier);

        Task<TState> EnterByHistory(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context, ILastActiveStateModifier<TState> lastActiveStateModifier, IStateDefinitionDictionary<TState, TEvent> stateDefinitions);
    }
}