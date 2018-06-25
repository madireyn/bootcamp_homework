using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{

    public class ArrayDictionary<TKey, TValue> : Collections.IDictionary<TKey, TValue>
    {
        private Pair<TKey, TValue>[] _pairs;

        public ArrayDictionary()
        {
            _pairs = MakeArrayOfPairs(10);
            Count = 0;
        }

        private void ExpandArrayCheck()
        {
            if (Count != _pairs.Length) return;
            var newItems = MakeArrayOfPairs(_pairs.Length + 10);
            Transfer(_pairs, newItems);
            _pairs = newItems;
        }

        private Pair<TKey, TValue>[] MakeArrayOfPairs(int arraySize)
        {
            if (arraySize < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return new Pair<TKey, TValue>[arraySize];
        }

        private void Transfer(Pair<TKey, TValue>[] source, Pair<TKey, TValue>[] destination)
        {

            if (source.Length > destination.Length)
            {
                throw new IndexOutOfRangeException();
            }

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
                var index = GetIndex(key);

                if (index > _pairs.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                if (!TryGetValue(key, out var value))
                {
                    throw new KeyNotFoundException();
                }
                return _pairs[index].Value;
            }
            set
            {
                var index = GetIndex(key);
                _pairs[index].Value = value;
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
                var valuesList = new List<TValue>();
                for (var i = 0; i < Count; i++)
                {
                    var value = _pairs[i].Value;
                    valuesList.Add(value);
                }

                return valuesList;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        private void KeyCheck(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }
        }

        public void Add(TKey key, TValue value)
        {
            KeyCheck(key);
            ExpandArrayCheck();
            var index = GetIndex(key);
            if (index != -1 && value != null)
            {
                _pairs[index].Value = value;
            }
            else
            {
                Count++;
                var newPair = new Pair<TKey, TValue>(key, value);
                _pairs[Count - 1] = newPair;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            for (var i = 0; i < Count; i++)
            {
                RemoveKeyValuePair(_pairs[i].Key);
            }
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var index = GetIndex(item.Key);
            return _pairs[index].Equals(item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            KeyCheck(key);
            return GetIndex(key) != -1;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

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

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
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
            KeyCheck(key);
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
            KeyCheck(key);
            if (GetIndex(key) == -1)
            {
                return false;
            }
            RemoveKeyValuePair(key);
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public int Size() => Count;

        public bool TryGetValue(TKey key, out TValue value)
        {
            KeyCheck(key);
            if (GetIndex(key) == -1)
            {
                value = default(TValue);
                return false;
            }
            value = _pairs[GetIndex(key)].Value;
            return true;
        }


        private bool KeyValuePairCheck(int index, TKey key)
        {
            return (_pairs[index] != null) && (_pairs[index].Key == null ||
                                               _pairs[index].Key.Equals(key));
        }

        public void RemoveKeyValuePair(TKey key)
        {
            KeyCheck(key);
            var index = GetIndex(key);
            if (index == -1)
            {
                throw new KeyNotFoundException();
            }

            if (KeyValuePairCheck(index, key))
            {
                _pairs[index] = _pairs[Count - 1];
                _pairs[Count - 1] = null;
            }
            Count--;
        }

        public TValue GetValue(TKey key)
        {
            KeyCheck(key);
            var index = GetIndex(key);
            var returnVal = default(TValue);
            if (index == -1)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                if (KeyValuePairCheck(index, key))
                { 
                    returnVal = _pairs[index].Value;
                }
            }
            return returnVal;
        }

        private class Pair<TKey, TValue>
        {
            public TKey Key { get; }
            public TValue Value { get; set; }

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