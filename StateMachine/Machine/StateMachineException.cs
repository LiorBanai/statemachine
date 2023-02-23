 
// <copyright file="StateMachineException.cs"  
 

using System;

namespace StateMachine.Machine
{
    public class StateMachineException : Exception
    {
        public StateMachineException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}