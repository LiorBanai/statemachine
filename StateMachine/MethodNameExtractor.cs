// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodNameExtractor.cs"  
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StateMachine
{
    public static class MethodNameExtractor
    {
        public static string ExtractMethodNameOrAnonymous(MethodInfo methodInfo)
        {
            return
                IsLambda(methodInfo)
                    ? "anonymous"
                    : methodInfo.Name;
        }

        private static bool IsLambda(MethodInfo methodInfo)
        {
            return methodInfo
                .DeclaringType
                .GetTypeInfo()
                .GetCustomAttributes(typeof(CompilerGeneratedAttribute), false)
                .Any();
        }
    }
}
