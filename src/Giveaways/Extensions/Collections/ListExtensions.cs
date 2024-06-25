using System;
using System.Collections.Generic;

namespace Giveaways;

/// <summary>
/// Provides extension methods for <see cref="IList{T}"/>.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Shuffles the elements of the specified sequence in random order.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The sequence to shuffle.</param>
    /// <returns>An enumerable sequence with the elements in random order.</returns>
    public static IEnumerable<T> Shuffle<T>(this IList<T> source)
    {
        for (int i = source.Count - 1; i >= 0; i--)
        {
            int swapIndex = Random.Shared.Next(i + 1);
            yield return source[swapIndex];
            source[swapIndex] = source[i];
        }
    }
}
