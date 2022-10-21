using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SystemBase.Utils.DotNet
{
    public static class LinqExtensions
    {
        public static T NthElement<T>(this IEnumerable<T> coll, int n)
        {
            return coll.OrderBy(x => x).Skip(n - 1).FirstOrDefault();
        }

        public static T[] RandomizeInPlace<T>(this T[] list)
        {
            for (var i = 0; i < list.Length; i++)
            {
                var rnd = (int) (Random.value * list.Length);
                (list[rnd], list[i]) = (list[i], list[rnd]);
            }

            return list;
        }
        
        public static List<T> RandomizeInPlace<T>(this List<T> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var rnd = (int) (Random.value * list.Count);
                (list[rnd], list[i]) = (list[i], list[rnd]);
            }

            return list;
        }
        
        public static T[] Randomize<T>(this T[] list)
        {
            var result = new T[list.Length];
            for (var i = 0; i < result.Length; i++)
            {
                var rnd = (int) (Random.value * result.Length);
                (result[rnd], result[i]) = (result[i], result[rnd]);
            }

            return result;
        }

        public static List<T> Randomize<T>(this IEnumerable<T> list)
        {
            var result = new List<T>(list);
            for (var i = 0; i < result.Count; i++)
            {
                var rnd = (int) (Random.value * result.Count);
                (result[rnd], result[i]) = (result[i], result[rnd]);
            }

            return result;
        }

        public static IEnumerable<T> AddDefaultCount<T>(this IEnumerable<T> coll, int n) where T : new()
        {
            var result = new List<T>();
            for (var i = 0; i < n; i++) result.Add(new T());

            return coll.Concat(result);
        }

        public static void Print<T>(this IEnumerable<T> list)
        {
            foreach (var element in list) Debug.Log(element);
        }

        public static void Print<T>(this IEnumerable<T> list, Func<T, string> returnStringToPrint)
        {
            foreach (var element in list) Debug.Log(returnStringToPrint(element));
        }
    }
}