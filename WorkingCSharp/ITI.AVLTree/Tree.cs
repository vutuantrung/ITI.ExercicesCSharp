using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ITI.AVLTree
{
    public class Tree<T> : IEnumerable<T> where T : IComparable
    {
        public Node<T> Head { get; set; }

        int _count;

        public int Count() => _count;

        public void Add(T value)
        {
            if (Head == null)
            {
                Head = new Node<T>(this, value);
                return;
            }

            AddToTree(Head, value);
            _count++;
        }

        private void AddToTree(Node<T> node, T value)
        {
            if (node.Left == null && node.Right == null)
            {
                Node<T> newNode = new Node<T>(this, value);
                if (value.CompareTo(node.Value) > 0) node.Left = newNode;
                else node.Right = newNode;
                newNode.Parent = node;
            }

            if (value.CompareTo(node.Value) < 0 && node.Left != null)
            {
                AddToTree(node.Left, value);
            }

            if (value.CompareTo(node.Value) > 0 && node.Right != null)
            {
                AddToTree(node.Right, value);
            }
        }

        public void Remove(T value)
        {
            throw new NotImplementedException();
        }

        public Node<T> SearchNode(Node<T> node, T value)
        {
            if (value.CompareTo(node.Value) == 0) return node;
            if(value.CompareTo(node.Value) < 0 && node.Left != null)
            {
                return SearchNode(node.Left, value);
            }
            if (value.CompareTo(node.Value) > 0 && node.Right != null)
            {
                return SearchNode(node.Right, value);
            }
            return null;
        }

        public bool Contains(T value) => SearchNode(Head, value) != null;

        public void Clear()
        {
            Head = null;
            _count = 0;
        }

        class EE : IEnumerator<T>
        {
            readonly Tree<T> _tree;
            Node<T> _currentNode;

            public EE(Tree<T> tree)
            {
                _tree = tree;
                _currentNode = _tree.Head;
            }

            public T Current
            {
                get
                {
                    if (_currentNode == null) throw new InvalidOperationException();
                    return _currentNode.Value;
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_currentNode.Right != null)
                {
                    _currentNode = _currentNode.Right;
                    return true;
                }
                if(_currentNode.Parent != null)
                {
                    Node<T> parentNode = _currentNode.Parent;
                    while(parentNode != null && _currentNode == parentNode.Right)
                    {
                        _currentNode = parentNode;
                        parentNode = parentNode.Parent;
                    }
                    return true;
                }
                return false;
            }

            public void Dispose()
            {
                throw new NotSupportedException();
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EE(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
