// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Catch.cs"  
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace StateMachine.Specs
{
    public static class Catch
    {
        public static Exception Exception(Action action)
        {
            try
            {
                action();

                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        public static async Task<Exception> Exception(Func<Task> action)
        {
            try
            {
                await action();

                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }
    }
}