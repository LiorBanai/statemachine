 
// <copyright file="IActionHolder.cs"  
 

using System.Threading.Tasks;

namespace StateMachine.AsyncMachine.ActionHolders
{
    /// <summary>
    /// Holds a transition action.
    /// </summary>
    public interface IActionHolder
    {
        /// <summary>
        /// Executes the transition action.
        /// </summary>
        /// <param name="argument">The state machine event argument.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Execute(object argument);

        /// <summary>
        /// Describes the action.
        /// </summary>
        /// <returns>Description of the action.</returns>
        string Describe();
    }
}