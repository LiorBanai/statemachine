// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParametrizedActionHolder{T}.cs"  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace StateMachine.AsyncMachine.ActionHolders
{
    using static MethodNameExtractor;

    public class ParametrizedActionHolder<T> : IActionHolder
    {
        private readonly MethodInfo originalActionMethodInfo;
        private readonly Func<T, Task> action;
        private readonly T parameter;

        public ParametrizedActionHolder(Func<T, Task> action, T parameter)
        {
            this.originalActionMethodInfo = action.GetMethodInfo();
            this.action = action;
            this.parameter = parameter;
        }

        public ParametrizedActionHolder(Action<T> action, T parameter)
        {
            this.originalActionMethodInfo = action.GetMethodInfo();
            this.action = argument =>
            {
                action(argument);
                return TaskEx.Completed;
            };

            this.parameter = parameter;
        }

        public async Task Execute(object argument)
        {
            await this.action(this.parameter).ConfigureAwait(false);
        }

        public string Describe()
        {
            return ExtractMethodNameOrAnonymous(this.originalActionMethodInfo);
        }
    }
}