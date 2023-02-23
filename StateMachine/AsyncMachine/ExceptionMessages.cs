 
// <copyright file="ExceptionMessages.cs"  
 

using System;
using System.Globalization;
using StateMachine.AsyncMachine.States;
using StateMachine.AsyncMachine.Transitions;

namespace StateMachine.AsyncMachine
{
    /// <summary>
    /// Holds all exception messages.
    /// </summary>
    public static class ExceptionMessages
    {
        public const string InitialStateNotConfigured = "Initial state is not configured.";

        public const string ValueNotInitialized = "Value is not initialized";

        /// <summary>
        /// State machine is already initialized.
        /// </summary>
        public const string StateMachineIsAlreadyInitialized = "state machine is already initialized";

        /// <summary>
        /// State machine is not initialized.
        /// </summary>
        public const string StateMachineNotInitialized = "state machine is not initialized";

        /// <summary>
        /// State machine has not yet entered initial state.
        /// </summary>
        public const string StateMachineHasNotYetEnteredInitialState = "Initial state is not yet entered.";

        /// <summary>
        /// There must not be more than one transition for a single event of a state with no guard.
        /// </summary>
        public const string OnlyOneTransitionMayHaveNoGuard = "There must not be more than one transition for a single event of a state with no guard.";

        /// <summary>
        /// Transition without guard has to be last declared one.
        /// </summary>
        public const string TransitionWithoutGuardHasToBeLast = "The transition without guard has to be the last defined transition because state machine checks transitions in order of declaration.";

        public const string CannotSetALastActiveStateThatIsNotASubState = "The state that is set as the last active state of a super state has to be a sub state";

        /// <summary>
        /// State cannot be its own super-state..
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>error message.</returns>
        public static string StateCannotBeItsOwnSuperState(string state)
        {
            return string.Format(CultureInfo.InvariantCulture, "State {0} cannot be its own super-state.", state);
        }

        public static string CannotSetStateAsASuperStateBecauseASuperStateIsAlreadySet<TState, TEvent>(TState newSuperStateId, IStateDefinition<TState, TEvent> stateAlreadyHavingASuperState)
            where TState : IComparable
            where TEvent : IComparable
        {
            Guard.AgainstNullArgument("stateAlreadyHavingASuperState", stateAlreadyHavingASuperState);

            return string.Format(
                CultureInfo.InvariantCulture,
                "Cannot set state {0} as a super state because the state {1} has already a super state {2}.",
                newSuperStateId,
                stateAlreadyHavingASuperState.Id,
                stateAlreadyHavingASuperState.SuperState.Id);
        }

        /// <summary>
        /// Transition cannot be added to the state because it has already been added to the state.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="transition">The transition.</param>
        /// <param name="state">The state.</param>
        /// <returns>error message.</returns>
        public static string TransitionDoesAlreadyExist<TState, TEvent>(ITransitionDefinition<TState, TEvent> transition, IStateDefinition<TState, TEvent> state)
            where TState : IComparable
            where TEvent : IComparable
        {
            Guard.AgainstNullArgument("transition", transition);

            return string.Format(
                        CultureInfo.InvariantCulture,
                        "Transition {0} cannot be added to the state {1} because it has already been added to the state {2}.",
                        transition,
                        state,
                        transition.Source);
        }

        public static string CannotFindStateDefinition<TState>(TState state)
            where TState : IComparable
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "Cannot find StateDefinition for state {0}. Are you sure you have configured this state via myStateDefinitionBuilder.In(..) or myStateDefinitionBuilder.DefineHierarchyOn(..)?",
                state);
        }
    }
}