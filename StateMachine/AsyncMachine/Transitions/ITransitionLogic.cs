 
// <copyright file="ITransitionLogic.cs"  
 

using System;
using System.Threading.Tasks;

namespace StateMachine.AsyncMachine.Transitions
{
    public interface ITransitionLogic<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        /// <summary>
        /// Fires the transition.
        /// </summary>
        /// <param name="transitionDefinition">The definition of the transition which should happen.</param>
        /// <param name="context">The event context.</param>
        /// <param name="lastActiveStateModifier">The last active state modifier.</param>
        /// <param name="stateDefinitions">The definitions for all states of this state Machine.</param>
        /// <returns>The result of the transition.</returns>
        Task<ITransitionResult<TState>> Fire(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> context,
            ILastActiveStateModifier<TState> lastActiveStateModifier,
            IStateDefinitionDictionary<TState, TEvent> stateDefinitions);
    }
}
