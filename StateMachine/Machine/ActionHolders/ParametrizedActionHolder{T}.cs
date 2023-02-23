// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParametrizedActionHolder{T}.cs"  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

namespace StateMachine.Machine.ActionHolders
{
    using static MethodNameExtractor;

    public class ParametrizedActionHolder<T> : IActionHolder
    {
        private readonly Action<T> action;

        private readonly T parameter;

        public ParametrizedActionHolder(Action<T> action, T parameter)
        {
            this.action = action;
            this.parameter = parameter;
        }

        public void Execute(object argument)
        {
            this.action(this.parameter);
        }

        public string Describe()
        {
            return ExtractMethodNameOrAnonymous(this.action.GetMethodInfo());
        }
    }
}