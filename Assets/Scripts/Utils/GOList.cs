using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
}