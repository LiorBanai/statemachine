 
// <copyright file="IStateLogic.cs"  
 

using System;

namespace StateMachine.Machine.States
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
        ITransitionResult<TState> Fire(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context, ILastActiveStateModifier<TState> lastActiveStateModifier, IStateDefinitionDictionary<TState, TEvent> stateDefinitions);

        void Entry(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context);

        void Exit(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context, ILastActiveStateModifier<TState> lastActiveStateModifier);

        TState EnterByHistory(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context, ILastActiveStateModifier<TState> lastActiveStateModifier, IStateDefinitionDictionary<TState, TEvent> stateDefinitions);
    }
}