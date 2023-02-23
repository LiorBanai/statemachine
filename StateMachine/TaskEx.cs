 
// <copyright file="TaskEx.cs"  
 

using System.Threading.Tasks;

namespace StateMachine
{
    internal static class TaskEx
    {
        public static readonly Task Completed = Task.FromResult(0);

        public static readonly Task<bool> True = Task.FromResult(true);

        public static readonly Task<bool> False = Task.FromResult(false);
    }
}