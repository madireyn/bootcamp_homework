using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;

namespace Collections
{


    public class ArrayDictionary<TKey, TValue> : Collections.IDictionary<TKey, TValue>
    {
        private Pair[] _pairs;

        public ArrayDictionary()
        {
            _pairs = MakeArrayOfPairs(10);
            Count = 0;
        }

        private void ExpandArrayCheck()
        {
            if (Count != _pairs.Length) return;
            var newItems = MakeArrayOfPairs(_pairs.Length * 2);
            Transfer(_pairs, newItems);
            _pairs = newItems;
        }

        private Pair[] MakeArrayOfPairs(int arraySize)
        {
            return new Pair[arraySize];
        }

        private void Transfer(Pair[] source, Pair[] destination)
        {
            for (var i = 0; i < Count; i++)
            {
                destination[i] = source[i];
            }
        }

        private int GetIndex(TKey key)
        {
            for (var i = 0; i < Count; i++)
            {
                if (_pairs[i].Key != null && _pairs[i].Key.Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!this.TryGetValue(key, out var value))
                    throw new KeyNotFoundException();

                return value;
            }
            set
            {
                var index = GetIndex(key);
                _pairs[index] = new Pair(key, value);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                var keysList = new List<TKey>();
                for (var i = 0; i < Count; i++)
                {
                    var key = _pairs[i].Key;
                    keysList.Add(key);
                }

                return keysList;
            }

        }

        public ICollection<TValue> Values
        {
            get
            {
                var valueCollection = new Collection<TValue>();
                foreach (var keyValuePair in _pairs)
                {
                    valueCollection.Add(keyValuePair.Value);
                }

                return valueCollection;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            ExpandArrayCheck();
            var index = GetIndex(key);
            if (index != -1)
            {
                this[key] = value;
            }
            else
            {
                Count++;
                var newPair = new Pair(key, value);
                _pairs[Count - 1] = newPair;
            }
        }

        public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            for (var i = 0; i < Count; i++)
            {
                RemoveKeyValuePair(_pairs[i].Key);
            }
        }

        public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            var index = GetIndex(item.Key);
            return _pairs[index].Equals(item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            return GetIndex(key) != -1;
        }

        public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Copy(this, array, arrayIndex);
        }

        private static void Copy<T>(ICollection<T> source, T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if ((array.Length - arrayIndex) < source.Count)
            {
                throw new ArgumentException();
            }

            foreach (var item in source)
            {
                array[arrayIndex++] = item;
            }
        }

        public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                var keyValuePair = new KeyValuePair<TKey, TValue>(_pairs[i].Key, _pairs[i].Value);
                yield return keyValuePair;
            }
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsEmpty()
            {
                return Count == 0;
            }

        public TValue RemoveAndReturnValue(TKey key)
        {
            var index = GetIndex(key);
            var removedVal = default(TValue);

            if (index == -1)
            {
                throw new KeyNotFoundException();
            }

            if ((_pairs[index] != null) && (_pairs[index].Key == null ||
                                            _pairs[index].Key.Equals(key)))
            {
                _pairs[index] = _pairs[Count - 1];
                _pairs[Count - 1] = null;
            }
            Count--;
            return removedVal;
        }

        public bool Remove(TKey key)
        {
            if (GetIndex(key) == -1)
            {
                return false;
            }
            RemoveKeyValuePair(key);
            return true;
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public int Size() => Count;

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (GetIndex(key) == -1)
            {
                value = default(TValue);
                return false;
            }
            value = this[key];
            return true;
        }

        public void RemoveKeyValuePair(TKey key)
        {
            var index = GetIndex(key);
            if (index == -1)
            {
                throw new KeyNotFoundException();
            }

            if ((_pairs[index] != null) && (_pairs[index].Key == null ||
                                            _pairs[index].Key.Equals(key)))
            {
                _pairs[index] = _pairs[Count - 1];
                _pairs[Count - 1] = null;
            }
            Count--;
        }

        private class Pair
        {
            public TKey Key { get; }
            public TValue Value { get; }

            public Pair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public override string ToString()
            {
                return "[" + Key + ", " + Value + "]";
            }
        }
    }
}