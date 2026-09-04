public interface IRepository<TKey, TValue>
{
    bool TryGet(TKey key, out TValue value);
}