 
// <copyright file="ArgumentLessGuardHolder.cs"  
 

using System;
using System.Reflection;

namespace StateMachine.Machine.GuardHolders
{
    using static MethodNameExtractor;

    /// <summary>
    /// Holds an argument less guard.
    /// </summary>
    public class ArgumentLessGuardHolder : IGuardHolder
    {
        private readonly Func<bool> guard;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentLessGuardHolder"/> class.
        /// </summary>
        /// <param name="guard">The guard.</param>
        public ArgumentLessGuardHolder(Func<bool> guard)
        {
            this.guard = guard;
        }

        /// <summary>
        /// Executes the guard.
        /// </summary>
        /// <param name="argument">The state machine event argument.</param>
        /// <returns>Result of the guard execution.</returns>
        public bool Execute(object argument)
        {
            return this.guard();
        }

        /// <summary>
        /// Describes the guard.
        /// </summary>
        /// <returns>Description of the guard.</returns>
        public string Describe()
        {
            return ExtractMethodNameOrAnonymous(this.guard.GetMethodInfo());
        }
    }
}