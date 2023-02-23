 
// <copyright file="StatesExceptionMessages.cs"  
 

using System.Globalization;

namespace StateMachine.AsyncMachine.States
{
    /// <summary>
    /// Holds all exception messages.
    /// </summary>
    public static class StatesExceptionMessages
    {
        /// <summary>
        /// State cannot be its own super-state..
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>error message.</returns>
        public static string StateCannotBeItsOwnSuperState(string state)
        {
            return string.Format(CultureInfo.InvariantCulture, "State {0} cannot be its own super-state.", state);
        }

        /// <summary>
        /// State cannot be the initial sub-state to itself.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>error message.</returns>
        public static string StateCannotBeTheInitialSubStateToItself(string state)
        {
            return string.Format(
                CultureInfo.InvariantCulture, "State {0} cannot be the initial sub-state to itself.", state);
        }

        /// <summary>
        /// State cannot be the initial state of super state because it is not a direct sub-state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="superState">State of the super.</param>
        /// <returns>error message.</returns>
        public static string StateCannotBeTheInitialStateOfSuperStateBecauseItIsNotADirectSubState(
            string state, string superState)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "State {0} cannot be the initial state of super state {1} because it is not a direct sub-state.",
                state,
                superState);
        }
    }
}