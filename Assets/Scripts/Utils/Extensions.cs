using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class Extensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T[] GetRangeOut<T>(this IList<T> list, int startingIndex, int amount)
    {
        List<T> result = new List<T>();

        for (int i = startingIndex, j = 0; j < amount && i < list.Count; i++, j++)
        {
            result.Add(list[i]);
        }

        for (int i = list.Count - 1; i >= startingIndex; i--)
        {
            list.RemoveAt(i);
        }

        return result.ToArray();
    }
}
