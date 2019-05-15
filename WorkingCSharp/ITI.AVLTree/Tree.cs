using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ITI.AVLTree
{
    public class Tree<T> : IEnumerable<T> where T: IComparable
    {
        public Node<T> Head { get; set; }

        int _count;

        public void Add(T value)
        {
            throw new NotImplementedException();
        }

        public void Remove(T value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T value)
        {
            throw new NotImplementedException();
        }

        public int Count() => _count;

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
