 
// <copyright file="EquivalenceExtensions.cs"  
 

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Collections;

namespace StateMachine.Specs
{
    public static class EquivalenceExtensions
    {
        public static void IsEquivalentInOrder<T>(this GenericCollectionAssertions<T> genericCollectionAssertions, IList<T> other)
            where T : class
        {
            var listToAssert = ConvertOrCastToList(genericCollectionAssertions.Subject);

            listToAssert
                .Count
                .Should()
                .Be(other.Count);

            for (var i = 0; i < listToAssert.Count; i++)
            {
                listToAssert[i]
                    .Should()
                    .BeEquivalentTo(other[i]);
            }
        }

        private static IList<T> ConvertOrCastToList<T>(IEnumerable<T> source)
        {
            return source as IList<T> ?? source.ToList();
        }
    }
}
