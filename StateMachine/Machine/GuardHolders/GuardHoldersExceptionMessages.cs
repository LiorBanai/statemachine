 
// <copyright file="GuardHoldersExceptionMessages.cs"  
 

using System.Globalization;

namespace StateMachine.Machine.GuardHolders
{
    /// <summary>
    /// Holds all exception messages.
    /// </summary>
    public static class GuardHoldersExceptionMessages
    {
        /// <summary>
        /// Cannot cast argument to guard argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="guard">The guard.</param>
        /// <returns>error message.</returns>
        public static string CannotCastArgumentToGuardArgument(object argument, string guard)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "Cannot cast argument to match guard method. Argument = {0}, Guard = {1}",
                argument,
                guard);
        }
    }
}