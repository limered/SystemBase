using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace SystemBase.Utils
{
    public static class LinqExtensions
    {
        public static T NthElement<T>(this IEnumerable<T> coll, int n)
        {
            return coll.OrderBy(x => x).Skip(n - 1).FirstOrDefault();
        }

        public static List<T> Randomize<T>(this List<T> list)
        {
            var result = new List<T>(list.Count);
            while (list.Count > 0)
            {
                var rnd = (int) (Random.value * list.Count);
                result.Add(list[rnd]);
                list.RemoveAt(rnd);
            }
            return result;
        }
    }
}
