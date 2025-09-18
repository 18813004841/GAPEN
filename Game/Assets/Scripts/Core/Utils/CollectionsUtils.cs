using System.Collections.Generic;

namespace Core.Utils
{
    public static class CollectionsUtils
    {
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> collection)
        {
            if (hashSet == null)
            {
                throw new System.ArgumentNullException(nameof(hashSet));
            }

            if (collection == null)
            {
                throw new System.ArgumentNullException(nameof(collection));
            }

            foreach (var item in collection)
            {
                hashSet.Add(item);
            }
        }
    }
}