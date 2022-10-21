using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace SystemBase.Utils.DotNet
{
    public static class ListExtensions
    {
        public static T RandomElement<T>(this ICollection<T> collection)
        {
            var index = Random.Range(0, collection.Count);
            return collection.ElementAt(index);
        }
    }
}