 
// <copyright file="IGuardHolder.cs"  
 

using System.Threading.Tasks;

namespace StateMachine.AsyncMachine.GuardHolders
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
        Task<bool> Execute(object argument);

        /// <summary>
        /// Describes the guard.
        /// </summary>
        /// <returns>Description of the guard.</returns>
        string Describe();
    }
}