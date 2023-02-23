 
// <copyright file="ArgumentLessActionHolder.cs"  
 

using System;
using System.Reflection;

namespace StateMachine.Machine.ActionHolders
{
    using static MethodNameExtractor;

    public class ArgumentLessActionHolder : IActionHolder
    {
        private readonly Action action;

        public ArgumentLessActionHolder(Action action)
        {
            this.action = action;
        }

        public void Execute(object argument)
        {
            this.action();
        }

        public string Describe()
        {
            return ExtractMethodNameOrAnonymous(this.action.GetMethodInfo());
        }
    }
}