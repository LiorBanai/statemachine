 
// <copyright file="IActionHolder.cs"  
 

namespace StateMachine.Machine.ActionHolders
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
        void Execute(object argument);

        /// <summary>
        /// Describes the action.
        /// </summary>
        /// <returns>Description of the action.</returns>
        string Describe();
    }
}