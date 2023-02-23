 
// <copyright file="ITransitionDefinition.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.AsyncMachine.ActionHolders;
using StateMachine.AsyncMachine.GuardHolders;
using StateMachine.AsyncMachine.States;

namespace StateMachine.AsyncMachine.Transitions
{
    public interface ITransitionDefinition<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        IStateDefinition<TState, TEvent> Source { get; }

        IStateDefinition<TState, TEvent> Target { get; }

        IGuardHolder Guard { get; }

        IEnumerable<IActionHolder> Actions { get; }

        bool IsInternalTransition { get; }
    }
}