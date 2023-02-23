 
// <copyright file="HistoryType.cs"  
 

namespace StateMachine
{
    /// <summary>
    /// Defines the history behavior of a state (on re-entrance of a super state).
    /// </summary>
    public enum HistoryType
    {
        /// <summary>
        /// The state enters into its initial sub-state. The sub-state itself enters its initial sub-state and so on
        /// until the innermost nested state is reached.
        /// </summary>
        None,

        /// <summary>
        /// The state enters into its last active sub-state. The sub-state itself enters its initial sub-state and so on
        /// until the innermost nested state is reached.
        /// </summary>
        Shallow,

        /// <summary>
        /// The state enters into its last active sub-state. The sub-state itself enters into-its last active state and so on
        /// until the innermost nested state is reached.
        /// </summary>
        Deep
    }
}