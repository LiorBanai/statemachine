 
// <copyright file="IInitializable.cs"  
 

using System;

namespace StateMachine.Infrastructure
{
    public interface IInitializable<out T>
    {
        bool IsInitialized { get; }

        Initializable<TResult> Map<TResult>(Func<T, TResult> func);

        T ExtractOrThrow();
    }
}