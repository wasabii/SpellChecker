﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpellChecker.Core.Extension
{
    public static class EnumerableExtension
    {
        public static void Each(this IEnumerable items, Action<object> action)
        {
            foreach (var i in items)
                action(i);
        }

        public static void Each<T>(this IEnumerable items, Func<object, bool> function)
        {
            foreach (var i in items)
                if (!function(i))
                    break;
        }

        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var i in items)
                action(i);
        }

        public static void Each<T>(this IEnumerable<T> items, Func<T, bool> function)
        {
            foreach (var i in items)
                if (!function(i))
                    break;
        }

        public static void Each<T>(this IEnumerable<T> items, Action<T, int> action)
        {
            var itemList = items.ToArray();

            for (int i = 0; i < itemList.Count(); i++)
                action(itemList[i], i);
        }

        public static void Each<T>(this IEnumerable<T> items, Func<T, int, bool> function)
        {
            var itemList = items.ToArray();

            for (int i = 0; i < itemList.Count(); i++)
                if (!function(itemList[i], i))
                    break;
        }
    }
}
