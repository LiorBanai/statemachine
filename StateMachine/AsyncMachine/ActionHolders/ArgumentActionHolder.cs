 
// <copyright file="ArgumentActionHolder.cs"  
 

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace StateMachine.AsyncMachine.ActionHolders
{
    using static MethodNameExtractor;

    public class ArgumentActionHolder<T> : IActionHolder
    {
        private readonly MethodInfo originalActionMethodInfo;
        private readonly Func<T, Task> action;

        public ArgumentActionHolder(Func<T, Task> action)
        {
            this.originalActionMethodInfo = action.GetMethodInfo();
            this.action = action;
        }

        public ArgumentActionHolder(Action<T> action)
        {
            this.originalActionMethodInfo = action.GetMethodInfo();
            this.action = argument =>
            {
                action(argument);
                return TaskEx.Completed;
            };
        }

        public async Task Execute(object argument)
        {
            T castArgument = default(T);

            if (argument != System.Reflection.Missing.Value && argument != null && !(argument is T))
            {
                throw new ArgumentException(ActionHoldersExceptionMessages.CannotCastArgumentToActionArgument(argument, this.Describe()));
            }

            if (argument != System.Reflection.Missing.Value)
            {
                castArgument = (T)argument;
            }

            await this.action(castArgument).ConfigureAwait(false);
        }

        public string Describe()
        {
            return ExtractMethodNameOrAnonymous(this.originalActionMethodInfo);
        }
    }
}