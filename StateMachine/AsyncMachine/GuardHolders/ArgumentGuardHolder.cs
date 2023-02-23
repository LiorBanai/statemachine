 
// <copyright file="ArgumentGuardHolder.cs"  
 

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace StateMachine.AsyncMachine.GuardHolders
{
    using static MethodNameExtractor;

    /// <summary>
    /// Holds a single argument guard.
    /// </summary>
    /// <typeparam name="T">Type of the argument of the guard.</typeparam>
    public class ArgumentGuardHolder<T> : IGuardHolder
    {
        private readonly MethodInfo originalGuardMethodInfo;
        private readonly Func<T, Task<bool>> guard;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentGuardHolder{T}"/> class.
        /// </summary>
        /// <param name="guard">The guard.</param>
        public ArgumentGuardHolder(Func<T, bool> guard)
        {
            this.originalGuardMethodInfo = guard.GetMethodInfo();
            this.guard = argument => guard(argument) ? TaskEx.True : TaskEx.False;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentGuardHolder{T}"/> class.
        /// </summary>
        /// <param name="guard">The guard.</param>
        public ArgumentGuardHolder(Func<T, Task<bool>> guard)
        {
            this.originalGuardMethodInfo = guard.GetMethodInfo();
            this.guard = guard;
        }

        /// <summary>
        /// Executes the guard.
        /// </summary>
        /// <param name="argument">The state machine event argument.</param>
        /// <returns>Result of the guard execution.</returns>
        public async Task<bool> Execute(object argument)
        {
            if (argument != null && !(argument is T))
            {
                throw new ArgumentException(GuardHoldersExceptionMessages.CannotCastArgumentToGuardArgument(argument, this.Describe()));
            }

            return await this.guard((T)argument).ConfigureAwait(false);
        }

        /// <summary>
        /// Describes the guard.
        /// </summary>
        /// <returns>Description of the guard.</returns>
        public string Describe()
        {
            return ExtractMethodNameOrAnonymous(this.originalGuardMethodInfo);
        }
    }
}