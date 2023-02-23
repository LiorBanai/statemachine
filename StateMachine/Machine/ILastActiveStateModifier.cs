 
// <copyright file="ILastActiveStateModifier.cs"  
 

using System;
using StateMachine.Infrastructure;

namespace StateMachine.Machine
{
    public interface ILastActiveStateModifier<TState>
        where TState : IComparable
    {
        Optional<TState> GetLastActiveStateFor(TState state);

        void SetLastActiveStateFor(TState state, TState newLastActiveState);
    }
}