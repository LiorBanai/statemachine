// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Missing.cs"  
// --------------------------------------------------------------------------------------------------------------------

namespace StateMachine.AsyncMachine
{
    public sealed class Missing
    {
        public static readonly Missing Value = new Missing();

        private Missing()
        {
        }
    }
}