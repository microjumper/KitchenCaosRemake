using System.Collections.Generic;

public static class SerializableKeyValuePairExtensions
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<SerializableKeyValuePair<TKey, TValue>> pairCollection)
    {
        var dictionary = new Dictionary<TKey, TValue>();

        foreach (var pair in pairCollection)
        {
            dictionary[pair.key] = pair.value;
        }

        return dictionary;
    }
}