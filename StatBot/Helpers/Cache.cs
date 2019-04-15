using System.Collections.Concurrent;

namespace StatBot.Helpers
{
    public abstract class Cache<TKey, TValue>
    {
        protected readonly ConcurrentDictionary<TKey, TValue> cache = new ConcurrentDictionary<TKey, TValue>();

        public TValue GetOrAdd(TKey key)
        {
            return cache.GetOrAdd(key, GetValue);
        }

        protected abstract TValue GetValue(TKey key);
    }
}