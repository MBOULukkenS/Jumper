using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class GOList<T> : IList<T> where T : MonoBehaviour
    {
        public int Count => _list.Count;
        public bool IsReadOnly => false;

        private readonly List<T> _list;

        public GOList() : this(new T[0])
        {
        }

        public GOList(int size)
        {
            _list = new List<T>(size);
        }

        public GOList(IEnumerable<T> items)
        {
            _list = new List<T>(items);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            foreach (T item in _list.ToList())
                Remove(item);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (item == null)
                return false;

            Object.Destroy(item.gameObject);
            return _list.Remove(item);
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Remove(_list.ElementAt(index));
        }

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
    }

    public class SortedGOList<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
        where TValue : MonoBehaviour
    {
        private readonly SortedList<TKey, TValue> _sortedList;

        public SortedGOList() : this(new Dictionary<TKey, TValue>())
        {}

        public SortedGOList(IDictionary<TKey, TValue> dictionary) : this(dictionary, Comparer<TKey>.Default)
        {}
        
        public SortedGOList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            _sortedList = new SortedList<TKey, TValue>(dictionary, comparer);
        }

        public bool Contains(object key)
        {
            return ((IDictionary)_sortedList).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)_sortedList).GetEnumerator();
        }

        public void Remove(object key)
        {
            if (key is MonoBehaviour obj)
                Object.Destroy(obj.gameObject);
            
            ((IDictionary)_sortedList).Remove(key);
        }

        public bool IsFixedSize => ((IDictionary)_sortedList).IsFixedSize;

        bool IDictionary.IsReadOnly => ((IDictionary)_sortedList).IsReadOnly;

        public object this[object key]
        {
            get => ((IDictionary)_sortedList)[key];
            set => ((IDictionary)_sortedList)[key] = value;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _sortedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sortedList.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)_sortedList).Add(item);
        }

        public void Add(object key, object value)
        {
            ((IDictionary)_sortedList).Add(key, value);
        }

        void IDictionary.Clear()
        {
            foreach (TKey key in new SortedList(_sortedList).Keys)
                Remove(key);
        }

        public void Clear()
        {
            foreach (TKey key in new SortedList<TKey, TValue>(_sortedList).Keys)
                Remove(key);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>) _sortedList).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>) _sortedList).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            Object.Destroy(item.Value);
            return ((IDictionary<TKey, TValue>) _sortedList).Remove(item);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_sortedList).CopyTo(array, index);
        }

        int ICollection.Count => _sortedList.Count;

        public bool IsSynchronized => ((ICollection) _sortedList).IsSynchronized;
        public object SyncRoot => ((ICollection)_sortedList).SyncRoot;

        public int Count => _sortedList.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            _sortedList.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _sortedList.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            Object.Destroy(_sortedList[key].gameObject);
            return _sortedList.Remove(key);;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _sortedList.TryGetValue(key, out value);
        }

        public TKey GetKey(TValue value)
        {
            return _sortedList.FirstOrDefault(kvp => kvp.Value == value).Key;
        }
        
        public TValue GetValue(TKey key)
        {
            return _sortedList.FirstOrDefault(kvp => kvp.Key.Equals(key)).Value;
        }

        public TValue this[TKey key]
        {
            get => _sortedList[key];
            set => _sortedList[key] = value;
        }

        ICollection IDictionary.Values => new[] {_sortedList.Values};

        ICollection IDictionary.Keys => new[] {_sortedList.Keys};

        public ICollection<TKey> Keys => _sortedList.Keys;
        public ICollection<TValue> Values => _sortedList.Values;

        /*public ICollection<TValue> Values => ((IDictionary<TKey, TValue>)_sortedList).Values;
        public ICollection<TKey> Keys => ((IDictionary<TKey, TValue>)_sortedList).Keys;*/
    }
}