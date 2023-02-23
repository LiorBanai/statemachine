 
// <copyright file="StringExtensions.cs"  
 

namespace StateMachine.UnitTests
{
    internal static class StringExtensions
    {
        public static string IgnoringNewlines(this string s) =>
            s
                .Replace("\n", string.Empty)
                .Replace("\r", string.Empty);
    }
}