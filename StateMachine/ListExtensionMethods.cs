 
// <copyright file="ListExtensionMethods.cs"  
 

using System;
using System.Collections.Generic;

namespace StateMachine
{
    internal static class ListExtensionMethods
    {
         public static void ForEach<T>(this IList<T> list, Action<T> action)
         {
             foreach (T item in list)
             {
                 action(item);
             }
         }
    }
}