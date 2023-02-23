 
// <copyright file="ArgumentActionHolder.cs"  
 

using System;
using System.Reflection;

namespace StateMachine.Machine.ActionHolders
{
    using static MethodNameExtractor;

    public class ArgumentActionHolder<T> : IActionHolder
    {
        private readonly Action<T> action;

        public ArgumentActionHolder(Action<T> action)
        {
            this.action = action;
        }

        public void Execute(object argument)
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

            this.action(castArgument);
        }

        public string Describe()
        {
            return ExtractMethodNameOrAnonymous(this.action.GetMethodInfo());
        }
    }
}