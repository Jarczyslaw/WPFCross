﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Commons
{
    public static class IEnumerableExtensions
    {
        public static int SafeMax<T>(this IEnumerable<T> enumerable, Func<T, int> selector, int defaultValue = default(int))
        {
            return enumerable.Any() ? enumerable.Max(selector) : defaultValue;
        }
    }
}
