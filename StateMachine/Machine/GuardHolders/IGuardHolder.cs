 
// <copyright file="IGuardHolder.cs"  
 

namespace StateMachine.Machine.GuardHolders
{
    /// <summary>
    /// Holds a guard.
    /// </summary>
    public interface IGuardHolder
    {
        /// <summary>
        /// Executes the guard.
        /// </summary>
        /// <param name="argument">The state machine event argument.</param>
        /// <returns>Result of the guard execution.</returns>
        bool Execute(object argument);

        /// <summary>
        /// Describes the guard.
        /// </summary>
        /// <returns>Description of the guard.</returns>
        string Describe();
    }
}