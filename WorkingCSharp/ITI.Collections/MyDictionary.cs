﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ITI.Collections
{
    public class MyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        int _count;
        Node[] _buckets;

        class Node
        {
            public readonly TKey Key;
            public TValue Value;
            public Node Next;

            public Node(TKey k) => Key = k;
        }

        public MyDictionary()
        {
            _buckets = new Node[11];
        }

        public int Count => _count;

        public void Remove(TKey key)
        {
            int h = GetHashCode(key);
            int idx = h % _buckets.Length;
            var (p, f) = FindInBucket(key, idx);
            if (f != null)
            {
                --_count;
                if (p == null) _buckets[idx] = f.Next;
                else p.Next = f.Next;
            }
        }

        int GetHashCode(TKey key) => Math.Abs(key.GetHashCode());

        bool Equals(TKey k1, TKey k2) => k1.Equals(k2);

        public void Add(TKey key, TValue value)
        {
            int h = GetHashCode(key);
            int idx = h % _buckets.Length;
            Node n = FindInBucket(key, idx).Found;
            if (n != null) throw new InvalidOperationException();
            else AddNewKeyValue(key, value, h, idx);
        }

        public TValue this[TKey key]
        {
            get
            {
                int h = GetHashCode(key);
                int idx = h % _buckets.Length;
                Node n = FindInBucket(key, idx).Found;
                if (n != null) return n.Value;
                throw new KeyNotFoundException();
            }
            set
            {
                int h = GetHashCode(key);
                int idx = h % _buckets.Length;
                Node n = FindInBucket(key, idx).Found;
                if (n != null)
                {
                    n.Value = value;
                }
                else AddNewKeyValue(key, value, h, idx);
            }
        }

        void AddNewKeyValue(TKey key, TValue value, int hashCode, int currentIdx)
        {
            int fillFactor = _count / _buckets.Length;
            if (fillFactor >= 2)
            {
                Grow();
                currentIdx = hashCode % _buckets.Length;
            }
            _count++;
            _buckets[currentIdx] = new Node(key)
            {
                Value = value,
                Next = _buckets[currentIdx]
            };
        }

        (Node Previous, Node Found) FindInBucket(TKey key, int idx)
        {
            Node prev = null;
            Node n = _buckets[idx];
            while (n != null)
            {
                if (Equals(n.Key, key)) break;
                prev = n;
                n = n.Next;
            }
            return (prev, n);
        }

        void Grow()
        {
            int newLength = NextPrimeNumber(_buckets.Length);
            var newBuckets = new Node[newLength];
            Node n;
            int idxB = 0;
            while (idxB < _buckets.Length && (n = _buckets[idxB++]) != null)
            {
                do
                {
                    var next = n.Next;
                    int idx = GetHashCode(n.Key) % newBuckets.Length;
                    n.Next = newBuckets[idx];
                    newBuckets[idx] = n;
                    n = next;
                }
                while (n != null);
            }
            _buckets = newBuckets;
        }

        static int NextPrimeNumber(int length)
        {
            return length * 2;
        }

        class EE : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            readonly MyDictionary<TKey, TValue> _holder;
            int _idxB;
            Node _n;

            public EE(MyDictionary<TKey, TValue> holder)
            {
                _holder = holder;
            }

            public KeyValuePair<TKey, TValue> Current => new KeyValuePair<TKey, TValue>(_n.Key, _n.Value);

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if ((_n = _n?.Next) != null) return true;
                while (_idxB < _holder._buckets.Length
                        && (_n = _holder._buckets[_idxB++]) == null) ;
                return _n != null;
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new EE(this);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumEasy()
        {
            int idxB = 0;
            Node n;
            while (idxB < _buckets.Length && (n = _buckets[idxB++]) != null)
            {
                do
                {
                    yield return new KeyValuePair<TKey, TValue>(n.Key, n.Value);
                    n = n.Next;
                }
                while (n != null);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
