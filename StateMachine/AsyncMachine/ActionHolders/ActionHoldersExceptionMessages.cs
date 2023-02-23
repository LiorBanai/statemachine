 
// <copyright file="ActionHoldersExceptionMessages.cs"  
 

using System.Globalization;

namespace StateMachine.AsyncMachine.ActionHolders
{
    /// <summary>
    /// Holds all exception messages.
    /// </summary>
    public static class ActionHoldersExceptionMessages
    {
        /// <summary>
        /// Cannot cast argument to action argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="action">The action.</param>
        /// <returns>error message.</returns>
        public static string CannotCastArgumentToActionArgument(object argument, string action)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "Cannot cast argument to match action method. Argument = {0}, Action = {1}",
                argument,
                action);
        }
    }
}