using System.Collections.Generic;
using SystemCollections = System.Collections.Generic;

namespace Collections
{
    public interface IDictionary<TKey, TValue> : SystemCollections.IDictionary<TKey, TValue> { 

        int Size();

        bool IsEmpty();

        TValue RemoveAndReturnValue(TKey key);

        void RemoveKeyValuePair(TKey key);

        TValue GetValue(TKey key);



    }
}