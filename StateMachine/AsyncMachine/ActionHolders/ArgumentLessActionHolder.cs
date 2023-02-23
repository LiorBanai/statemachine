 
// <copyright file="ArgumentLessActionHolder.cs"  
 

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace StateMachine.AsyncMachine.ActionHolders
{
    using static MethodNameExtractor;

    public class ArgumentLessActionHolder : IActionHolder
    {
        private readonly MethodInfo originalActionMethodInfo;
        private readonly Func<Task> action;

        public ArgumentLessActionHolder(Func<Task> action)
        {
            this.originalActionMethodInfo = action.GetMethodInfo();
            this.action = action;
        }

        public ArgumentLessActionHolder(Action action)
        {
            this.originalActionMethodInfo = action.GetMethodInfo();
            this.action = () =>
            {
                action();
                return TaskEx.Completed;
            };
        }

        public async Task Execute(object argument)
        {
            await this.action().ConfigureAwait(false);
        }

        public string Describe()
        {
            return ExtractMethodNameOrAnonymous(this.originalActionMethodInfo);
        }
    }
}