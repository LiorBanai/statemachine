 
// <copyright file="InternalExtensionBase.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Infrastructure;
using StateMachine.Machine;
using StateMachine.Machine.States;
using StateMachine.Machine.Transitions;

namespace StateMachine.Extensions
{
    public class InternalExtensionBase<TState, TEvent> : IExtensionInternal<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        public virtual void StartedStateMachine()
        {
        }

        public virtual void StoppedStateMachine()
        {
        }

        public virtual void EventQueued(TEvent eventId, object eventArgument)
        {
        }

        public virtual void EventQueuedWithPriority(TEvent eventId, object eventArgument)
        {
        }

        public virtual void SwitchedState(IStateDefinition<TState, TEvent> oldState, IStateDefinition<TState, TEvent> newState)
        {
        }

        public virtual void EnteringInitialState(TState state)
        {
        }

        public virtual void EnteredInitialState(TState state, ITransitionContext<TState, TEvent> context)
        {
        }

        public virtual void FiringEvent(ref TEvent eventId, ref object eventArgument)
        {
        }

        public virtual void FiredEvent(ITransitionContext<TState, TEvent> context)
        {
        }

        public virtual void HandlingEntryActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
        }

        public virtual void HandledEntryActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            Exception exception)
        {
        }

        public virtual void HandlingExitActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
        }

        public virtual void HandledExitActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            Exception exception)
        {
        }

        public virtual void HandlingGuardException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            ref Exception exception)
        {
        }

        public virtual void HandledGuardException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            Exception exception)
        {
        }

        public virtual void HandlingTransitionException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
        }

        public virtual void HandledTransitionException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            Exception exception)
        {
        }

        public virtual void SkippedTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> context)
        {
        }

        public virtual void ExecutingTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext)
        {
        }

        public virtual void ExecutedTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext)
        {
        }

        public virtual void Loaded(
            IInitializable<TState> loadedCurrentState,
            IReadOnlyDictionary<TState, TState> loadedHistoryStates,
            IReadOnlyCollection<EventInformation<TEvent>> events)
        {
        }

        public void EnteringState(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context)
        {
        }
    }
}