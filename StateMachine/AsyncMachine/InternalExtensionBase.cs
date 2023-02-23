 
// <copyright file="InternalExtensionBase.cs"  
 

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StateMachine.AsyncMachine.States;
using StateMachine.AsyncMachine.Transitions;
using StateMachine.Infrastructure;

namespace StateMachine.AsyncMachine
{
    public class InternalExtensionBase<TState, TEvent> : IExtensionInternal<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        public virtual Task StartedStateMachine()
        {
            return TaskEx.Completed;
        }

        public virtual Task StoppedStateMachine()
        {
            return TaskEx.Completed;
        }

        public virtual Task EventQueued(TEvent eventId, object eventArgument)
        {
            return TaskEx.Completed;
        }

        public virtual Task EventQueuedWithPriority(TEvent eventId, object eventArgument)
        {
            return TaskEx.Completed;
        }

        public virtual Task SwitchedState(IStateDefinition<TState, TEvent> oldState, IStateDefinition<TState, TEvent> newState)
        {
            return TaskEx.Completed;
        }

        public virtual Task EnteringInitialState(TState state)
        {
            return TaskEx.Completed;
        }

        public virtual Task EnteredInitialState(TState state, ITransitionContext<TState, TEvent> context)
        {
            return TaskEx.Completed;
        }

        public virtual Task FiringEvent(ref TEvent eventId, ref object eventArgument)
        {
            return TaskEx.Completed;
        }

        public virtual Task FiredEvent(ITransitionContext<TState, TEvent> context)
        {
            return TaskEx.Completed;
        }

        public virtual Task HandlingEntryActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
            return TaskEx.Completed;
        }

        public virtual Task HandledEntryActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            Exception exception)
        {
            return TaskEx.Completed;
        }

        public virtual Task HandlingExitActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
            return TaskEx.Completed;
        }

        public virtual Task HandledExitActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            Exception exception)
        {
            return TaskEx.Completed;
        }

        public virtual Task HandlingGuardException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            ref Exception exception)
        {
            return TaskEx.Completed;
        }

        public virtual Task HandledGuardException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            Exception exception)
        {
            return TaskEx.Completed;
        }

        public virtual Task HandlingTransitionException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
            return TaskEx.Completed;
        }

        public virtual Task HandledTransitionException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            Exception exception)
        {
            return TaskEx.Completed;
        }

        public virtual Task SkippedTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> context)
        {
            return TaskEx.Completed;
        }

        public virtual Task ExecutingTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext)
        {
            return TaskEx.Completed;
        }

        public virtual Task ExecutedTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext)
        {
            return TaskEx.Completed;
        }

        public virtual Task Loaded(
            IInitializable<TState> loadedCurrentState,
            IReadOnlyDictionary<TState, TState> loadedHistoryStates,
            IReadOnlyCollection<EventInformation<TEvent>> events,
            IReadOnlyCollection<EventInformation<TEvent>> priorityEvents)
        {
            return TaskEx.Completed;
        }

        public Task EnteringState(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context)
        {
            return TaskEx.Completed;
        }
    }
}