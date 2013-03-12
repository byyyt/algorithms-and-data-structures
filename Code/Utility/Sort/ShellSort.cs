﻿using System;
using System.Collections.Generic;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] ShellSort(T[] input)
        {
            int gap = 1, k;
            T current;
            List<int> gaps = new List<int>();

            while (gap < input.Length)
            {
                gaps.Add(gap);
                gap = (int)Math.Ceiling(2.25 * gaps[gaps.Count - 1]);
            }

            for (int i = gaps.Count - 1; i >= 0; i--)
            {
                gap = gaps[i];
                for (int j = gap; j < input.Length; j++)
                {
                    current = input[j];
                    for (k = j; k >= gap; k -= gap)
                    {
                        if (input[k - gap].CompareTo(current) > 0)
                            input[k] = input[k - gap];
                        else
                            break;
                    }
                    input[k] = current;
                }
            }

            return input;
        }
    }
}