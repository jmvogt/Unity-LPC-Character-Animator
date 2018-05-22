using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public static class CollectionExtensions
    {
        public static TValue SafeGet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null || key == null) return default(TValue);

            if (!dictionary.ContainsKey(key)) return default(TValue);

            return dictionary[key];
        }
    }
}
