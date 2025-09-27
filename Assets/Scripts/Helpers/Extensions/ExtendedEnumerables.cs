using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Extends the functionality of lists and arrays.
/// </summary>
public static class ExtendedEnumerables
{
    /// <summary>
    ///     Selects a random item from the list.
    /// </summary>
    /// <typeparam name="T">Type of items held in the list.</typeparam>
    /// <param name="list">Reference to the list.</param>
    /// <returns>Random item from list</returns>
    public static T SelectRandom<T>(this IEnumerable<T> list)
    {
        if (list == null) return default;
        var size = list.Count();
        if(size == 0) return default;

        return list.ElementAt(Random.Range(0, size));
    }
}
